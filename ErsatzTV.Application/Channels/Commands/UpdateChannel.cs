using ErsatzTV.Core;

namespace ErsatzTV.Application.Channels;

public record UpdateChannel(
        int Id,
        string Number,
        string Name,
        int FFmpegProfileId,
        List<UpdateChannelScheduleDayTemplateItem> ChannelScheduleDayTemplates)
    : IRequest<Either<BaseError, UpdateChannelResult>>;
