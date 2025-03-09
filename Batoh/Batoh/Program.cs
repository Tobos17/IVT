using System;
using System.Collections.Generic;
using System.Linq;

class KnapsackBacktracking
{
    static int maxPrice = 0;
    static List<int> bestItems = new List<int>();

    static void SolveKnapsack(int[] weights, int[] prices, int capacity, int index, int currentWeight, int currentPrice, List<int> chosenItems)
    {
        if (currentWeight > capacity)
            return;

        if (currentPrice > maxPrice)
        {
            maxPrice = currentPrice;
            bestItems = new List<int>(chosenItems);
        }

        if (index >= weights.Length)
            return;

        SolveKnapsack(weights, prices, capacity, index + 1, currentWeight, currentPrice, chosenItems);

        if (currentWeight + weights[index] <= capacity)
        {
            chosenItems.Add(index + 1);
            SolveKnapsack(weights, prices, capacity, index + 1, currentWeight + weights[index], currentPrice + prices[index], chosenItems);
            chosenItems.RemoveAt(chosenItems.Count - 1);
        }
    }

    static void Main()
    {
        int[] weights = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int[] prices = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int capacity = int.Parse(Console.ReadLine());

        SolveKnapsack(weights, prices, capacity, 0, 0, 0, new List<int>());

        Console.WriteLine(maxPrice);
        Console.WriteLine(string.Join(" ", bestItems));
    }
}
