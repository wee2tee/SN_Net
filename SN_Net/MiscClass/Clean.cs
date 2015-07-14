using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.MiscClass
{
    public static class Clean
    {
        public static string cleanString(this string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }
}
