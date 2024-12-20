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
        int m_width;
        int m_height;
        int[,] m_level;
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
            for (int height = 0; height < m_height; height++)
            {
                for (int width = 0; width < m_width; width++)
                {
                    if (((height == 0 || height == m_height - 1) && (0 <= width && width < m_width)) ||
                        ((width == 0 || width == m_width - 1) && (0 <= height && height < m_height)))
                    {
                        level[height, width] = 1;
                    }
                    else
                    {
                        level[height, width] = 0;
                    }
                }
            }
            return level;
        }
        public void Draw()
        {
            for (int height = 0; height < m_height; height++)
            {
                for (int width = 0; width < m_width; width++)
                {
                    if (false)
                    {

                    }
                    else
                    {
                        switch (m_level[height, width])
                        {
                            case 1:Console.Write("x ");break;
                            default:Console.Write("  ");break;
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
