using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Utilities
{
    static public class TimeConverter
    {
        static public long GetCurrentUnixTime()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        static public long DateTimeToUnix(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }
        static public DateTime UnixToDateTime(long unix)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unix).DateTime;
        }
    }
}
