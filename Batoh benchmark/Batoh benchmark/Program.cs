using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace BenchmarkKnapsack
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<MyBenchmark>();

            //vypisování
            //MyBenchmark myBenchmark = new MyBenchmark();
            //myBenchmark.Knapsack_Backtracking();
            //myBenchmark.Knapsack_DynamicProgramming();
        }
    }

    [MemoryDiagnoser]
    public class MyBenchmark
    {
        int[] weights;
        int[] costs;
        long capacity;

        //pro backtracking
        int finalPrice1;

        public MyBenchmark()
        {
            Random rand = new Random();
            int size = rand.Next(5, 15);
            weights = new int[size];
            costs = new int[size];
            capacity = rand.Next(20, 50);

            for (int i = 0; i < size; i++)
            {
                weights[i] = rand.Next(1, 20);
                costs[i] = rand.Next(1, 100);


            }

            //Console.WriteLine("Weights: " + string.Join(", ", weights));
            //Console.WriteLine("Costs: " + string.Join(", ", costs));
            //Console.WriteLine("Capacity: " + capacity);

        }

        public void SolveKnapsack(int index, int currentWeight, int currentPrice, List<int> chosenItems)
        {

            if (index >= weights.Length)
            {
                if (currentPrice > finalPrice1)
                {
                    finalPrice1 = currentPrice;
                }
                return;
            }

            SolveKnapsack(index + 1, currentWeight, currentPrice, chosenItems);


            if (currentWeight + weights[index] <= capacity)
            {
                chosenItems.Add(index + 1);
                SolveKnapsack(index + 1, currentWeight + weights[index], currentPrice + costs[index], chosenItems);
                chosenItems.RemoveAt(chosenItems.Count - 1);
            }
        }



        [Benchmark]
        public void Knapsack_Backtracking()
        {
            //podobně jako u mincí
            SolveKnapsack(0, 0, 0, new List<int>());

            //výpisání výsledku
            //Console.WriteLine(finalPrice1);


        }

        [Benchmark]
        public void Knapsack_DynamicProgramming()
        {
            int[,] table = new int[weights.Length + 1, capacity + 1];

            for (int i = 0; i < costs.Length + 1; i++)
            {
                for (int j = 0; j < capacity + 1; j++)
                {
                    //pomocnej řádek nul viz obrázek v prezentaci
                    if (i == 0)
                    {
                        table[i, j] = 0;
                        //Console.WriteLine(table[i, j]);
                        continue;
                    }
                    int currentWeight = weights[i - 1];
                    int currentCost = costs[i - 1];

                    if (currentWeight <= j)
                    {
                        //Console.WriteLine(currentCosts);
                        //Console.WriteLine(table[i - 1, j] + "" + currentWeight + table[i - 1, j - currentCost]);
                        if (table[i - 1, j] <= currentCost + table[i - 1, j - currentWeight])
                        {
                            //přepis s checkem validity
                            table[i, j] = currentCost + table[i - 1, j - currentWeight];
                        }
                        else
                        {
                            //sepis seshora
                            table[i, j] = table[i - 1, j];
                        }
                    }
                    else
                    {
                        //sepis seshora
                        table[i, j] = table[i - 1, j];
                    }
                }
            }

            //// výpis všech čelnů
            //for (int i = 0; i < table.GetLength(0); i++)
            //{
            //    for (int j = 0; j < table.GetLength(1); j++)
            //    {
            //        Console.Write(table[i, j] + " ");
            //    }

            //}

            //výpis výsledku
            //Console.WriteLine(table[costs.Length, capacity]);
            

        }
    }

}