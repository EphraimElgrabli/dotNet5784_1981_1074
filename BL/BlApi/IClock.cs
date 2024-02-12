using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IClock
    {
        public DateTime GetStartProject();

        public DateTime SetStartProject(DateTime dateTime);
    }
}
