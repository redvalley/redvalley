namespace RedValley.Extensions
{
    /// <summary>
    /// Defines some usefully utility extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether a string is null or empty.
        /// </summary>
        /// <param name="s">The string that should be checked.</param>
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Determines whether a string is not null or empty.
        /// </summary>
        /// <param name="s">The string that should be checked.</param>
        public static bool IsNotEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Converts the value of objects to strings based on the formats specified and inserts them into another string. 
        /// This method only forwards the formatting to the original <see cref="string.Format(string,object[])"/> method.
        /// It makes it possible to format any string directly e.g. "Hello {0}".Format("Yellow").
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static string F(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
