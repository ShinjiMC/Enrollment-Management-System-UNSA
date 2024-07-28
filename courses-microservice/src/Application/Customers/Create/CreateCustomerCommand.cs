using MediatR;
using ErrorOr;
namespace Application.Customers.Create;

public record CreateCustomerCommand(
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string Zipcode
) : IRequest<ErrorOr<Unit>>;