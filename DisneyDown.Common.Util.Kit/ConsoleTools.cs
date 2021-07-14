using System;

namespace DisneyDown.Common.Util.Kit
{
    public static class ConsoleTools
    {
        public static void PauseExecution()
        {
            try
            {
                //message
                ConsoleWriters.Write(@"Press any key to continue...");

                //halt execution
                Console.ReadKey();

                //new line
                ConsoleWriters.Write("\n");
            }
            catch (Exception ex)
            {
                //notify the user
                ConsoleWriters.ConsoleWriteError($@"Pause execution error: {ex.Message}");
            }
        }
    }
}