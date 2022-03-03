using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraveNewWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int coordinatePlayerX;
            int coordinatePlayerY;
            int score = 0;
            int maxScore;
            char[,] map = ReadMap("map", out coordinatePlayerX, out coordinatePlayerY, out maxScore);
            char symbolPlayer = '$';
            bool isOpen = true;


            Console.CursorVisible = false;
            DrawMap(map);
            while (isOpen == true)
            {
                Console.SetCursorPosition(coordinatePlayerY, coordinatePlayerX);
                Console.Write(symbolPlayer);
                Console.SetCursorPosition(20, 2);
                Console.WriteLine($"Набранные очки: {score}/{maxScore}");

                MovePlayer(map, ref coordinatePlayerX, ref coordinatePlayerY, symbolPlayer);
                CollectPoint(map, coordinatePlayerX, coordinatePlayerY, ref score, ref maxScore, ref isOpen);
            }
        }

        static void MovePlayer(char[,] map, ref int coordinatePlayerX,ref int coordinatePlayerY, char symbolPlayer = '$')
        {
            int playerDirectionX = 0;
            int playerDirectionY = 0;
            ConsoleKeyInfo userInput = Console.ReadKey(true);

            switch (userInput.Key)
            {
                case ConsoleKey.W:
                    playerDirectionX = -1;
                    playerDirectionY = 0;
                    break;
                case ConsoleKey.D:
                    playerDirectionX = 0;
                    playerDirectionY = 1;
                    break;
                case ConsoleKey.S:
                    playerDirectionX = 1;
                    playerDirectionY = 0;
                    break;
                case ConsoleKey.A:
                    playerDirectionX = 0;
                    playerDirectionY = -1;
                    break;
            }

            if (map[coordinatePlayerX + playerDirectionX, coordinatePlayerY + playerDirectionY] != '#')
            {
                Console.SetCursorPosition(coordinatePlayerY, coordinatePlayerX);
                Console.Write(' ');

                coordinatePlayerX += playerDirectionX;
                coordinatePlayerY += playerDirectionY;

                Console.SetCursorPosition(coordinatePlayerY, coordinatePlayerX);
                Console.Write(symbolPlayer);
            }
        }

        static void CollectPoint(char[,] map, int coordinatePlayerX, int coordinatePlayerY, ref int score, ref int maxScore, ref bool isOpen)
        {
            if (map[coordinatePlayerX, coordinatePlayerY] == '*')
            {
                map[coordinatePlayerX, coordinatePlayerY] = ' ';
                score++;
            }
            if (score == maxScore)
            {
                Console.Clear();
                Console.WriteLine("Вы собрали все очки!");
                isOpen = false;
            }
        }

        static char[,] ReadMap(string pathToMap, out int coordinatePlayerX, out int coordinatePlayerY, out int maxScore)
        {
            string[] fileMap = File.ReadAllLines($"Maps/{pathToMap}.txt");
            char[,] map = new char[fileMap.Length, fileMap[0].Length];
            coordinatePlayerX = 0;
            coordinatePlayerY = 0;
            maxScore = 0;

            for (int currentRow = 0; currentRow < fileMap.Length; currentRow++)
            {
                for (int currentColumns = 0; currentColumns < fileMap[0].Length; currentColumns++)
                {
                    map[currentRow, currentColumns] = fileMap[currentRow][currentColumns];
                    
                    if (fileMap[currentRow][currentColumns] == '$')
                    {
                        coordinatePlayerX = currentRow;
                        coordinatePlayerY = currentColumns;
                    }

                    if (fileMap[currentRow][currentColumns] == '*')
                        maxScore++;
                }
            }

            return map;
        }

        static void DrawMap(char[,] map)
        {
            for (int currentRows = 0; currentRows < map.GetLength(0); currentRows++)
            {
                for (int currentColumns = 0; currentColumns < map.GetLength(1); currentColumns++)
                {
                    Console.Write(map[currentRows, currentColumns]);
                }
                Console.WriteLine();
            }
        }
    }
}