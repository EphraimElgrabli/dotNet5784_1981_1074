﻿namespace BlImplementation;
using BlApi;
using DO;
internal class Bl : IBl
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private static DateTime s_TimeNow = DateTime.Now;
    public IUser User => new UserImplementation(this);
    public ITask Task => new TaskImplemtation(this);
    public IClock Clock => new ClockImplementation(this);
    public void  DeleteAll(){
        _dal.Task.DeleteAll();
        _dal.User.DeleteAll();
        _dal.Dependency.DeleteAll();
    }
    public DateTime DateNow
    {
        get { return s_TimeNow; }
        private set { s_TimeNow = value; }
    }
        public void PrometeSecond()
    {
            s_TimeNow = s_TimeNow.AddSeconds(1);
        }
    public void PrometeMinute()
    {
            s_TimeNow = s_TimeNow.AddMinutes(1);
    }
    public void PrometeHour()
    {
            s_TimeNow = s_TimeNow.AddHours(1);
    }
    public void PrometeDay()
    {
            s_TimeNow = s_TimeNow.AddDays(1);
    }
    public void PrometeMonth()
    {
            s_TimeNow = s_TimeNow.AddMonths(1);
    }
    public void PrometeYear()
    {
            s_TimeNow = s_TimeNow.AddYears(1);
    }
    public void initalizeTime()
    {
            s_TimeNow = DateTime.Now.Date;
    }

    
}
