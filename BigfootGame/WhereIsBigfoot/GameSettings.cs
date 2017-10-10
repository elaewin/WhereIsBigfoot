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

        public static int[] GetGameSettings()
        {
            string speedChosen;
            int typeSpeed;
            string foreChosen;
            int foreground;
            string backChosen;
			int background;

            do
            {
                speedChosen = Game.GetInput("Choose a gamespeed from 1 (slow) to 10 (fast).\n (Default speed is 10.)\n> ");
                if (speedChosen == "")
                    speedChosen = "8";
                typeSpeed = Convert.ToInt16(speedChosen);
            } while (typeSpeed < 1 || typeSpeed > 10);

            do
            {
                foreChosen = Game.GetInput("\nChoose a text color by entering the corresponding number. Default background color is black. \nBlack = 1 Gray = 2 Blue = 3 Green = 4 Cyan = 5 Red = 6 Magenta = 7 Yellow = 8 White = 9 \n> ");
                if (foreChosen == "")
                    foreChosen = "4";
                foreground = Convert.ToInt16(foreChosen);
            } while (foreground < 1 || foreground > 9);

            do
            {
                backChosen = Game.GetInput("\nChoose a background color by entering the corresponding number, cannot be the same as text color. Default text color is green.\nBlack = 1 Gray = 2 Blue = 3 Green = 4 Cyan = 5 Red = 6 Magenta = 7 Yellow = 8 White = 9 \n> ");
                if (backChosen == "")
                    backChosen = "1";
                background = Convert.ToInt16(backChosen);
            } while (background < 1 || background > 9 || foreground == background);

            int[] settings = { typeSpeed, foreground, background };

            Console.WriteLine("\nYour settings have been implemented. Boom!");
            return settings;
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
                    milliseconds = 15;
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
                    consoleColor = ConsoleColor.Green;
                    break;
            }
            return consoleColor;
        }

        public ConsoleColor BackgroundConverter()
        {
            var consoleColor = new ConsoleColor();

            switch (background)
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
                    consoleColor = ConsoleColor.Black;
                    break;
            }
            return consoleColor;
        }

        

    }
}
