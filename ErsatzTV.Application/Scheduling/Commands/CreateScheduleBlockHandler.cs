using ErsatzTV.Core;
using ErsatzTV.Core.Domain.Scheduling;
using ErsatzTV.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ErsatzTV.Application.Scheduling;

public class CreateScheduleBlockHandler :
    IRequestHandler<CreateScheduleBlock, Either<BaseError, CreateScheduleBlockResult>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public CreateScheduleBlockHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Either<BaseError, CreateScheduleBlockResult>> Handle(
        CreateScheduleBlock request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        Validation<BaseError, ScheduleBlock> validation = await Validate(dbContext, request);
        return await validation.Apply(scheduleBlock => PersistScheduleBlock(dbContext, scheduleBlock));
    }

    private static async Task<CreateScheduleBlockResult> PersistScheduleBlock(
        TvContext dbContext,
        ScheduleBlock scheduleBlock)
    {
        await dbContext.ScheduleBlocks.AddAsync(scheduleBlock);
        await dbContext.SaveChangesAsync();
        return new CreateScheduleBlockResult(scheduleBlock.Id);
    }

    private Task<Validation<BaseError, ScheduleBlock>> Validate(
        TvContext dbContext,
        CreateScheduleBlock request) =>
        ValidateName(dbContext, request)
            .MapT(
                name => new ScheduleBlock
                {
                    Name = name,
                    Items = request.Items.Map(
                        item => new ScheduleBlockItem
                        {
                            Index = item.Index,
                            Name = item.Name,
                            Query = item.Query,
                            PlayoutMode = item.PlayoutMode,
                            PlaybackOrder = item.PlaybackOrder
                        }).ToList()
                });

    private static async Task<Validation<BaseError, string>> ValidateName(
        TvContext dbContext,
        CreateScheduleBlock createScheduleBlock)
    {
        Validation<BaseError, string> result1 = createScheduleBlock.NotEmpty(c => c.Name)
            .Bind(_ => createScheduleBlock.NotLongerThan(50)(c => c.Name));

        int duplicateNameCount = await dbContext.ScheduleBlocks
            .CountAsync(ps => ps.Name == createScheduleBlock.Name);

        var result2 = Optional(duplicateNameCount)
            .Where(count => count == 0)
            .ToValidation<BaseError>("ScheduleBlock name must be unique");

        return (result1, result2).Apply((_, _) => createScheduleBlock.Name);
    }
}
