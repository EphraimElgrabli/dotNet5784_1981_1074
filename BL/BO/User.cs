namespace BO;
using DO;


public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public UserLevel Level { get; set; }
    public List<TaskInUser>? Tasks { get; set; } = null;//here all the task that this user need to do
    public override string ToString() => this.ToStringProperty();
}
