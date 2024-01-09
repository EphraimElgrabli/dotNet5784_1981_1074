namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="DependentTask"></param>
/// <param name="DependsOnTask"></param>
public record Dependency
(
    int Id,
  int? DependentTask=null,
  int? DependsOnTask = null
)
{

    public Dependency() : this(0) { } //empty ctor for stage 3

    
}
