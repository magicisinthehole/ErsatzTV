using ErsatzTV.Core.Api.Scheduling;
using ErsatzTV.Core.Domain.Scheduling;
using ErsatzTV.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static ErsatzTV.Application.Scheduling.Mapper;

namespace ErsatzTV.Application.Scheduling;

public class GetAllScheduleDayTemplatesForApiHandler : IRequestHandler<GetAllScheduleDayTemplatesForApi,
    List<ScheduleDayTemplateResponseModel>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public GetAllScheduleDayTemplatesForApiHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<List<ScheduleDayTemplateResponseModel>> Handle(
        GetAllScheduleDayTemplatesForApi request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        List<ScheduleDayTemplate> scheduleDayTemplates = await dbContext.ScheduleDayTemplates
            .AsNoTracking()
            .Include(dt => dt.Items)
            .ThenInclude(ti => ti.ScheduleBlock)
            .ToListAsync(cancellationToken);
        return scheduleDayTemplates.Map(ProjectToResponseModel).ToList();
    }
}
