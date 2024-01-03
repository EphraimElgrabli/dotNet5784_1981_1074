
namespace DO;

public record Dependency
(
  int Id,
  int DependentTask,
  int DependsOnTask
)
{
    public Dependency() : this(0) { } //empty ctor for stage 3

    
}
