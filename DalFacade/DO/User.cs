namespace DO;

/// <summary>
/// Represents a user in the wedding management app.
/// </summary>
/// <param name="Id">The unique identifier for the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="PhoneNumber">The phone number of the user.</param>
/// <param name="Name">The name of the user.</param>
/// <param name="Level">The level of involvement of the user (e.g., supportes, closeFriends).</param>
public record User
(
    int Id,
    string Email,
    string PhoneNumber,
    string Name,
    DO.UserLevel Level
)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="Id">The unique identifier for the user.</param>
    public User() : this(0, "", "", "", 0) { } // Empty constructor for stage 3
};