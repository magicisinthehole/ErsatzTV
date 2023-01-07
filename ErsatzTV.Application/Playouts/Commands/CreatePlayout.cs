using ErsatzTV.Core;

namespace ErsatzTV.Application.Playouts;

public record CreatePlayout(
    int ChannelId,
    int ProgramScheduleId) : IRequest<Either<BaseError, CreatePlayoutResponse>>;
