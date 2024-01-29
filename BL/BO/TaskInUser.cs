namespace BO;

public class TaskInUser
{
    public int Id { get; set; }
    public string Alias { get; set; }
    public override string ToString() => this.ToStringProperty();
}
