namespace DO;

[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

[Serializable]
public class DalAlreadyExistException : Exception
{
    public DalAlreadyExistException(string? message) : base(message) { }
}
[Serializable]
public class CanNotDeletedException : Exception
{
    public CanNotDeletedException(string? message) : base(message) { }
}


