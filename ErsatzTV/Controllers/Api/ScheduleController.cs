using System.ComponentModel.DataAnnotations;
using ErsatzTV.Application.Scheduling;
using ErsatzTV.Core;
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
    public async Task<List<ScheduleBlockResponseModel>> GetAllBlocks() =>
        await _mediator.Send(new GetAllScheduleBlocksForApi());

    [HttpPost("/api/schedule/blocks/new")]
    public async Task<Either<BaseError, CreateScheduleBlockResult>> AddOneBlock(
        [Required] [FromBody]
        CreateScheduleBlock request) => await _mediator.Send(request);

    [HttpPut("/api/schedule/blocks/update")]
    public async Task<Either<BaseError, UpdateScheduleBlockResult>> UpdateOneBlock(
        [Required] [FromBody]
        UpdateScheduleBlock request) => await _mediator.Send(request);

    [HttpGet("/api/schedule/blocks/{id}")]
    public async Task<Option<ScheduleBlockResponseModel>> GetOneBlock(int id) =>
        await _mediator.Send(new GetScheduleBlockByIdForApi(id));

    [HttpGet("/api/schedule/day-templates")]
    public async Task<List<ScheduleDayTemplateResponseModel>> GetAllDayTemplates() =>
        await _mediator.Send(new GetAllScheduleDayTemplatesForApi());

    [HttpPost("/api/schedule/day-templates/new")]
    public async Task<Either<BaseError, CreateScheduleDayTemplateResult>> AddOneDayTemplate(
        [Required] [FromBody]
        CreateScheduleDayTemplate request) => await _mediator.Send(request);

    [HttpGet("/api/schedule/day-templates/{id}")]
    public async Task<Option<ScheduleDayTemplateResponseModel>> GetOneDayTemplate(int id) =>
        await _mediator.Send(new GetScheduleDayTemplateByIdForApi(id));

    [HttpPut("/api/schedule/day-templates/update")]
    public async Task<Either<BaseError, UpdateScheduleDayTemplateResult>> UpdateOneDayTemplate(
        [Required] [FromBody]
        UpdateScheduleDayTemplate request) => await _mediator.Send(request);
}
