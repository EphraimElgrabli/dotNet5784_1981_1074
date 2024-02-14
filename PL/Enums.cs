using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;
internal class UserLevelCollection : IEnumerable
{
    static readonly IEnumerable<BO.UserLevel> s_enums =
(Enum.GetValues(typeof(BO.UserLevel)) as IEnumerable<BO.UserLevel>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

