using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulationProject
{
    internal class Program
    {
            
        public class Tile
        {
            public int x;
            public int y;
            public int time;
            public List<int[]> path;

            public Tile (int x, int y, int time, List<int[]> path)
            {
                this.x = x;
                this.y = y;
                this.time = time;
                this.path = path;
            }
        }

        public static void PathTracer(int n, int m, int k, int[,] moves, int[,] tileMap)
        {
            Queue<Tile> queue = new Queue<Tile>();

            Tile start = new Tile(0, 0, 0, new List<int[]> { new int[2] { 0, 0 } });

            queue.Enqueue(start);

            bool[,] visited = new bool[n, n];
            visited[0, 0] = true;

            while (queue.Count > 0)
            {
                Tile targetTile = queue.Dequeue();

                for (int h = 0; h < k; h++)
                {

                    int nextX = targetTile.x + moves[h, 0];
                    int nextY = targetTile.y + moves[h, 1];

                    //Console.WriteLine(nextX);
                    //Console.WriteLine(nextY);

                    //Console.WriteLine(h + " " + k);

                    if (nextX > n - 1 || nextY > n - 1)
                    {
                        //Console.WriteLine("er");
                        continue;
                    }

                    bool avalanche = false;

                    //y = x
                    if (moves[h, 0] > 0 && moves[h, 1] > 0)
                    {
                        for (int currentX = targetTile.x + 1; currentX <= nextX; currentX++)
                        {
                            int d = currentX - targetTile.x;

                            if (targetTile.time >= tileMap[targetTile.x + d, targetTile.y + d])
                            {
                                //Console.WriteLine(h);
                                avalanche = true;
                                continue;
                            }

                        }
                    }
                    //horziontalni
                    else if (moves[h, 1] == 0)
                    {
                        for (int currentX = targetTile.x + 1; currentX <= nextX; currentX++)
                        {
                            if (targetTile.time >= tileMap[currentX, targetTile.y])
                            {
                                avalanche = true;
                                continue;
                            }
                        }
                    }
                    //vertikalni
                    else if (moves[h, 0] == 0)
                    {
                        for (int currentY = targetTile.y + 1; currentY <= nextY; currentY++)
                        {
                            if (targetTile.time >= tileMap[targetTile.x, currentY])
                            {
                                avalanche = true;
                                continue;
                            }
                        }
                    }


                    if (!visited[nextX, nextY] && !avalanche)
                    {
                        visited[nextX, nextY] = true;

                        //Console.WriteLine(nextX + ", " + nextY);
                        //navstivyl policko a posunutej
                        Tile nextTile = new Tile(nextX, nextY, targetTile.time + 1, new List<int[]>(targetTile.path) { new int[2] { nextX, nextY } });


                        //cíl
                        if (nextTile.x == n - 1 && nextTile.y == n - 1)
                        {
                            Console.WriteLine(nextTile.time);

                            if (nextTile.time != -1)
                            {
                                foreach (int[] xy in nextTile.path)
                                {
                                    Console.WriteLine($"[{xy[0]}, {xy[1]}]");
                                }

                            }
                            return;

                        }


                        if (nextTile.time < tileMap[nextX, nextY])
                        {
                            queue.Enqueue(nextTile);
                        }

                    }
                }
            }

            //pokud se nedostane k n-1, n-1
            Console.WriteLine(-1);
        }

        static void Main(string[] args)
        {
            try
            {
                string File = "b.txt";

                using (StreamReader sr = new StreamReader(File))
                {
                    int n = int.Parse(sr.ReadLine());
                    int m = int.Parse(sr.ReadLine());

                    int[,] tileMap = new int[n, n];

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            tileMap[i, j] = 99999;
                        }
                    }

                    for (int i = 0; i < m; i++)
                    {
                        string[] input = sr.ReadLine().Split();

                        int x = int.Parse(input[0]);
                        int y = int.Parse(input[1]);
                        int t = int.Parse(input[2]);

                        tileMap[x, y] = t;
                    }

                    int k = int.Parse(sr.ReadLine());

                    int[,] moves = new int[k, 2];

                    for (int i = 0; i < k; i++)
                    {
                        string[] input = sr.ReadLine().Split();

                        int x = int.Parse(input[0]);
                        int y = int.Parse(input[1]);

                        moves[i, 0] = x;
                        moves[i, 1] = y;
                    }

                    PathTracer(n, m, k, moves, tileMap);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
    }
}