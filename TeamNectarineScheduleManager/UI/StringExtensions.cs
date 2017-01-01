namespace TeamNectarineScheduleManager.UserInterface
{
    public static class StringExtensions
    {
        /// <summary>
        /// Extension method to center text (pad both left and right).
        /// </summary>
        public static string PadBoth(this string str, int totalWidth, char paddingChar = ' ')
        {
            int padding = totalWidth - str.Length;
            int padLeft = padding / 2 + str.Length;
            return str.PadLeft(padLeft, paddingChar).PadRight(totalWidth, paddingChar);
        }
    }
}
