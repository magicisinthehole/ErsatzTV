using ErsatzTV.Core.Api.Scheduling;

namespace ErsatzTV.Application.Scheduling;

public record GetScheduleDayTemplateByIdForApi(int Id) : IRequest<Option<ScheduleDayTemplateResponseModel>>;
