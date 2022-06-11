using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreServices
{
    public static class MethodExtensions
    {
        public static bool ToBoolean(this object o)
        {
            try
            {
                return Convert.ToBoolean(o);
            }
            catch
            {
                return false;
            }
        }
        public static long ToLong(this object o)
        {
            try
            {
                return Convert.ToInt64(o);
            }
            catch
            {
                return 0;
            }
        }
    }
}
