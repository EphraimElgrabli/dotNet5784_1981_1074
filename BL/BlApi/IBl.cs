namespace BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IBl
{
    public IUser User { get; }
    public ITask Task { get; }
}
