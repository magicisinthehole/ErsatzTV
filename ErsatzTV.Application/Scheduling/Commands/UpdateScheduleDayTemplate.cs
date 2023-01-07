using ErsatzTV.Core;

namespace ErsatzTV.Application.Scheduling;

public record UpdateScheduleDayTemplate
    (int Id, string Name, List<UpdateScheduleDayTemplateItem> Items) : IRequest<
        Either<BaseError, UpdateScheduleDayTemplateResult>>;
