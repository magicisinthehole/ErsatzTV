using ErsatzTV.Core.Api.Scheduling;

namespace ErsatzTV.Application.Scheduling;

public record GetScheduleBlockByIdForApi(int Id) : IRequest<Option<ScheduleBlockResponseModel>>;
