using DisneyDown.Common.Util.Kit.Enums;
using System;

// ReSharper disable InvertIf
// ReSharper disable LocalizableElement
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global

namespace DisneyDown.Common.Util.Kit
{
    public static class ConsoleWriters
    {
        public static ConsoleWriterColouringMode ColouringMode { get; set; } = ConsoleWriterColouringMode.ForeColour;
        public static bool DisableAllOutput { get; set; } = false;
        public static bool DebugMode { get; set; } = false;

        public static void ClearConsole(bool clear = true)
        {
            if (clear)
                Console.Clear();
        }

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
            if (!DisableAllOutput)
            {
                Console.ForegroundColor = c;
                Console.WriteLine(text);
                Console.ForegroundColor = after;
            }
        }

        public static void Write(string text, ConsoleColor c = ConsoleColor.White,
            ConsoleColor after = ConsoleColor.White)
        {
            if (!DisableAllOutput)
            {
                Console.ForegroundColor = c;
                Console.Write(text);
                Console.ForegroundColor = after;
            }
        }

        public static void ConsoleWriteLineColouredChar(string letters, char c, ConsoleColor backColor,
            ConsoleColor foreColor = ConsoleColor.White)
        {
            if (!DisableAllOutput)
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
        }

        public static void ConsoleWriteDebug(string msg, bool writeDebugNotation = true)
        {
            if (!DisableAllOutput && DebugMode)
            {
                ConsoleColourSet.ConsoleColourOcr(ColouringMode);
                if (writeDebugNotation)
                    Console.WriteLine("[d] " + msg);
                else
                    Console.WriteLine(msg);
                ConsoleColourSet.ConsoleColourDef();
            }
        }

        public static void ConsoleWriteError(string msg, bool writeErrorNotation = true)
        {
            if (!DisableAllOutput)
            {
                ConsoleColourSet.ConsoleColourRed(ColouringMode);
                if (writeErrorNotation)
                    Console.WriteLine("[!] " + msg);
                else
                    Console.WriteLine(msg);
                ConsoleColourSet.ConsoleColourDef();
            }
        }

        public static void ConsoleWriteQuestion(string msg, bool writeQuestionNotation = true)
        {
            if (!DisableAllOutput)
            {
                ConsoleColourSet.ConsoleColourBlu(ColouringMode);
                if (writeQuestionNotation)
                    Console.WriteLine("[?] " + msg);
                else
                    Console.WriteLine(msg);
                ConsoleColourSet.ConsoleColourDef();
            }
        }

        public static void ConsoleWriteSuccess(string msg, bool writeSuccessNotation = true)
        {
            if (!DisableAllOutput)
            {
                ConsoleColourSet.ConsoleColourGrn(ColouringMode);
                if (writeSuccessNotation)
                    Console.WriteLine("[i] " + msg);
                else
                    Console.WriteLine(msg);
                ConsoleColourSet.ConsoleColourDef();
            }
        }

        public static void ConsoleWriteInfo(string msg, bool writeInfoNotation = true)
        {
            if (!DisableAllOutput)
            {
                ConsoleColourSet.ConsoleColourCyn(ColouringMode);
                if (writeInfoNotation)
                    Console.WriteLine("[i] " + msg);
                else
                    Console.WriteLine(msg);
                ConsoleColourSet.ConsoleColourDef();
            }
        }

        public static void ConsoleWriteWarning(string msg, bool writeWarningNotation = true)
        {
            if (!DisableAllOutput)
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
}