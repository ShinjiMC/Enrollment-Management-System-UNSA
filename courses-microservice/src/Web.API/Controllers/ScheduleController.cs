using Microsoft.AspNetCore.Mvc;
using MediatR;
using ErrorOr;
using Application.Schedules.Create;
using Application.Schedules.GetAll;
using Application.Schedules.GetById;
using Application.Schedules.Update;
using Application.Schedules.GetByYearSemesterSchool;

namespace Web.API.Controllers;

[Route("schedules")]
public class Schedules : ApiController
{
    private readonly ISender _mediator;

    public Schedules(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScheduleCommand command)
    {
        var createResult = await _mediator.Send(command);
        
        return createResult.Match(
            customerId => Ok(customerId),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var createResult = await _mediator.Send(new GetAllScheduleQuery());
        
        return createResult.Match(
            customerId => Ok(customerId),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customerResult = await _mediator.Send(new GetByIdScheduleQuery(id));

        return customerResult.Match(
            customer => Ok(customer),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScheduleCommand command)
    {
        if (command.ScheduleId != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Schedule.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            customerId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] int year, [FromQuery] string semester , [FromQuery] string schoolId)
        {
            var query = new GetByYearSemesterSchool(year, semester, schoolId);
            var searchResult = await _mediator.Send(query);

            return searchResult.Match(
                schedules => Ok(schedules),
                errors => Problem(errors)
            );
        }
}