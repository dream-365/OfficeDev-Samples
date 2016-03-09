using System;

namespace Console_with_O365
{
    class Util
    {
        public static void ListTimeZones()
        {
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
                Console.WriteLine("{0} - {1}", z.BaseUtcOffset, z.Id);
        }
    }
}
