using ErsatzTV.Core.Api.Channels;
using ErsatzTV.Core.Interfaces.Repositories;
using static ErsatzTV.Application.Channels.Mapper;

namespace ErsatzTV.Application.Channels;

public class GetChannelByIdForApiHandler : IRequestHandler<GetChannelByIdForApi, Option<ChannelResponseModel>>
{
    private readonly IChannelRepository _channelRepository;

    public GetChannelByIdForApiHandler(IChannelRepository channelRepository) => _channelRepository = channelRepository;

    public Task<Option<ChannelResponseModel>> Handle(
        GetChannelByIdForApi request,
        CancellationToken cancellationToken) =>
        _channelRepository.Get(request.Id).MapT(ProjectToResponseModel);
}
