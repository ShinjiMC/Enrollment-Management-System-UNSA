using MediatR;
using ErrorOr;
namespace Application.Customers.Delete;

public record DeleteCustomerCommand(Guid Id) : IRequest<ErrorOr<Unit>>;