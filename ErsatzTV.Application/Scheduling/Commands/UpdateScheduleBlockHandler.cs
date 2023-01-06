using ErsatzTV.Core;
using ErsatzTV.Core.Domain.Scheduling;
using ErsatzTV.Infrastructure.Data;
using ErsatzTV.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ErsatzTV.Application.Scheduling;

public class
    UpdateScheduleBlockHandler : IRequestHandler<UpdateScheduleBlock, Either<BaseError, UpdateScheduleBlockResult>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public UpdateScheduleBlockHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Either<BaseError, UpdateScheduleBlockResult>> Handle(
        UpdateScheduleBlock request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        Validation<BaseError, ScheduleBlock> validation = await Validate(dbContext, request);
        return await validation.Apply(p => ApplyUpdateRequest(dbContext, p, request));
    }

    private async Task<UpdateScheduleBlockResult> ApplyUpdateRequest(
        TvContext dbContext,
        ScheduleBlock sb,
        UpdateScheduleBlock update)
    {
        sb.Name = update.Name;
        
        ScheduleBlockItem[] existing = sb.Items.ToArray();
        UpdateScheduleBlockItem[] toAdd = update.Items.Filter(item => existing.All(l => l.Index != item.Index)).ToArray();
        ScheduleBlockItem[] toRemove = existing.Filter(item => update.Items.All(l => l.Index != item.Index)).ToArray();
        UpdateScheduleBlockItem[] toUpdate = update.Items
            .Filter(l => toAdd.All(a => a.Index != l.Index) && toRemove.All(r => r.Index != l.Index)).ToArray();

        sb.Items.RemoveAll(toRemove.Contains);

        foreach (UpdateScheduleBlockItem item in toAdd)
        {
            sb.Items.Add(
                new ScheduleBlockItem
                {
                    Index = item.Index,
                    Name = item.Name,
                    Query = item.Query,
                    PlayoutMode = item.PlayoutMode,
                    PlaybackOrder = item.PlaybackOrder
                });
        }
        
        foreach (UpdateScheduleBlockItem item in toUpdate)
        {
            foreach (ScheduleBlockItem target in sb.Items.Filter(i => i.Index == item.Index))
            {
                target.Index = item.Index;
                target.Name = item.Name;
                target.Query = item.Query;
                target.PlayoutMode = item.PlayoutMode;
                target.PlaybackOrder = item.PlaybackOrder;
            }
        }

        await dbContext.SaveChangesAsync();
        return new UpdateScheduleBlockResult(sb.Id);
    }

    private static async Task<Validation<BaseError, ScheduleBlock>> Validate(
        TvContext dbContext,
        UpdateScheduleBlock request) =>
        (await ScheduleBlockMustExist(dbContext, request), ValidateName(request))
        .Apply((scheduleBlockToUpdate, _) => scheduleBlockToUpdate);

    private static Task<Validation<BaseError, ScheduleBlock>> ScheduleBlockMustExist(
        TvContext dbContext,
        UpdateScheduleBlock updateScheduleBlock) =>
        dbContext.ScheduleBlocks
            .Include(sb => sb.Items)
            .SelectOneAsync(p => p.Id, p => p.Id == updateScheduleBlock.Id)
            .Map(o => o.ToValidation<BaseError>("ScheduleBlock does not exist."));

    private static Validation<BaseError, string> ValidateName(UpdateScheduleBlock updateScheduleBlock) =>
        updateScheduleBlock.NotEmpty(x => x.Name)
            .Bind(_ => updateScheduleBlock.NotLongerThan(50)(x => x.Name));
}
