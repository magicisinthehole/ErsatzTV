using ErsatzTV.Core.Api.Scheduling;
using ErsatzTV.Infrastructure.Data;
using ErsatzTV.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using static ErsatzTV.Application.Scheduling.Mapper;

namespace ErsatzTV.Application.Scheduling;

public class GetScheduleBlockByIdForApiHandler : IRequestHandler<GetScheduleBlockByIdForApi, Option<ScheduleBlockResponseModel>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public GetScheduleBlockByIdForApiHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<Option<ScheduleBlockResponseModel>> Handle(
        GetScheduleBlockByIdForApi request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.ScheduleBlocks
            .AsNoTracking()
            .Include(sb => sb.Items)
            .SelectOneAsync(p => p.Id, p => p.Id == request.Id)
            .MapT(ProjectToResponseModel);
    }
}
