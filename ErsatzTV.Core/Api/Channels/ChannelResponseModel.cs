using Newtonsoft.Json;

namespace ErsatzTV.Core.Api.Channels;

public record ChannelResponseModel(
    int Id,
    string Number,
    string Name,
    [property: JsonProperty("ffmpegProfileId")]
    int FFmpegProfileId,
    [property: JsonProperty("ffmpegProfile")]
    string FFmpegProfile,
    string Language,
    string StreamingMode,
    List<ChannelScheduleDayTemplateResponseModel> ChannelScheduleDayTemplates);
