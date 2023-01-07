using ErsatzTV.Core.Api.Scheduling;
using ErsatzTV.Infrastructure.Data;
using ErsatzTV.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using static ErsatzTV.Application.Scheduling.Mapper;

namespace ErsatzTV.Application.Scheduling;

public class GetScheduleDayTemplateByIdForApiHandler : IRequestHandler<GetScheduleDayTemplateByIdForApi,
    Option<ScheduleDayTemplateResponseModel>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public GetScheduleDayTemplateByIdForApiHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Option<ScheduleDayTemplateResponseModel>> Handle(
        GetScheduleDayTemplateByIdForApi request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.ScheduleDayTemplates
            .AsNoTracking()
            .Include(sb => sb.Items)
            .ThenInclude(sb => sb.ScheduleBlock)
            .SelectOneAsync(p => p.Id, p => p.Id == request.Id)
            .MapT(ProjectToResponseModel);
    }
}
