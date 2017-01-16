namespace TeamNectarineScheduleManager.Table
{
    using System.Collections.Generic;

    public class TableWeek : Table
    {
        private const int NumberOfColumns = 7;
        private static readonly string[] Headers = { "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY", "SUNDAY" };

        public TableWeek(Dictionary<string, string> activities, int columnWidth = 20)
            : base(
                  columnWidth,
                  NumberOfColumns,
                  activities,
                  Headers)
        {
        }
    }
}
