using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{
    internal class Level
    {
        public int Width { get { return m_width; } }
        int m_width;
        int m_height;
        public int Height { get { return m_height; } }
        int[,] m_level;
        public int[,] GetLevel { get { return m_level; } set { m_level = value; } }
        public enum Legend
        {
            empty = 0,
            wall = 1,
        }
        public Level(int[,] level)
        {
            m_level = level;
            m_width = level.GetLength(1);
            m_height = level.GetLength(0);
        }
        public Level(int size) : this(size, size)
        {
        }
        public Level(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_level = Create();
        }

        public int[,] Create()
        {
            int[,] level = new int[m_height, m_width];
            for (int heightIdx = 0; heightIdx < m_height; heightIdx++)
            {
                for (int widthIdx = 0; widthIdx < m_width; widthIdx++)
                {
                    if (((heightIdx == 0 || heightIdx == m_height - 1) && (0 <= widthIdx && widthIdx < m_width)) ||
                        ((widthIdx == 0 || widthIdx == m_width - 1) && (0 <= heightIdx && heightIdx < m_height)))
                    {
                        level[heightIdx, widthIdx] = 1;
                    }
                    else
                    {
                        level[heightIdx, widthIdx] = 0;
                    }
                }
            }
            return level;
        }
        public void Draw(int[] pos1, int[] pos2, List<int[]> path)
        {
            for (int heightIdx = 0; heightIdx < m_height; heightIdx++)
            {
                for (int widthIdx = 0; widthIdx < m_width; widthIdx++)
                {
                    bool isPath = false;
                    for (int i = 0; i < path.Count; i++)
                    {
                        if ((path[i][1] == heightIdx && path[i][0] == widthIdx))
                        {
                            isPath = true;
                            break;
                        }
                    }
                    if (pos1[1] == heightIdx && pos1[0] == widthIdx)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("  ");
                        Console.ResetColor();
                    }
                    else if (pos2[1] == heightIdx && pos2[0] == widthIdx)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("  ");
                        Console.ResetColor();
                    }
                    else if (isPath)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write("  ");
                        Console.ResetColor();
                    }
                    else if (m_level[heightIdx, widthIdx] == 9)
                    {
                        Console.Write("o ");
                    }
                    else
                    {
                        switch (m_level[heightIdx, widthIdx])
                        {
                            case 1: Console.Write("x "); break;
                            default: Console.Write("  "); break;
                        }
                    }
                }
                Console.WriteLine();
            }
        }
        public void Draw(List<int[]> snake)
        {
            for (int heightIdx = 0; heightIdx < m_height; heightIdx++)
            {
                for (int widthIdx = 0; widthIdx < m_width; widthIdx++)
                {
                    bool isOnSnake = false;
                    bool isHead = false;
                    for (int i = 0; i < snake.Count; i++)
                    {
                        if (snake[i][0] == widthIdx && snake[i][1] == heightIdx)
                        {
                            isOnSnake = true;
                            isHead = (i == 0);
                            break;
                        }
                    }
                    if (isOnSnake)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        if (isHead)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("[]");
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                        Console.ResetColor();
                    }
                    else if (m_level[heightIdx, widthIdx] == 9)
                    {
                        Console.Write("o ");
                    }
                    else
                    {
                        switch (m_level[heightIdx, widthIdx])
                        {
                            case 1: Console.Write("x "); break;
                            default: Console.Write("  "); break;
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
