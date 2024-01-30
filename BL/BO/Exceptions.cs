namespace DO;

/// <summary>
/// Exception thrown when a BL does not exist.
/// </summary>
[Serializable]
public class BlDoesNotExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlDoesNotExistException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown when a BL already exists.
/// </summary>
[Serializable]
public class BlAlreadyExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlAlreadyExistException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlAlreadyExistException(string? message) : base(message) { }
}
[Serializable]
public class CanNotDeletedException : Exception
{
    public CanNotDeletedException(string? message) : base(message) { }
}


[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlXMLFileLoadCreateException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
}

[Serializable]
public class BlNullPropertyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlNullPropertyException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlNullPropertyException(string? message) : base(message) { }
}