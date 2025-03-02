using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] coins = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int target = int.Parse(Console.ReadLine());

        List<List<int>> results = new List<List<int>>();
        FindCombinations(coins, target, 0, new List<int>(), results);
        
        //Console.WriteLine(results.Count);
        if (results.Count == 0)
        {
            Console.WriteLine("nelze");
            return;
        }
        results.Sort((a, b) => CompareLists(a, b));
        foreach (var combination in results)
        {
            Console.WriteLine(string.Join(" ", combination));
        }

    }

    static void FindCombinations(int[] coins, int target, int index, List<int> current, List<List<int>> results)
    {
        if (target == 0)
        {
            //Console.WriteLine(current.Count);
            if (current.Count > 0) 
                results.Add(new List<int>(current));
            return;
        }

        for (int i = index; i < coins.Length; i++)
        {
            if (coins[i] <= target)
            {
                //Console.WriteLine(coins[i] + " " + target);
                current.Add(coins[i]);
                FindCombinations(coins, target - coins[i], i, current, results);
                current.RemoveAt(current.Count - 1);
            }
        }
    }

    static int CompareLists(List<int> a, List<int> b)
    {
        int minLength = Math.Min(a.Count, b.Count);
        for (int i = 0; i < minLength; i++)
        {
            if (a[i] != b[i])
                return b[i].CompareTo(a[i]); 
        }
        return b.Count.CompareTo(a.Count);
    }
}
