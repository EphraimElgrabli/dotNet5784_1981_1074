namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string Alias { get; set; }
    public int Cost { get; set;}
    public DateTime CreatedAtDate { get; set; }
    public Status Status{ get; set; }
    public List<TaskInList>? Dependencies { get; set; } = null;
    public MilestoneInTask? Milestone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public UserInTask? User { get; set; }
    public UserLevel Complexity { get; set; }
    public double pracentstart { get; set; }
    public double pracentbetween { get; set; }
    public double pracentend { get; set; }
    public override string ToString() => this.ToStringProperty();
}
/*
 */
