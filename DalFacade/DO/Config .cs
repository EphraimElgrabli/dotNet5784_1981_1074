
namespace DO;

public record Config
(
    DO.Status Status,
    DateTime StartDate,
    DateTime EndDate
)
{
    public Config() : this(0,0,0) { } //empty ctor for stage 3

    /// <summary>
    /// RegistrationDate - registration date of the current student record
    /// </summary>
    public DateTime RegistrationDate => DateTime.Now; //get only

}
