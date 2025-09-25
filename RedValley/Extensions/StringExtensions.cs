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
        public static bool IsEmpty(this string? s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Determines whether a string is not null or empty.
        /// </summary>
        /// <param name="s">The string that should be checked.</param>
        public static bool IsNotEmpty(this string? s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Determines whether all strings are contained within the current string.
        /// </summary>
        /// <param name="s">The string that should be checked.</param>
        /// <param name="stringsToMatch">The string that should be contained within the current string</param>
        public static bool ContainsAll(this string? s,params IEnumerable<string> stringsToMatch)
        {
            if (s == null)
            {
                return false;
            }
            
            return !s.IsEmpty() && stringsToMatch.All(stringToMatch => s.Contains(stringToMatch, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Determines whether any string is contained within the current string.
        /// </summary>
        /// <param name="s">The string that should be checked.</param>
        /// <param name="stringsToMatch">The string that should be contained within the current string</param>
        public static bool ContainsAny(this string? s, params IEnumerable<string> stringsToMatch)
        {
            if (s == null)
            {
                return false;
            }
            
            return !s.IsEmpty() && stringsToMatch.Any(stringToMatch => s.Contains(stringToMatch, StringComparison.InvariantCultureIgnoreCase));
        }
        
        /// <summary>
        /// Determines whether any string is contained within the current string.
        /// </summary>
        /// <param name="s">The string that should be checked.</param>
        /// <param name="stringListToMatch">The string list that should be and combined and its entries contained within the current string</param>
        public static bool ContainsAnyAndCombined(this string? s, IEnumerable<IEnumerable<string>> stringListToMatch)
        {
            if (s == null)
            {
                return false;
            }

            if (s.IsEmpty())
            {
                return false;
            }
            
            List<string> alReadyMatchedToSkip = new List<string>();
            
            foreach (IEnumerable<string> stringsToMatch in stringListToMatch)
            {
                var currentStringsToMatch = stringsToMatch.Except(alReadyMatchedToSkip);
                
                if (s.ContainsAny(currentStringsToMatch))
                {
                    var matchedString = stringsToMatch.FirstOrDefault(stringToCheck => s.Contains(stringToCheck, StringComparison.InvariantCultureIgnoreCase));
                    if (matchedString != null)
                    {
                        alReadyMatchedToSkip.Add(matchedString);    
                    }
                    
                }
                else
                {
                    return false;
                }
            }

            return true;
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
