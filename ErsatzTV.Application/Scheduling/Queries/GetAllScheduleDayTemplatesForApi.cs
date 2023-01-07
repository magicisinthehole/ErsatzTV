using ErsatzTV.Core.Api.Scheduling;

namespace ErsatzTV.Application.Scheduling;

public record GetAllScheduleDayTemplatesForApi : IRequest<List<ScheduleDayTemplateResponseModel>>;
