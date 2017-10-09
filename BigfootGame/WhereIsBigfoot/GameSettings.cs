using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string ForegroundConverter()
        {
            switch (foreground)
            {
                case 1:
                    return "black";
                case 2:
                    return "Gray";
                case 3:
                    return "Blue";
                case 4:
                    return "Green";
                case 5:
                    return "Cyan";
                case 6:
                    return "Red";
                case 7:
                    return "Magenta";
                case 8:
                    return "Yellow";
                case 9:
                    return "White";
                default:
                    return "White";
            }
        }

        public string BackgroundConverter()
        {
            switch (background)
            {
                case 1:
                    return "black";
                case 2:
                    return "Gray";
                case 3:
                    return "Blue";
                case 4:
                    return "Green";
                case 5:
                    return "Cyan";
                case 6:
                    return "Red";
                case 7:
                    return "Magenta";
                case 8:
                    return "Yellow";
                case 9:
                    return "White";
                default:
                    return "White";
            }
        }
    }
}
