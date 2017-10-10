using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;
using static System.Console;

namespace WhereIsBigfoot
{
    public class GameSettings
    {
        int typeSpeed, foreground, background;

        public GameSettings(int typeSpeed, int foreground, int background)
        {
            this.typeSpeed = typeSpeed;
            this.foreground = foreground;
            this.background = background;
        }

        public int TypeSpeed
        {
            get { return this.typeSpeed; }
        }

        public int Foreground
        {
            get { return this.foreground; }
        }

        public int Background
        {
            get { return this.background; }
        }

        public void TypeLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(TypeSpeedConverter()); // Sleep for 15 milliseconds between characters.
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public int TypeSpeedConverter()
        {
            int milliseconds;

            switch (typeSpeed)
            {
                case 1:
                    milliseconds = 60;
                    break;
                case 2:
                    milliseconds = 55;
                    break;
                case 3:
                    milliseconds = 50;
                    break;
                case 4:
                    milliseconds = 45;
                    break;
                case 5:
                    milliseconds = 40;
                    break;
                case 6:
                    milliseconds = 35;
                    break;
                case 7:
                    milliseconds = 30;
                    break;
                case 8:
                    milliseconds = 25;
                    break;
                case 9:
                    milliseconds = 20;
                    break;
                case 10:
                    milliseconds = 15;
                    break;
                default:
                    milliseconds = 30;
                    break;
            }

            return milliseconds;
        }

        public ConsoleColor ForegroundConverter()
        {
            var consoleColor = new ConsoleColor();

            switch (foreground)
            {
                case 1:
                    consoleColor = ConsoleColor.Black;
                    break;
                case 2:
                    consoleColor = ConsoleColor.Gray;
                    break;
                case 3:
                    consoleColor = ConsoleColor.Blue;
                    break;
                case 4:
                    consoleColor = ConsoleColor.Green;
                    break;
                case 5:
                    consoleColor = ConsoleColor.Cyan;
                    break;
                case 6:
                    consoleColor = ConsoleColor.Red;
                    break;
                case 7:
                    consoleColor = ConsoleColor.Magenta;
                    break;
                case 8:
                    consoleColor = ConsoleColor.Yellow;
                    break;
                case 9:
                    consoleColor = ConsoleColor.White;
                    break;
                default:
                    consoleColor = ConsoleColor.White;
                    break;
            }
            return Console.ForegroundColor = consoleColor;
        }

        public ConsoleColor BackgroundConverter()
        {
            var consoleColor = new ConsoleColor();

            switch (foreground)
            {
                case 1:
                    consoleColor = ConsoleColor.Black;
                    break;
                case 2:
                    consoleColor = ConsoleColor.Gray;
                    break;
                case 3:
                    consoleColor = ConsoleColor.Blue;
                    break;
                case 4:
                    consoleColor = ConsoleColor.Green;
                    break;
                case 5:
                    consoleColor = ConsoleColor.Cyan;
                    break;
                case 6:
                    consoleColor = ConsoleColor.Red;
                    break;
                case 7:
                    consoleColor = ConsoleColor.Magenta;
                    break;
                case 8:
                    consoleColor = ConsoleColor.Yellow;
                    break;
                case 9:
                    consoleColor = ConsoleColor.White;
                    break;
                default:
                    consoleColor = ConsoleColor.White;
                    break;
            }
            return Console.BackgroundColor = consoleColor;
        }

        

        public static int[] GetGameSettings()
        {
            int typeSpeed;
            int foreground;
            int background;

            do
            {
                typeSpeed = Convert.ToInt16(Game.GetInput("Choose a gamespeed from 1-10: "));
            } while (typeSpeed < 1 || typeSpeed > 10);

            do
            {
                foreground = Convert.ToInt16(Game.GetInput("Choose a foreground color by entering the corresponding number: \n Black = 1 Gray = 2 Blue = 3 Green = 4 Cyan = 5 Red = 6 Magenta = 7 Yellow = 8 White = 9"));
            } while (foreground < 1 || foreground > 9);

            do
            {
                background = Convert.ToInt16(Game.GetInput("Choose a background color by entering the corresponding number: \n Black = 1 Gray = 2 Blue = 3 Green = 4 Cyan = 5 Red = 6 Magenta = 7 Yellow = 8 White = 9"));
            } while (background < 1 || background > 9);

            int[] settings = { typeSpeed, foreground, background };

            Console.WriteLine("Boom! your settings are implemented");
            return settings;
        }
    }
}
