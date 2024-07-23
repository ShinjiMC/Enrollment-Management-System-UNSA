namespace users_microservice.models;
public class AdminModel
{
    public int ID { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string FullName { get; set; }
    public Role Role { get; set; }
}