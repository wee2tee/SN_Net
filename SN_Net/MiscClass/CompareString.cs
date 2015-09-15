using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SN_Net.MiscClass
{
    public class CompareStrings : IComparer<string>
    {
        // Because the class implements IComparer, it must define a 
        // Compare method. The method returns a signed integer that indicates 
        // whether s1 > s2 (return is greater than 0), s1 < s2 (return is negative),
        // or s1 equals s2 (return value is 0). This Compare method compares strings. 
        public int Compare(string s1, string s2)
        {
            return string.CompareOrdinal(s1, s2);
        }
    }
}
