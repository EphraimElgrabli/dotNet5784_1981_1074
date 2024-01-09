namespace DO;

/// <summary>
/// Represents a dependency between two tasks in the wedding management app.
/// </summary>
/// <param name="Id">The unique identifier for the dependency.</param>
/// <param name="DependentTask">The task that depends on another task.</param>
/// <param name="DependsOnTask">The task that is a dependency for another task.</param>
public record Dependency
(
    int Id,
    int? DependentTask = null,
    int? DependsOnTask = null
)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Dependency"/> class.
    /// </summary>
    /// <param name="Id">The unique identifier for the dependency.</param>
    public Dependency() : this(0) { } // Empty constructor for stage 3
}
