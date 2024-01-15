namespace DO;

/// <summary>
/// Represents a dependency between two tasks in the wedding management app.
/// </summary>
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
