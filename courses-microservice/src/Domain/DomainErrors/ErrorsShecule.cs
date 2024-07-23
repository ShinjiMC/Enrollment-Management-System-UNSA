using ErrorOr;

namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class Schedule
    {
        
        public static Error InvalidScheduleDetails => 
            Error.Validation("Schedule.Details", "Details has not valid format.");


        public static Error AddressWithBadFormat => 
            Error.Validation("Customer.Address", "Address is not valid.");
    }
}