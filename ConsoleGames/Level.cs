using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{
    public struct LevelCell
    {
        int cellId;
        string cellString;
        ConsoleColor cellColor;
        public LevelCell(int id, string s, ConsoleColor color = ConsoleColor.Black)
        {
            CellId = id;
            CellString = s;
            CellColor = color;
        }

        public int CellId { get => cellId; set => cellId = value; }
        public string CellString { get => cellString; set => cellString = value; }
        public ConsoleColor CellColor { get => cellColor; set => cellColor = value; }
    }
    internal class Level
    {
        public int Width { get { return m_width; } }
        int m_width;
        int m_height;
        int[,] m_level;
        int[,] m_levelAdpt;
        public int Height { get { return m_height; } }
        public int[,] LevelAccesor { get { return m_level; } set { m_level = value; } }
        public int[,] LevelAdpt { get { return m_levelAdpt; } set { m_levelAdpt = value; } }
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
            m_levelAdpt = new int[Height, Width];
        }
        public Level(int size) : this(size, size)
        {
        }
        public Level(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_level = Create();
            m_levelAdpt = new int[height, width];
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
        public void Draw(List<int[]> snake, int[] coin, List<LevelCell> cells = null)
        {
            cells ??= new List<LevelCell>();
            for (int i = 0; i < snake.Count; i++)
            {
                m_levelAdpt[snake[i][1], snake[i][0]] = 4;
            }
            m_levelAdpt[snake[0][1], snake[0][0]] = 3;
            m_levelAdpt[coin[1], coin[0]] = 2;
            Draw(cells);
        }
        public void Draw(List<int[]> path, int[] start, int[] end, List<LevelCell> cells = null)
        {
            cells ??= new List<LevelCell>();
            for (int i = 0; i < path.Count; i++)
            {
                m_levelAdpt[path[i][1], path[i][0]] = 4;
            }
            m_levelAdpt[start[1], start[0]] = 2;
            m_levelAdpt[end[1], end[0]] = 3;
            Draw(cells);
        }
        public void Draw(List<LevelCell> cells)
        {
            Console.Clear();
            for (int heightIdx = 0; heightIdx < m_height; heightIdx++)
            {
                for (int widthIdx = 0; widthIdx < m_width; widthIdx++)
                {
                    bool handled = false;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        if (m_levelAdpt[heightIdx, widthIdx] == cells[i].CellId)
                        {
                            Console.BackgroundColor = cells[i].CellColor;
                            Console.Write(cells[i].CellString);
                            Console.ResetColor();
                            handled = true;
                        }
                    }
                    if (!handled)
                    {
                        switch (m_levelAdpt[heightIdx, widthIdx])
                        {
                            case 1: Console.Write("x "); break;
                            default: Console.Write("  "); break;
                        }
                    }
                }
                Console.WriteLine();
            }
            m_levelAdpt = (int[,])m_level.Clone();
        }
        public void Debug()
        {
            for (int heightIdx = 0; heightIdx < m_height; heightIdx++)
            {
                for (int widthIdx = 0; widthIdx < m_width; widthIdx++)
                {
                    Console.Write(m_level[heightIdx, widthIdx] + " ");
                }
                Console.Write("\t");
                for (int widthIdx = 0; widthIdx < m_width; widthIdx++)
                {
                    Console.Write(m_levelAdpt[heightIdx, widthIdx] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
