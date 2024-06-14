using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    internal class Map
    {


        public List <char[,]> ReadMap()
        {
            string RootFileName = "C:\\Users\\giuse\\source\\repos\\MyGame\\mymap_.txt";
            string[] Mappen = new string[3];
            List<char[,]> CharMappe = new List<char[,]>();
            for (int i = 0; i < Mappen.Length; i++)
            {
                char[] pfadCharArray = RootFileName.ToCharArray();
                pfadCharArray[pfadCharArray.Length - 5] = i.ToString()[0];
                string t_pfad = new string(pfadCharArray);
                Mappen[i] = t_pfad;


            }
            foreach (string pfad in Mappen)
            {

               
                string[] lines = File.ReadAllLines(pfad);
                int rows = lines.Length;
                int cols = 0;
                foreach (string line in lines)
                {
                    if (line.Length > cols)
                    {
                        cols = line.Length;
                    }
                }

                char[,] charArray = new char[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        charArray[i, j] = lines[i][j];
                    }

                    for (int j = lines[i].Length; j < cols; j++)
                    {
                        charArray[i, j] = ' ';
                    }
                }
                CharMappe.Add(charArray);

            }

            return CharMappe;
        }



        public Ding[,] GenerateMap(char[,] map)
        {
            Ding[,] Mappa = new Ding[map.GetLength(0), map.GetLength(1)];

            for (int i = 1; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == '#' && map[i - 1, j] == '#')
                    {
                        Point p = new Point(j * 30, i * 30);
                        GroundBrick brick = new GroundBrick(p);

                        Mappa[i, j] = brick;
                    }
                    else if (map[i, j] == '#' && map[i - 1, j] != '#')
                    {
                        Point p = new Point(j * 30, i * 30);
                        GrassBrick brick = new GrassBrick(p);
                        Mappa[i, j] = brick;


                    }
                    else if (map[i, j] == '+')
                    {
                        Point p = new Point(j * 30, i * 30);
                        SpikeBrick brick = new SpikeBrick(p);
                        Mappa[i, j] = brick;
                    }
                    else if (map[i, j] == '-')
                    {
                        Point p = new Point(j * 30, i * 30);
                        WaterBrick brick = new WaterBrick(p);
                        Mappa[i, j] = brick;
                    }
                    else if (map[i, j] == 'X')
                    {
                        Point p = new Point(j * 30, i * 30);
                        Me player  = new Me(p);
                        player.X = j * 30;
                        player.Y = i * 30;  
                        Mappa[i, j] = player;



                    }
                    else if (map[i, j] == 'Y')
                    {
                        Point p = new Point(j * 30, i * 30);
                        Baum brick = new Baum(p);
                        Mappa[i, j] = brick;



                    }
                    else
                    {
                        Point p = new Point(j * 30, i * 30);
                        Brick brick = new Brick(p);
                        Mappa[i, j] = brick;


                    }



                }


            }
            return Mappa;

        }

    }
}
