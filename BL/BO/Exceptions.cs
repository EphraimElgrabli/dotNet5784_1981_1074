namespace BO;

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
    public BlDoesNotExistException() { }
    public BlDoesNotExistException(string? message, Exception? innerException) : base(message, innerException) { }
}

public class BlValueNotExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlValueNotExistException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlValueNotExistException(string? message) : base(message) { }
    public BlValueNotExistException() { }
    public BlValueNotExistException(string? message, Exception? innerException) : base(message, innerException) { }

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

    public BlAlreadyExistException() { }

    public BlAlreadyExistException(string? message, Exception? innerException) : base(message, innerException) { }
}


/// <summary>
/// Exception thrown when an element can not be deleted.
/// </summary>
[Serializable]
public class CanNotDeletedException : Exception
{
    public CanNotDeletedException(string? message) : base(message) { }
    public CanNotDeletedException() { }
    public CanNotDeletedException(string? message, Exception? innerException) : base(message, innerException) { }
}

/// <summary>
///  exception that alert the user about an error about the xml file
/// </summary>
[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlXMLFileLoadCreateException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException() { }
    public BlXMLFileLoadCreateException(string? message, Exception? innerException) : base(message, innerException) { }
}

/// <summary>
/// exception that alert using null object.
/// </summary>
[Serializable]
public class BlNullPropertyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlNullPropertyException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlNullPropertyException(string? message) : base(message) { }
    public BlNullPropertyException() { }
    public BlNullPropertyException(string? message, Exception? innerException) : base(message, innerException) { }
}

public class BlDateProblemException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlDateProblemException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlDateProblemException(string? message) : base(message) { }
    public BlDateProblemException() { }
    public BlDateProblemException(string? message, Exception? innerException) : base(message, innerException) { }
}
public class BlValueInvalidException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlDateProblemException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BlValueInvalidException(string? message) : base(message) { }
    public BlValueInvalidException() { }
    public BlValueInvalidException(string? message, Exception? innerException) : base(message, innerException) { }
}