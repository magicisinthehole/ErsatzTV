using ErsatzTV.Core;
using ErsatzTV.Core.Scheduling;

namespace ErsatzTV.Application.Playouts;

public record BuildChannelPlayout(int ChannelId, PlayoutBuildMode Mode) : IRequest<Either<BaseError, Unit>>,
    IBackgroundServiceRequest;
