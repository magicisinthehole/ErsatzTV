using ErsatzTV.Core.Api.Scheduling;
using ErsatzTV.Core.Domain.Scheduling;
using ErsatzTV.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static ErsatzTV.Application.Scheduling.Mapper;

namespace ErsatzTV.Application.Scheduling;

public class
    GetAllScheduleBlocksForApiHandler : IRequestHandler<GetAllScheduleBlocksForApi, List<ScheduleBlockResponseModel>>
{
    private readonly IDbContextFactory<TvContext> _dbContextFactory;

    public GetAllScheduleBlocksForApiHandler(IDbContextFactory<TvContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public async Task<List<ScheduleBlockResponseModel>> Handle(
        GetAllScheduleBlocksForApi request,
        CancellationToken cancellationToken)
    {
        await using TvContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        List<ScheduleBlock> scheduleBlocks = await dbContext.ScheduleBlocks
            .AsNoTracking()
            .Include(sb => sb.Items)
            .ToListAsync(cancellationToken);
        return scheduleBlocks.Map(ProjectToResponseModel).ToList();
    }
}
