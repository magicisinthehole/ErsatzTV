using ErsatzTV.Core.Api.Scheduling;

namespace ErsatzTV.Application.Scheduling;

public record GetAllScheduleBlocksForApi : IRequest<List<ScheduleBlockResponseModel>>;
