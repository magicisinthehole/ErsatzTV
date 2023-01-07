using ErsatzTV.Core.Api.Channels;

namespace ErsatzTV.Application.Channels;

public record GetChannelByIdForApi(int Id) : IRequest<Option<ChannelResponseModel>>;
