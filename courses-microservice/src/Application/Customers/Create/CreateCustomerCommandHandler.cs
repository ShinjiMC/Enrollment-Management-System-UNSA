using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;
namespace Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork){
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if(PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber){
                return Errors.Customer.PhoneNumberWithBadFormat;
            }
            if(Address.Create(command.Country, command.Line1, command.Line2, command.City, command.State, command.Zipcode) is not Address address){
                return Errors.Customer.AddressWithBadFormat;
            }
            var customer = new Customer(
                Guid.NewGuid(),
                command.Name,
                command.LastName,
                command.Email,
                phoneNumber,
                address,
                true 
            );

            _customerRepository.Add(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Failure("Customer.Create.Failure", ex.Message);
        }
    }
}