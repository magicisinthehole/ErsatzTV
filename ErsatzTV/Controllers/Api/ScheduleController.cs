using ErsatzTV.Application.Scheduling;
using ErsatzTV.Core.Api.Scheduling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErsatzTV.Controllers.Api;

[ApiController]
public class ScheduleController
{
    private readonly IMediator _mediator;

    public ScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/api/schedule/blocks")]
    public async Task<List<ScheduleBlockResponseModel>> GetAll() =>
        await _mediator.Send(new GetAllScheduleBlocksForApi());
}
