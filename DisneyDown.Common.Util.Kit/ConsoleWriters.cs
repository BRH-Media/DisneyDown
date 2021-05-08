using DisneyDown.Common.Util.Kit.Enums;
using System;

// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global

namespace DisneyDown.Common.Util.Kit
{
    public static class ConsoleWriters
    {
        public static ConsoleWriterColouringMode ColouringMode { get; set; } = ConsoleWriterColouringMode.ForeColour;

        public static void Break()
            => Console.Write("\n");

        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public static void WriteProperty(string property, string value)
        {
            Write(property, ConsoleColor.Green);
            Write($" {value}\n", ConsoleColor.Gray);
        }

        public static void WriteLine(string text, ConsoleColor c = ConsoleColor.White,
            ConsoleColor after = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(text);
            Console.ForegroundColor = after;
        }

        public static void Write(string text, ConsoleColor c = ConsoleColor.White,
            ConsoleColor after = ConsoleColor.White)
        {
            Console.ForegroundColor = c;
            Console.Write(text);
            Console.ForegroundColor = after;
        }

        public static void ConsoleWriteLineColouredChar(string letters, char c, ConsoleColor backColor,
            ConsoleColor foreColor = ConsoleColor.White)
        {
            var array = letters.ToCharArray();

            foreach (var chr in array)
                if (chr == c)
                {
                    Console.ForegroundColor = foreColor;
                    Console.BackgroundColor = backColor;
                    Console.Write(chr);
                }
                else
                {
                    ConsoleColourSet.ConsoleColourDef();
                    Console.Write(chr);
                }

            ConsoleColourSet.ConsoleColourDef();
            Console.WriteLine();
        }

        public static void ConsoleWriteError(string msg, bool writeErrorNotation = true)
        {
            ConsoleColourSet.ConsoleColourRed(ColouringMode);
            if (writeErrorNotation)
                Console.WriteLine("[!] " + msg);
            else
                Console.WriteLine(msg);
            ConsoleColourSet.ConsoleColourDef();
        }

        public static void ClearConsole(bool clear = true)
        {
            if (clear) Console.Clear();
        }

        public static void ConsoleWriteSuccess(string msg, bool writeSuccessNotation = true)
        {
            ConsoleColourSet.ConsoleColourGrn(ColouringMode);
            if (writeSuccessNotation)
                Console.WriteLine("[i] " + msg);
            else
                Console.WriteLine(msg);
            ConsoleColourSet.ConsoleColourDef();
        }

        public static void ConsoleWriteInfo(string msg, bool writeInfoNotation = true)
        {
            ConsoleColourSet.ConsoleColourBlu(ColouringMode);
            if (writeInfoNotation)
                Console.WriteLine("[i] " + msg);
            else
                Console.WriteLine(msg);
            ConsoleColourSet.ConsoleColourDef();
        }

        public static void ConsoleWriteWarning(string msg, bool writeWarningNotation = true)
        {
            ConsoleColourSet.ConsoleColourOcr(ColouringMode);
            if (writeWarningNotation)
                Console.WriteLine("[!] " + msg);
            else
                Console.WriteLine(msg);
            ConsoleColourSet.ConsoleColourDef();
        }
    }
}