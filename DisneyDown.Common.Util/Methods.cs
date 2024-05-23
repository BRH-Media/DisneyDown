using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DisneyDown.Common.Util
{
    /// <summary>
    /// Generic methods that are commonly accessed
    /// </summary>
    public static class Methods
    {
        /// <summary>
        /// Base URL from string; gets the URL 'directory' of a file resource
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetBaseUrl(string url)
            => new Uri(new Uri(url), ".").OriginalString;

        /// <summary>
        /// Check if all files in a list exist on the file system
        /// </summary>
        /// <param name="inputFiles"></param>
        /// <returns></returns>
        public static bool AllExistInList(IEnumerable<string> inputFiles)
        {
            try
            {
                //return true only if every single file in the list exists
                return inputFiles.All(File.Exists);
            }
            catch
            {
                //nothing
            }

            //default
            return false;
        }
    }
}