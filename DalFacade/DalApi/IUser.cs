namespace DalApi;
using DO;

/// <summary>
/// Interface declaration for IUser, extending ICrud interface with Dependency as the generic type
/// </summary>
public interface IUser : ICrud<User> { }