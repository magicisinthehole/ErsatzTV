using ErsatzTV.Core;
using ErsatzTV.Core.Domain.Scheduling;
using ErsatzTV.Infrastructure.Data;
using ErsatzTV.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ErsatzTV.Application.Scheduling;

public class
    UpdateScheduleDayTemplateHandler : IRequestHandler<UpdateScheduleDayTemplate,
        Either<BaseError, UpdateScheduleDayTemplateResult>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public UpdateScheduleDayTemplateHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Either<BaseError, UpdateScheduleDayTemplateResult>> Handle(
        UpdateScheduleDayTemplate request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        Validation<BaseError, ScheduleDayTemplate> validation = await Validate(dbContext, request);
        return await validation.Apply(p => ApplyUpdateRequest(dbContext, p, request));
    }

    private async Task<UpdateScheduleDayTemplateResult> ApplyUpdateRequest(
        TvContext dbContext,
        ScheduleDayTemplate sb,
        UpdateScheduleDayTemplate update)
    {
        sb.Name = update.Name;

        ScheduleDayTemplateItem[] existing = sb.Items.ToArray();
        UpdateScheduleDayTemplateItem[] toAdd = update.Items.Filter(item => existing.All(l => l.Index != item.Index))
            .ToArray();
        ScheduleDayTemplateItem[] toRemove =
            existing.Filter(item => update.Items.All(l => l.Index != item.Index)).ToArray();
        UpdateScheduleDayTemplateItem[] toUpdate = update.Items
            .Filter(l => toAdd.All(a => a.Index != l.Index) && toRemove.All(r => r.Index != l.Index)).ToArray();

        sb.Items.RemoveAll(toRemove.Contains);

        foreach (UpdateScheduleDayTemplateItem item in toAdd)
        {
            sb.Items.Add(
                new ScheduleDayTemplateItem
                {
                    Index = item.Index,
                    StartTime = item.StartTime,
                    ScheduleBlockId = item.ScheduleBlockId
                });
        }

        foreach (UpdateScheduleDayTemplateItem item in toUpdate)
        {
            Option<ScheduleDayTemplateItem> maybeTarget = Optional(sb.Items.Find(i => i.Index == item.Index));
            foreach (ScheduleDayTemplateItem target in maybeTarget)
            {
                // schedule block is part of the key, so we need to remove and re-add to change that
                if (target.ScheduleBlockId != item.ScheduleBlockId)
                {
                    sb.Items.Remove(target);
                    sb.Items.Add(
                        new ScheduleDayTemplateItem
                        {
                            Index = item.Index,
                            StartTime = item.StartTime,
                            ScheduleBlockId = item.ScheduleBlockId
                        });
                }
                else
                {
                    // these properties can be updated on their own
                    target.Index = item.Index;
                    target.StartTime = item.StartTime;
                }
            }
        }

        await dbContext.SaveChangesAsync();
        return new UpdateScheduleDayTemplateResult(sb.Id);
    }

    private static async Task<Validation<BaseError, ScheduleDayTemplate>> Validate(
        TvContext dbContext,
        UpdateScheduleDayTemplate request) =>
        (await ScheduleDayTemplateMustExist(dbContext, request), ValidateName(request))
        .Apply((scheduleDayTemplateToUpdate, _) => scheduleDayTemplateToUpdate);

    private static Task<Validation<BaseError, ScheduleDayTemplate>> ScheduleDayTemplateMustExist(
        TvContext dbContext,
        UpdateScheduleDayTemplate updateScheduleDayTemplate) =>
        dbContext.ScheduleDayTemplates
            .Include(sb => sb.Items)
            .SelectOneAsync(p => p.Id, p => p.Id == updateScheduleDayTemplate.Id)
            .Map(o => o.ToValidation<BaseError>("ScheduleDayTemplate does not exist."));

    private static Validation<BaseError, string> ValidateName(UpdateScheduleDayTemplate updateScheduleDayTemplate) =>
        updateScheduleDayTemplate.NotEmpty(x => x.Name)
            .Bind(_ => updateScheduleDayTemplate.NotLongerThan(50)(x => x.Name));
}
