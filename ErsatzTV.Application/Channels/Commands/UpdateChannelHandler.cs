using ErsatzTV.Core;
using ErsatzTV.Core.Domain;
using ErsatzTV.Core.Domain.Scheduling;
using ErsatzTV.Infrastructure.Data;
using ErsatzTV.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ErsatzTV.Application.Channels;

// TODO: probably need to update a bunch more properties here for full channel edit capability
public class UpdateChannelHandler : IRequestHandler<UpdateChannel, Either<BaseError, UpdateChannelResult>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public UpdateChannelHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Either<BaseError, UpdateChannelResult>> Handle(
        UpdateChannel request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        Validation<BaseError, Channel> validation = await Validate(dbContext, request);
        return await validation.Apply(p => ApplyUpdateRequest(dbContext, p, request));
    }

    private async Task<UpdateChannelResult> ApplyUpdateRequest(
        TvContext dbContext,
        Channel c,
        UpdateChannel update)
    {
        c.Number = update.Number;
        c.Name = update.Name;
        c.FFmpegProfileId = update.FFmpegProfileId;
        // c.StreamingMode = update.StreamingMode;

        ChannelScheduleDayTemplate[] existing = c.ChannelScheduleDayTemplates.ToArray();
        UpdateChannelScheduleDayTemplateItem[] toAdd = update.ChannelScheduleDayTemplates
            .Filter(item => existing.All(l => l.Index != item.Index))
            .ToArray();
        ChannelScheduleDayTemplate[] toRemove = existing
            .Filter(item => update.ChannelScheduleDayTemplates.All(l => l.Index != item.Index)).ToArray();
        UpdateChannelScheduleDayTemplateItem[] toUpdate = update.ChannelScheduleDayTemplates
            .Filter(l => toAdd.All(a => a.Index != l.Index) && toRemove.All(r => r.Index != l.Index)).ToArray();

        c.ChannelScheduleDayTemplates.RemoveAll(toRemove.Contains);

        foreach (UpdateChannelScheduleDayTemplateItem item in toAdd)
        {
            c.ChannelScheduleDayTemplates.Add(
                new ChannelScheduleDayTemplate
                {
                    ScheduleDayTemplateId = item.ScheduleDayTemplateId,
                    Index = item.Index,
                    DaysOfWeek = GetDaysOfWeek(item.DaysOfWeek),
                    DaysOfMonth = GetDaysOfMonth(item.DaysOfMonth),
                    MonthsOfYear = GetMonthsOfYear(item.MonthsOfYear)
                });
        }

        foreach (UpdateChannelScheduleDayTemplateItem item in toUpdate)
        {
            Option<ChannelScheduleDayTemplate> maybeTarget =
                Optional(c.ChannelScheduleDayTemplates.Find(i => i.Index == item.Index));
            foreach (ChannelScheduleDayTemplate target in maybeTarget)
            {
                // schedule day template is part of the key, so we need to remove and re-add to change that
                if (target.ScheduleDayTemplateId != item.ScheduleDayTemplateId)
                {
                    c.ChannelScheduleDayTemplates.Remove(target);
                    c.ChannelScheduleDayTemplates.Add(
                        new ChannelScheduleDayTemplate
                        {
                            ScheduleDayTemplateId = item.ScheduleDayTemplateId,
                            Index = item.Index,
                            DaysOfWeek = GetDaysOfWeek(item.DaysOfWeek),
                            DaysOfMonth = GetDaysOfMonth(item.DaysOfMonth),
                            MonthsOfYear = GetMonthsOfYear(item.MonthsOfYear)
                        });
                }
                else
                {
                    // these properties can be updated on their own
                    target.Index = item.Index;
                    target.DaysOfWeek = GetDaysOfWeek(item.DaysOfWeek);
                    target.DaysOfMonth = GetDaysOfMonth(item.DaysOfMonth);
                    target.MonthsOfYear = GetMonthsOfYear(item.MonthsOfYear);
                }
            }
        }

        await dbContext.SaveChangesAsync();
        return new UpdateChannelResult(c.Id);
    }

    private static async Task<Validation<BaseError, Channel>> Validate(
        TvContext dbContext,
        UpdateChannel request) =>
        (await ChannelMustExist(dbContext, request), ValidateName(request))
        .Apply((scheduleBlockToUpdate, _) => scheduleBlockToUpdate);

    private static Task<Validation<BaseError, Channel>> ChannelMustExist(
        TvContext dbContext,
        UpdateChannel updateChannel) =>
        dbContext.Channels
            .Include(sb => sb.ChannelScheduleDayTemplates)
            .SelectOneAsync(p => p.Id, p => p.Id == updateChannel.Id)
            .Map(o => o.ToValidation<BaseError>("Channel does not exist."));

    private static Validation<BaseError, string> ValidateName(UpdateChannel updateChannel) =>
        updateChannel.NotEmpty(x => x.Name)
            .Bind(_ => updateChannel.NotLongerThan(50)(x => x.Name));

    private static ICollection<DayOfWeek> GetDaysOfWeek(ICollection<DayOfWeek> input) =>
        input is null || input.Count == 0
            ? new[]
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            }
            : input;

    private static ICollection<int> GetDaysOfMonth(ICollection<int> input) =>
        input is null || input.Count == 0 ? Enumerable.Range(1, 31).ToArray() : input;

    private static ICollection<int> GetMonthsOfYear(ICollection<int> input) =>
        input is null || input.Count == 0 ? Enumerable.Range(1, 12).ToArray() : input;
}
