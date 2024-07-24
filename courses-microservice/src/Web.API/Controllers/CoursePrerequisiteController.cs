using Application.Customers.Create;
using Application.Customers.Delete;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ErrorOr;
using Application.CoursesPrerequisite.Create;
using Application.CoursesPrerequisite.Delete;
using Application.CoursesPrerequisite.GetAll;

namespace Web.API.Controllers;

[Route("course/prerequisite")]
public class CoursePrerequisite : ApiController
{
    private readonly ISender _mediator;

    public CoursePrerequisite(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var customerResult = await _mediator.Send(new GetAllCoursesPrerequisiteQuery(id));

        return customerResult.Match(
            customer => Ok(customer),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCoursesPrerequisiteCommand command)
    {
        var createResult = await _mediator.Send(command);

        return createResult.Match(
            customerId => Ok(customerId),
            errors => Problem(errors)
        );
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteCoursePrerequisiteCommand(id));

        return deleteResult.Match(
            customerId => NoContent(),
            errors => Problem(errors)
        );
    }
}