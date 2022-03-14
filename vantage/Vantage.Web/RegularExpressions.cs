using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vantage.Web
{
    public static class RegularExpressions
    {
        public const string Hyperlink = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
    }
}
