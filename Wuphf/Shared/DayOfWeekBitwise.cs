using System;
namespace Wuphf.Shared
{
    public enum DayOfWeekBitwise
    {
        None = 0
        , Sunday = 1
        , Monday = 2
        , Tuesday = 4
        , Wednesday = 8
        , Thursday = 16
        , Friday = 32
        , Saturday = 64
    }
    public static class DayOfWeekBitwiseExtensions
    {
        public static DayOfWeekBitwise ToBitwise(this DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return DayOfWeekBitwise.Sunday;
                case DayOfWeek.Monday:
                    return DayOfWeekBitwise.Monday;
                case DayOfWeek.Tuesday:
                    return DayOfWeekBitwise.Tuesday;
                case DayOfWeek.Wednesday:
                    return DayOfWeekBitwise.Wednesday;
                case DayOfWeek.Thursday:
                    return DayOfWeekBitwise.Thursday;
                case DayOfWeek.Friday:
                    return DayOfWeekBitwise.Friday;
                case DayOfWeek.Saturday:
                    return DayOfWeekBitwise.Saturday;
            }
            return DayOfWeekBitwise.None;
        }
    }
}
