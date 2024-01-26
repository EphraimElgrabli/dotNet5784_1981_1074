using System.Collections;
using System.Reflection;

namespace BO;

static class Tools
{
    public static string ToStringProperty<T>(this T entity)
    {

        string st = "";
        foreach (PropertyInfo item in entity.GetType().GetProperties())
        {
            var enumerable = item.GetValue(entity, null);

            if ((enumerable is IEnumerable) && !(enumerable is string))
            {
                IEnumerable? en = enumerable as IEnumerable;
                foreach (var _item in en)
                {
                    st += _item.ToStringProperty();

                }
            }
            else
            {
                st += "\n" + item.Name +
           ": " + item.GetValue(entity, null);
            }
        }
        return st;
    }
}
