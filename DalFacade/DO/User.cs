
namespace DO;

public record User
(
    int Id,
    string Email,
    string PhoneNumber,
    string Name,
    DO.UserLevel Level
)
{
    public User() : this(0, null, null,null,0) { } //empty ctor for stage 3
};
