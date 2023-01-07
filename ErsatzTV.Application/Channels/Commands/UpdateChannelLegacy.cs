using ErsatzTV.Core;
using ErsatzTV.Core.Domain;

namespace ErsatzTV.Application.Channels;

[Obsolete("This drives the legacy UI and should be removed")]
public record UpdateChannelLegacy
(
    int ChannelId,
    string Name,
    string Number,
    string Group,
    string Categories,
    int FFmpegProfileId,
    string Logo,
    string PreferredAudioLanguageCode,
    string PreferredAudioTitle,
    StreamingMode StreamingMode,
    int? WatermarkId,
    int? FallbackFillerId,
    string PreferredSubtitleLanguageCode,
    ChannelSubtitleMode SubtitleMode,
    ChannelMusicVideoCreditsMode MusicVideoCreditsMode,
    string MusicVideoCreditsTemplate) : IRequest<Either<BaseError, ChannelViewModel>>;
