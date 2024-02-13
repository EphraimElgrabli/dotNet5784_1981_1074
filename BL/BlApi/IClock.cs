﻿namespace BlApi;

public interface IClock
{
    public DateTime? GetStartProject();
    public DateTime? GetEndProject();

    public DateTime? SetStartProject(DateTime dateTime);
    public DateTime? SetEndProject(DateTime dateTime);
    public void resetTimeProject();

    public BO.StatusProject GetStatusProject();

}
