﻿using BO;

namespace BlImplementation;
/// <summary>
/// Clock implementation class for project timing.
/// </summary>
internal class ClockImplementation : BlApi.IClock
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Gets the start date of the project.
    /// </summary>
    public DateTime? GetStartProject() => _dal.Clock.GetStartProject();

    /// <summary>
    /// Gets the end date of the project.
    /// </summary>
    public DateTime? GetEndProject() => _dal.Clock.GetEndProject();

    /// <summary>
    /// Sets the start date of the project.
    /// </summary>
    public DateTime? SetStartProject(DateTime startdateTime) => _dal.Clock.SetStartProject(startdateTime);

    /// <summary>
    /// Sets the end date of the project.
    /// </summary>
    public DateTime? SetEndProject(DateTime enddateTime) => _dal.Clock.SetEndProject(enddateTime);

    /// <summary>
    /// Gets the status of the project based on current date.
    /// </summary>
    public StatusProject GetStatusProject()
    {
        if (GetEndProject() < DateTime.Now)
            return StatusProject.Done;
        if (GetStartProject() < DateTime.Now)
            return StatusProject.InProgress;
        return StatusProject.UnStarted;
    }

    /// <summary>
    /// Resets the project timing.
    /// </summary>
    public void resetTimeProject() => _dal.Clock.resetTimeProject();
}