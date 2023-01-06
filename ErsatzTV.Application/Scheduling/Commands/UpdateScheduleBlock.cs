using ErsatzTV.Core;

namespace ErsatzTV.Application.Scheduling;

public record UpdateScheduleBlock
    (int Id, string Name, List<UpdateScheduleBlockItem> Items) : IRequest<Either<BaseError, UpdateScheduleBlockResult>>;
