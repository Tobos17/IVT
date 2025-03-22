using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        //načetní vstupu (daná hodnota mince a cilová částka) 
        int[] coins = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int target = int.Parse(Console.ReadLine());

        //list pro všechny kombinace, které drží mince, jejichž součet odpovídá cilové částce
        List<List<int>> results = new List<List<int>>();

        //pokud je cilová částka 0, k rekurzi nedojde a vypiši "nelze"
        //to dělám kvůli podmínce v FindCombinations if(target == 0),
        //ktera zastavuje rekurzi, jenže kdyby byla cílová částka nula,
        //tak by se tato podmínka spsutila a přidala do listu results prazdny list current.
        //Tento list by poté neměl délku nula, ale 1 a
        //další podmínka if (results.Count == 0) by nefungovala.
        //Takže by se nevypsalo nic místo "nelze".
        if (target == 0)
        {
            Console.WriteLine("nelze");
            return;
        }

        //zavolání metody v které je rekurze s backtrackingem
        FindCombinations(coins, target, 0, new List<int>(), results);

        //Pokud je cílová částka není nula, ale její hodnoty nelze dosáhnout
        //žádným uspořádáním daných mincí, délka výsledného listu results bude 0
        //a vypíše se "nelze".
        if (results.Count == 0)
        {
            Console.WriteLine("nelze");
            return;
        }

        //jelikož se do vstupu píší hodnoty mince sestupně
        //výsledný list results bude sestupný a to i jeho všechny prvky (listy).
        //Tudíž se nemusí sortovat a pouze výpíši jeho prvky, které budou sestupně.
        foreach (var combination in results)
        {
            Console.WriteLine(string.Join(" ", combination));
        }

    }

    static void FindCombinations(int[] coins, int target, int index, List<int> current, List<List<int>> results)
    {
        //podmínka pro uknočení rekurze
        //pokud se do dočasného listu current přidala hodnota,
        //která změní hodnotu tohoto listu tak, aby odpovídala cílové částce target,
        //tento list se přidá do vysledného listu results a rekurze se ukončí
        if (target == 0)
        {

            results.Add(new List<int>(current));
            return;
        }

        //rekurze pro všechny mince
        for (int i = index; i < coins.Length; i++)
        {

            //podminka pro zjisťění validity mince
            //Je-li její hodnta je mensí nebo rovna cílové částce
            //může se tedy přidat do listu current a pokračovat v rekurzi
            //Tento target je cílová částka pouze pro první iteraci dané mince.
            //Poté je tento target zmenšen o častky mincí, které se nacházejí v
            //dočasém listu current.
            if (coins[i] <= target)
            {
                
                //pokud se tedy může mince přidat a vejde se do dané cílové částky,
                //minci se pridá do dočasného listu current
                current.Add(coins[i]);

                //zde je zavolána rekurze, kde se pracuje s listem current,
                //v kterém je již pridaná odpovídající mince.
                //Hodnota mincí tohoto listu je odečtena od původního targetu (target - coins[i]),
                //čímž lze poté dosáhnout konečné rekurze, kde se tento rozdíl rovná 0.
                //Poté je rekurze ukonce v podmínce if(target == 0).
                //A je vyzkoušena pro všechny možné mince pro nelezení všech možných kombinací.
                //(Iterace všech možných mincí se všemi mincemi => všechny kombinace)
                FindCombinations(coins, target - coins[i], i, current, results);

                //pokud se zavolání rekurze FindCombinations stejně obejde bez vrácení (return),
                //je tento dočasný list current ochuzen o tento poseldní prvek, což mu umožnuje vrátit se
                //do jeho původního stavu a přídaní nového prvku. ([1, 2, všechny zbylé validní možnosti] nefunguje => [1, 2])
                current.RemoveAt(current.Count - 1);

                //poté opět vchází do for cyklu, kde je již další nová kombinace, která může vytvořit
                //nespočet dalších kombinací (poté v for cyklu => [1, 2, všechny zbylé validní možnosti bez tohoto akutláního indexu]).
            }
        }
    }
}