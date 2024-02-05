namespace BO;

public class Milestone
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public Status? Status { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double completionPrecentage { get; set; }
    public string? Remarks { get; set; }
    public List<TaskInList>? Dependencies { get; set; } = null;
    public override string ToString() => this.ToStringProperty();
}
