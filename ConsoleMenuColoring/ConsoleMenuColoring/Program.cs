using System;
using System.Collections.Generic;

namespace ConsoleMenus
{
    internal class Program
    {
        static List<char[,]> drawings = new List<char[,]>();
        static List<ConsoleColor[,]> colors = new List<ConsoleColor[,]>();
        static int currentWidth = Console.WindowWidth;
        static int currentHeight = Console.WindowHeight;

        static void Main(string[] args)
        {

            Console.SetWindowSize(currentWidth, currentHeight + 1);

            Console.CursorVisible = false;

            bool kilep = true;

            string[] menuItems = { "START", "EDIT", "DELETE", "EXIT" };
            int selectedItem = 0;

            while (kilep)
            {
                Console.Clear();
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedItem)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    string middle = $"│ {menuItems[i]} │";
                    string topBorder = "┌" + new string('─', middle.Length - 2) + "┐";
                    string bottomBorder = "└" + new string('─', middle.Length - 2) + "┘";

                    Console.WriteLine(topBorder);
                    Console.WriteLine(middle);
                    Console.WriteLine(bottomBorder);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedItem = (selectedItem + 1) % menuItems.Length;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedItem = (selectedItem - 1 + menuItems.Length) % menuItems.Length;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (menuItems[selectedItem] == "START")
                    {
                        StartDrawing();
                    }
                    else if (menuItems[selectedItem] == "EDIT")
                    {
                        EditDrawing();
                    }
                    else if (menuItems[selectedItem] == "DELETE")
                    {
                        DeleteDrawing();
                    }
                    else if (menuItems[selectedItem] == "EXIT")
                    {
                        kilep = false;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    kilep = false;
                }
            }
        }

        static void StartDrawing()
        {
            char[,] drawing = new char[currentHeight, currentWidth];
            ConsoleColor[,] colorMatrix = new ConsoleColor[currentHeight, currentWidth];
            for (int i = 0; i < currentHeight; i++)
            {
                for (int j = 0; j < currentWidth; j++)
                {
                    drawing[i, j] = ' ';
                    colorMatrix[i, j] = ConsoleColor.White;
                }
            }

            rajzTabla(drawing, colorMatrix);
            drawings.Add(drawing);
            colors.Add(colorMatrix);
        }

        static void EditDrawing()
        {
            if (drawings.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Nincs elmentett munka amit lehetne módosítani!");
                Console.ReadKey();
                return;
            }

            int selectedDrawing = SelectDrawing();
            if (selectedDrawing >= 0 && selectedDrawing < drawings.Count)
            {
                char[,] drawing = drawings[selectedDrawing];
                ConsoleColor[,] colorMatrix = colors[selectedDrawing];
                rajzTabla(drawing, colorMatrix);
                drawings[selectedDrawing] = drawing;
                colors[selectedDrawing] = colorMatrix;
            }
        }

        static void DeleteDrawing()
        {
            if (drawings.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Nincs elmentett munka amit lehetne törölni!");
                Console.ReadKey();
                return;
            }

            int selectedDrawing = SelectDrawing();
            if (selectedDrawing >= 0 && selectedDrawing < drawings.Count)
            {
                drawings.RemoveAt(selectedDrawing);
                colors.RemoveAt(selectedDrawing);
                Console.Clear();
                Console.WriteLine("Munka törölve");
                Console.ReadKey();
            }
        }

        static int SelectDrawing()
        {
            int selectedIndex = 0;
            ConsoleKeyInfo keyInfo;

            do
            {
                Console.Clear();
                Console.WriteLine("Munka választás:");
                Console.ForegroundColor = ConsoleColor.White;

                for (int i = 0; i < drawings.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Rajz {i + 1}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Rajz {i + 1}");
                    }
                }

                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex + 1) % drawings.Count;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex - 1 + drawings.Count) % drawings.Count;
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            return selectedIndex;
        }


        static void rajzTabla(char[,] drawing, ConsoleColor[,] colorMatrix)
        {
            Console.Clear();
            int x = currentWidth / 2;
            int y = currentHeight / 2;
            char karakter = '█';
            ConsoleColor currentColor = ConsoleColor.White;
            Console.CursorVisible = false;


            while (true)
            {
                Console.Clear();

                for (int i = 0; i < currentHeight; i++)
                {
                    for (int j = 0; j < currentWidth; j++)
                    {
                        Console.ForegroundColor = colorMatrix[i, j];
                        Console.Write(drawing[i, j]);
                    }
                    Console.WriteLine();
                }

                Console.SetCursorPosition(x, y);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);


                if (keyInfo.Key == ConsoleKey.LeftArrow && x > 0)
                {
                    x--;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow && x < currentWidth - 1)
                {
                    x++;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow && y > 0)
                {
                    y--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && y < currentHeight - 1)
                {
                    y++;
                }
                else if (keyInfo.Key == ConsoleKey.NumPad0)
                {
                    currentColor = ConsoleColor.White;
                }
                else if (keyInfo.Key == ConsoleKey.NumPad1)
                {
                    currentColor = ConsoleColor.Red;
                }
                else if (keyInfo.Key == ConsoleKey.NumPad2)
                {
                    currentColor = ConsoleColor.Green;
                }
                else if (keyInfo.Key == ConsoleKey.NumPad3)
                {
                    currentColor = ConsoleColor.Blue;
                }
                else if (keyInfo.Key == ConsoleKey.NumPad4)
                {
                    currentColor = ConsoleColor.Magenta;
                }
                else if (keyInfo.Key == ConsoleKey.NumPad5)
                {
                    currentColor = ConsoleColor.Yellow;
                }
                else if (keyInfo.Key == ConsoleKey.F1)
                {
                    karakter = '█';
                }
                else if (keyInfo.Key == ConsoleKey.F2)
                {
                    karakter = '▓';
                }
                else if (keyInfo.Key == ConsoleKey.F3)
                {
                    karakter = '▒';
                }
                else if (keyInfo.Key == ConsoleKey.F4)
                {
                    karakter = '░';
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    drawing[y, x] = karakter;
                    colorMatrix[y, x] = currentColor;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    drawing[y, x] = ' ';
                    colorMatrix[y, x] = ConsoleColor.White;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;

                }
            }
        }
    }
}