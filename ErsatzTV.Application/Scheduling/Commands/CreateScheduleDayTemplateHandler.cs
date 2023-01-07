using ErsatzTV.Core;
using ErsatzTV.Core.Domain.Scheduling;
using ErsatzTV.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ErsatzTV.Application.Scheduling;

public class CreateScheduleDayTemplateHandler :
    IRequestHandler<CreateScheduleDayTemplate, Either<BaseError, CreateScheduleDayTemplateResult>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public CreateScheduleDayTemplateHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Either<BaseError, CreateScheduleDayTemplateResult>> Handle(
        CreateScheduleDayTemplate request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        Validation<BaseError, ScheduleDayTemplate> validation = await Validate(dbContext, request);
        return await validation.Apply(
            scheduleDayTemplate => PersistScheduleDayTemplate(dbContext, scheduleDayTemplate));
    }

    private static async Task<CreateScheduleDayTemplateResult> PersistScheduleDayTemplate(
        TvContext dbContext,
        ScheduleDayTemplate scheduleDayTemplate)
    {
        await dbContext.ScheduleDayTemplates.AddAsync(scheduleDayTemplate);
        await dbContext.SaveChangesAsync();
        return new CreateScheduleDayTemplateResult(scheduleDayTemplate.Id);
    }

    private Task<Validation<BaseError, ScheduleDayTemplate>> Validate(
        TvContext dbContext,
        CreateScheduleDayTemplate request) =>
        ValidateName(dbContext, request)
            .MapT(
                name => new ScheduleDayTemplate
                {
                    Name = name,
                    Items = request.Items.Map(
                        item => new ScheduleDayTemplateItem
                        {
                            Index = item.Index,
                            StartTime = item.StartTime,
                            ScheduleBlockId = item.ScheduleBlockId
                        }).ToList()
                });

    private static async Task<Validation<BaseError, string>> ValidateName(
        TvContext dbContext,
        CreateScheduleDayTemplate createScheduleDayTemplate)
    {
        Validation<BaseError, string> result1 = createScheduleDayTemplate.NotEmpty(c => c.Name)
            .Bind(_ => createScheduleDayTemplate.NotLongerThan(50)(c => c.Name));

        int duplicateNameCount = await dbContext.ScheduleDayTemplates
            .CountAsync(ps => ps.Name == createScheduleDayTemplate.Name);

        var result2 = Optional(duplicateNameCount)
            .Where(count => count == 0)
            .ToValidation<BaseError>("ScheduleDayTemplate name must be unique");

        return (result1, result2).Apply((_, _) => createScheduleDayTemplate.Name);
    }
}
