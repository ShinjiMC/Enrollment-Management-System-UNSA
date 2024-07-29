using Application.Customers.Create;
using Application.Customers.Delete;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ErrorOr;
using Application.Courses.Create;
using Application.Courses.GetAll;
using Application.Courses.GetById;
using Application.Courses.Update;
using Application.Courses.Delete;

namespace Web.API.Controllers;

[Route("api/v1/courses")]
public class Courses : ApiController
{
    private readonly ISender _mediator;

    public Courses(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customersResult = await _mediator.Send(new GetAllCoursesQuery());

        return customersResult.Match(
            customers => Ok(customers),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customerResult = await _mediator.Send(new GetByIdCoursesQuery(id));

        return customerResult.Match(
            customer => Ok(customer),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
        courseId => CreatedAtAction(nameof(GetById), new { id = courseId }, courseId),
        errors => Problem(errors)
    );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Customer.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }
        var updateResult = await _mediator.Send(command);
        return updateResult.Match(
            customerId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteCourseCommand(id));

        return deleteResult.Match(
            customerId => NoContent(),
            errors => Problem(errors)
        );
    }
}