namespace DO;

/// <summary>
/// Exception thrown when a Data Access Layer (DAL) does not exist.
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DalDoesNotExistException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DalDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown when a Data Access Layer (DAL) already exists.
/// </summary>
[Serializable]
public class DalAlreadyExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DalAlreadyExistException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DalAlreadyExistException(string? message) : base(message) { }
}
