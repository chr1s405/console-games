using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        int m_width;
        int m_height;
        int[,] m_levelInit;
        int[,] m_level;
        public int Width { get { return m_width; } }
        public int Height { get { return m_height; } }
        public int[,] LevelAccesor { get { return m_levelInit; } set { m_levelInit = value; } }
        public int[,] LevelAdpt { get { return m_level; } set { m_level = value; } }
        public enum Legend
        {
            empty = 0,
            wall = 1,
        }
        public Level(int[,] level)
        {
            m_levelInit = level;
            m_width = level.GetLength(1);
            m_height = level.GetLength(0);
            m_level = new int[Height, Width];
        }
        public Level(int size) : this(size, size) { }
        public Level(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_levelInit = Create();
            m_level = new int[height, width];
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
        public void Reset()
        {
            m_level = (int[,])m_levelInit.Clone();
        }
        public void Edit(int x, int y, int value)
        {
            if( (0 <= y && y < m_height) && (0<= x && x < m_width))
            {
                m_level[y, x] = value;
            }
        }
        public void Edit(int[] pos, int value)
        {
            Edit(pos[0], pos[1], value);
        }
        public void Edit(int[,] pos, int value)
        {
            for (int i = 0; i < pos.GetLength(0); i++)
            {
                Edit(pos[i, 0], pos[i, 1], value);
            }
        }
        public void Edit(List<int[]> pos, int value)
        {
            for (int i = 0; i < pos.Count(); i++)
            {
                Edit(pos[i][0], pos[i][1], value);
            }
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
                        if (m_level[heightIdx, widthIdx] == cells[i].CellId)
                        {
                            Console.BackgroundColor = cells[i].CellColor;
                            Console.Write(cells[i].CellString);
                            Console.ResetColor();
                            handled = true;
                        }
                    }
                    if (!handled)
                    {
                        switch (m_level[heightIdx, widthIdx])
                        {
                            case 1: Console.Write("[]"); break;
                            default: Console.Write("  "); break;
                        }
                    }
                }
                Console.WriteLine();
            }
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
                    Console.Write(m_levelInit[heightIdx, widthIdx] + " ");
                }
                Console.WriteLine();
            }
        }
        public override string ToString()
        {
            string levelString = "";
            levelString += $"Width: {m_width}\n";
            levelString += $"Height: {m_height}\n";
            for (int heightIdx = 0; heightIdx < m_height; heightIdx++)
            {
                for (int widthIdx = 0; widthIdx < m_width; widthIdx++)
                {
                    levelString += (m_level[heightIdx, widthIdx] + " ");
                }
                levelString += ("\t");
                for (int widthIdx = 0; widthIdx < m_width; widthIdx++)
                {
                    levelString += (m_levelInit[heightIdx, widthIdx] + " ");
                }
                levelString += "\n";
            }
            return levelString;
        }
    }
}
