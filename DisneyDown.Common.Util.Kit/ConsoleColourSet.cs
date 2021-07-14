using DisneyDown.Common.Util.Kit.Enums;
using System;

namespace DisneyDown.Common.Util.Kit
{
    public static class ConsoleColourSet
    {
        public static void ConsoleColourDef()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void ConsoleColourRed(ConsoleWriterColouringMode m)
        {
            switch (m)
            {
                case ConsoleWriterColouringMode.BackColour:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;

                    break;

                case ConsoleWriterColouringMode.ForeColour:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;

                    break;
            }
        }

        public static void ConsoleColourOcr(ConsoleWriterColouringMode m)
        {
            switch (m)
            {
                case ConsoleWriterColouringMode.BackColour:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;

                    break;

                case ConsoleWriterColouringMode.ForeColour:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.BackgroundColor = ConsoleColor.Black;

                    break;
            }
        }

        public static void ConsoleColourGrn(ConsoleWriterColouringMode m)
        {
            switch (m)
            {
                case ConsoleWriterColouringMode.BackColour:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;

                    break;

                case ConsoleWriterColouringMode.ForeColour:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.BackgroundColor = ConsoleColor.Black;

                    break;
            }
        }

        public static void ConsoleColourBlu(ConsoleWriterColouringMode m)
        {
            switch (m)
            {
                case ConsoleWriterColouringMode.BackColour:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;

                    break;

                case ConsoleWriterColouringMode.ForeColour:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.BackgroundColor = ConsoleColor.Black;

                    break;
            }
        }

        public static void ConsoleColourCyn(ConsoleWriterColouringMode m)
        {
            switch (m)
            {
                case ConsoleWriterColouringMode.BackColour:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Cyan;

                    break;

                case ConsoleWriterColouringMode.ForeColour:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.BackgroundColor = ConsoleColor.Black;

                    break;
            }
        }
    }
}