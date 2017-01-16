namespace TeamNectarineScheduleManager.Table
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ExtensionMethods;

    public abstract class Table
    {
        private StringBuilder tableHead;
        private StringBuilder tableBody;
        private Dictionary<string, string> activities;
        private string border;
        private string whitespace;
        private string[] headers;
        private int numberOfColumns;
        private int columnHeight;
        private int columnWidth;

        protected Table(int columnWidth, int numberOfColumns, Dictionary<string, string> activities, string[] headers)
        {
            this.tableHead = new StringBuilder();
            this.tableBody = new StringBuilder();
            this.activities = activities;
            this.border = new string('═', columnWidth);
            this.whitespace = new string(' ', columnWidth);
            this.headers = headers;
            this.numberOfColumns = numberOfColumns;
            this.columnHeight = activities.Count * 2;
            this.columnWidth = columnWidth;
        }

        public void FillAndDraw()
        {
            tableHead.Append("╔");
            for (int i = 0; i < numberOfColumns - 1; i++)
            {
                tableHead.Append(border + "╦");
            }

            tableHead.AppendLine(border + "╗");

            for (int i = 0; i < numberOfColumns; i++)
            {
                tableHead.Append("║" + headers[i].Center(columnWidth));
            }

            tableHead.AppendLine("║");

            tableHead.Append("╠");
            for (int i = 0; i < numberOfColumns - 1; i++)
            {
                tableHead.Append(border + "╬");
            }

            tableHead.AppendLine(border + "╣");

            for (int i = 0; i < columnHeight; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    tableBody.Append("║" + whitespace);
                }

                tableBody.AppendLine("║");
            }

            tableBody.Append("╚");
            for (int i = 0; i < numberOfColumns - 1; i++)
            {
                tableBody.Append(border + "╩");
            }

            tableBody.AppendLine(border + "╝");

            var table = tableHead.Append(tableBody);
            Console.WriteLine();
            Console.Write(table);

            var indent = 2;

            // save original cursor position before changing it
            var origRow = Console.CursorTop;
            var origCol = Console.CursorLeft;

            // move cursor to the top left of monday column
            Console.SetCursorPosition(origCol, origRow - columnHeight - 1);

            for (int i = 0, space = 0; i < numberOfColumns; i++, space += columnWidth + 1)
            {
                foreach (var activity in activities)
                {
                    Console.SetCursorPosition(origCol + indent + space, Console.CursorTop);
                    Console.WriteLine(activity.Key);
                    Console.SetCursorPosition(origCol + indent + space, Console.CursorTop);
                    Console.WriteLine(activity.Value);
                }

                // return to the top of the current column
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - columnHeight);
            }

            // restore the original cursor position
            Console.SetCursorPosition(origCol, origRow);
        }
    }
}