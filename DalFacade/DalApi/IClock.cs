﻿
namespace DalApi;

public  interface IClock
{
    public DateTime? GetStartProject();
    public DateTime? GetEndProject();

    public DateTime? SetStartProject();
    public DateTime? SetEndProject();
    public void resetTimeProject();
}
