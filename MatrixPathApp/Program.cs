using System;

namespace MatrixPathApp
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[,] matrix = new byte[3,3];
            bool[,] state = new bool[,] 
            { 
                {false, false, false},
                {false, false, false},
                {false, false, false}
            };
            int step = 0;
            var start = "";

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("To get started, enter any characters and press enter.");
            Console.WriteLine("Send a blank line to regenerate.");
            Console.ResetColor();

            do
            {
                byte[] arr = new byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                Shuffle(arr);
                byte k = 0;
                for (byte i = 0; i < 3; i++)
                {
                    for (byte j = 0; j < 3; j++)
                    {
                        matrix[i, j] = arr[k++];
                    }
                }

                ShowResult(matrix, state, step);

                Console.WriteLine("Go? ");
                start = Console.ReadLine().Trim();
            } while (start == "");

            var result = "";
            byte x, y, fx, fy;
            x = 0;
            y = 0;

            //Find start position
            var localMax = matrix[x,y];
            for (byte i = 0; i < 3; i++)
            {
                for (byte j = 0; j < 3; j++)
                {
                    if (matrix[i, j] > localMax
                        && !(i == 0 && j == 1)
                        && !(i == 1 && j == 0)
                        && !(i == 1 && j == 2)
                        && !(i == 2 && j == 1))
                    {
                        localMax = matrix[i, j];
                        x = i;
                        y = j;
                    }
                }
            }
            state[x, y] = true;
            fx = x; fy = y;
            result += matrix[x, y];
            ShowResult(matrix, state, ++step);
            Console.WriteLine($"Result: {result}");
            Console.ReadLine();

            while (result.Length < matrix.LongLength && step < 11)
            {
                if (x == 1 && y == 1)
                {
                    x = fx;
                    y = fy;
                }
                byte next = 0;
                byte tmpX = x;
                byte tmpY = y;

                if (x > 0)
                {
                    if (!state[x - 1, y] && next < matrix[x - 1, y])
                    {
                        next = matrix[x - 1, y];
                        tmpX = (byte)(x - 1);
                        tmpY = y;
                    }
                }
                if (x < 2)
                {
                    if (!state[x + 1, y] && next < matrix[x + 1, y])
                    {
                        next = matrix[x + 1, y];
                        tmpX = (byte)(x + 1);
                        tmpY = y;
                    }
                }
                if (y < 2)
                {
                    if (!state[x, y + 1] && next < matrix[x, y + 1])
                    {
                        next = matrix[x, y + 1];
                        tmpX = x;
                        tmpY = (byte)(y + 1);
                    }
                }
                if (y > 0)
                {
                    if (!state[x, y - 1] && next < matrix[x, y - 1])
                    {
                        next = matrix[x, y - 1];
                        tmpX = x;
                        tmpY = (byte)(y - 1);
                    }
                }
                if (next > 0)
                {
                    x = tmpX;
                    y = tmpY;
                    result += matrix[x, y].ToString();
                    state[x, y] = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"It's end!");
                    Console.ResetColor();
                }

                ShowResult(matrix, state, ++step);
                Console.WriteLine($"Result: {result}");
                Console.ReadLine();
            }
        }

        public static void Shuffle(byte[] arr)
        {
            Random rand = new Random();

            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                byte tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
        }

        public static void ShowResult (byte[,] matrix, bool[,] state, int step)
        {
            switch (step)
            {
                case 0:
                    {
                        Console.WriteLine($"Initial data");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine($"First step");
                        break;
                    }
                case 9:
                    {
                        Console.WriteLine($"Final step");
                        break;
                    }
                default:
                    {
                        Console.WriteLine($"Step {step}.");
                        break;
                    }
            };

            for (byte i = 0; i < 3; i++)
            {
                for (byte j = 0; j < 3; j++)
                {
                    if (state[i, j])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write($" {matrix[i, j]} ");
                    if (j == 2)
                    {
                        Console.WriteLine();
                    }
                    Console.ResetColor();
                }
            }
        }
    }
}
