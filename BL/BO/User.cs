namespace BO;
using DO;


public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public UserLevel Level { get; set; }
    public override string ToString() => this.ToStringProperty();
}
