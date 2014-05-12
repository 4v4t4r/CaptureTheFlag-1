using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptureTheFlag
{
    public static class DateTimeFormatInfoExtensions
    {
        public static DateTimeFormatInfo CaptureTheFlagFormatInfo(this DateTimeFormatInfo @this)
        {
            return new DateTimeFormatInfo()
                {
                    FullDateTimePattern = "yyyy-MM-ddTHH:mm:ssZ"
                };
        }
    }
}
