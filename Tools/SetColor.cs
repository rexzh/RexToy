using System;
using System.Runtime.InteropServices;

namespace SetColor
{
    [Flags]
    public enum ConsoleColor
    {
        Black = 0x0000,
        Blue = 0x0001,
        Green = 0x0002,
        Cyan = 0x0003,
        Red = 0x0004,
        Magenta = 0x0005,
        Yellow = 0x0006,
        Grey = 0x0007,
        White = 0x0008
    }

    public class ConsoleColorController
    {
        const int STD_INPUT_HANDLE = -10;
        const int STD_OUTPUT_HANDLE = -11;
        const int STD_ERROR_HANDLE = -12;
        [DllImportAttribute("Kernel32.dll")]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImportAttribute("Kernel32.dll")]
        private static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, int wAttributes);

        private ConsoleColorController() { }

        public static bool SetForeGroundColour()
        {
            return SetForeGroundColour(ConsoleColor.Grey, false);
        }

        public static bool SetForeGroundColour(ConsoleColor foreGroundColour)
        {
            return SetForeGroundColour(foreGroundColour, true);
        }

        public static bool SetForeGroundColour(ConsoleColor foreGroundColour, bool brightColours)
        {
            IntPtr nConsole = GetStdHandle(STD_OUTPUT_HANDLE);
            int colourMap;

            if (brightColours)
                colourMap = (int)foreGroundColour | (int)ConsoleColor.White;
            else
                colourMap = (int)foreGroundColour;

            return SetConsoleTextAttribute(nConsole, colourMap);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    ConsoleColor color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), args[0], true);
                    ConsoleColorController.SetForeGroundColour(color, false);
                }
                catch
                {
                    Console.WriteLine("Usage: SetColor [color]");
                    Console.WriteLine("color = Black| Blue | Green | Cyan | Red | Magenta | Yellow | Grey | White");
                }
            }
            else
                ConsoleColorController.SetForeGroundColour();
            Console.Write("\n");
        }
    }
}
