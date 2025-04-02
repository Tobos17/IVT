using System;

namespace nrp
{
    class Program
    {
        static void nrp(int[] input)
        {
            //int[] input = { int.MinValue, 3, 14, 15, 92, 65, 35, 89, 79, 32, 38, 46, 26, 43 };      
            int n = input.Length - 1;

            //mínus nekonečno pomáhá k vypsání, bude mít vždy index nula, tudíž t[0] 
            //bude o jedno delší než počet prvků nejdelší posloupnosti (viz výpis)

            int[] t = new int[input.Length];
            int[] p = new int[input.Length];

            for (int i = n; i >= 0; i--)
            {
                t[i] = 1;
                p[i] = 0;

                for (int j = i + 1; j <= n; j++)
                {
                    if (input[i] < input[j] && t[i] < t[j] + 1)
                    {
                        t[i] = t[j] + 1;
                        p[i] = j;
                    }
                }

            }

            //výpis
            //počet prvků v nrp
            //Console.WriteLine(t[0] - 1);



            //List<(int, int)> results = new List<(int, int)>();
            List<int> results = new List<int>();

            //postupně od indexu nula do indexu posledního prvku nrp
            int index = p[0];
            while (index != 0)
            {
                //hodnota a index čísla v nrp
                //results.Add((input[index], index));

                //pouze hodnota
                results.Add(input[index]);

                index = p[index];
            }

            Console.WriteLine(string.Join(" ", results));
            //nová line
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vstupy.txt");
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++)
            {

                //Console.WriteLine($"line {i}: {lines[i]}"); 
                //if (string.IsNullOrWhiteSpace(lines[i])) continue;

                if(i % 2 != 0) continue;


                //int valdiního pole s mínus nekonečnem na začátku a čislicemi z souboru
                int[] input = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Prepend(int.MinValue)
                .ToArray();


                //Console.WriteLine(input.Length);
                //kvůli mínus nekonečnu 1
                if (input.Length == 1)
                {
                    Console.WriteLine("prázdná posloupnost");
                    //nová line
                    Console.WriteLine();
                    continue;
                }

                nrp(input);
            }



        }
    }
}