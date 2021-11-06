using DisneyDown.Common.Util.Kit;
using System;

namespace DisneyDown.Common.API.Structures
{
    public class MediaInformation
    {
        public string ManifestUrl { get; set; } = @"";
        public string MediaTitle { get; set; } = @"";
        public string MediaDescription { get; set; } = @"";
        public string MediaReleaseYear { get; set; } = @"";

        public string CreateFileName()
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(MediaTitle))
                {
                    //replace colons with dashes
                    var newTitle = MediaTitle.Replace(": ", " - ").ReplaceInvalidChars();

                    //file name
                    var fileName = string.IsNullOrWhiteSpace(MediaReleaseYear)
                        ? $@"{newTitle}.mkv"
                        : $@"{newTitle} ({MediaReleaseYear}).mkv";

                    //return the new file name
                    return fileName;
                }
            }
            catch (Exception ex)
            {
                //alert user
                ConsoleWriters.ConsoleWriteDebug($@"Error occurred whilst creating a file name for a Disney+ object: {ex.Message}");
            }

            //default return
            return @"decryptedDisney.mkv";
        }
    }
}