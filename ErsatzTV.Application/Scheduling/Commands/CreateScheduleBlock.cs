using ErsatzTV.Core;

namespace ErsatzTV.Application.Scheduling;

public record CreateScheduleBlock
    (string Name, List<CreateScheduleBlockItem> Items) : IRequest<Either<BaseError, CreateScheduleBlockResult>>;
