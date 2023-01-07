using ErsatzTV.Core;

namespace ErsatzTV.Application.Scheduling;

public record CreateScheduleDayTemplate(string Name, List<CreateScheduleDayTemplateItem> Items)
    : IRequest<Either<BaseError, CreateScheduleDayTemplateResult>>;
