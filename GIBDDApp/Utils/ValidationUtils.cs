using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GIBDDApp.Utils
{
    public static class ValidationUtils
    {
        public static readonly Regex _regex = new Regex("[^0-9.-]+");
        public static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
