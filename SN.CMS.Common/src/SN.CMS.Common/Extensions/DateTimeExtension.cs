using System;

namespace SN.CMS.Common
{
    public static class DateTimeExtension
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            var epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
            var unixTicks = new TimeSpan(dateTime.Ticks) - epochTicks;
            var unixTime = (long)unixTicks.TotalSeconds;
            return unixTime;
        }

        public static string ToString(this DateTime dateTime, bool onlyDate = false)
        {
            var result = string.Empty;
            if (!onlyDate)
                result = dateTime.ToString("yyyy-MM-dd HH:mm:sss");
            else
                result = dateTime.ToString("yyyy-MM-dd");
            return result;
        }
    }
}
