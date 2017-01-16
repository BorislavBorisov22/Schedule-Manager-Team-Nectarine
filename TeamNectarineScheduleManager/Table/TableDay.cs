namespace TeamNectarineScheduleManager.Table
{
    using System.Collections.Generic;

    public class TableDay : Table
    {
        private const int NumberOfColumns = 1;
        private static readonly string[] Header = { "TODAY" };

        public TableDay(Dictionary<string, string> activities, int columnWidth = 20)
            : base(
                  columnWidth,
                  NumberOfColumns,
                  activities,
                  Header)
        {
        }
    }
}
