using System;

namespace TaskManagement.Domain.Common
{
    public static class DateTimeFormatter
    {
        public static DateTime GetISDTime(DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, TimeZoneInfo.Local.Id, "India Standard Time");
        }
    }
}
