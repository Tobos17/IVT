using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulationProject
{
    class Auto
    {
        public int nosnost; 
        public int dobaCesty;
        public int dobaNakládání; 
        public int dobaVykládání; 

        public Auto(int n, int dC, int dN, int dV)
        {
            nosnost = n;
            dobaCesty = dC;
            dobaNakládání = dN;
            dobaVykládání = dV;
        }
        
        //pro seřazení v PriorityQueue podle nejdřív nosnosti (čím víc tím líc => -nosnost jako priorita) a pak podle časů (dobaCesty, dobaNakládání, dobaVykládání)
        public Tuple<int, int, int, int> GetPriority()
        {
            return Tuple.Create(-nosnost, dobaCesty, dobaNakládání, dobaVykládání);
        }
    }

    class Akce
    {
        public int jmeno;
        public TUdalost typUdalosti;
        public int zacatek;

        public Akce(int id, TUdalost typ, int z)
        {
            jmeno = id;
            typUdalosti = typ;
            zacatek = z;
        }

        
        public enum TUdalost { Naloz, Vyloz }
    }

    class Simulace
    {
        private List<Akce> KalendarAkci = new List<Akce>();
        private PriorityQueue<Auto, Tuple<int, int, int, int>> Auta = new PriorityQueue<Auto, Tuple<int, int, int, int>>();

        public void Spustit(int písek, List<Auto> seznamAut)
        {
            // Přidání aut do prioritní fronty a do kalendáře jeho akci (podle jmena auta)
            foreach (Auto auto in seznamAut)
            {   
                // zařazení do queue podle priorit
                Auta.Enqueue(auto, auto.GetPriority());
                KalendarAkci.Add(new Akce(seznamAut.IndexOf(auto), Akce.TUdalost.Naloz, 0));
            }

            int časSimulace = 0;
            int posledníNaložení = 0; // čas pro výpočet čekání v queue

            while (písek > 0 && KalendarAkci.Any())
            {
                // Kvůli seřazení => začínáme s akcí s nejmenším časem začátku 
                KalendarAkci = KalendarAkci.OrderBy(a => a.zacatek).ToList();
                Akce aktuálníAkce = KalendarAkci.First();
                KalendarAkci.RemoveAt(0);
                
                // musím najít akci pro dané auto
                Auto aktuálníAuto = seznamAut[aktuálníAkce.jmeno];
                časSimulace = aktuálníAkce.zacatek;

                if (aktuálníAkce.typUdalosti == Akce.TUdalost.Naloz)
                {
                    // Kontrola, zda auto musí čekat na předchozí nakládku (čas čekání v queue)
                    int startNaložení = Math.Max(časSimulace, posledníNaložení);
                    // Console.WriteLine($"{časSimulace} {Math.Max(časSimulace, posledníNaložení)}");


                    //zápis události
                    Console.WriteLine($"Čas {startNaložení}: Auto s {aktuálníAuto.nosnost}kg začíná nakládat.");

                    // Nastavíme čas posledního naložení na čas, kdy toto auto dokončí nakládání (aby auto v pořádí vědelo, kolik má čekat)
                    // pomocí Math.Max(časSimulace, posledníNaložení) se zjistí jestli bude čekat nebo přijelo do volné queue
                    posledníNaložení = startNaložení + aktuálníAuto.dobaNakládání;

                    // Přidej událost vyložení
                    KalendarAkci.Add(new Akce(aktuálníAkce.jmeno, Akce.TUdalost.Vyloz, posledníNaložení + aktuálníAuto.dobaCesty));
                }
                else if (aktuálníAkce.typUdalosti == Akce.TUdalost.Vyloz)
                {
                    //zápis události
                    Console.WriteLine($"Čas {časSimulace}: Auto s {aktuálníAuto.nosnost}kg začíná vykládat.");

                    //odeberu
                    písek -= aktuálníAuto.nosnost;

                    // Pokud je ještě nějaký písek k vykládání, musí se počkat, dokud nebude vše přepraveno
                    if (písek > 0)
                    {
                        // Přidáváme událost nakládání po vykládání
                        int časDokončeníVykládání = časSimulace + aktuálníAuto.dobaVykládání;
                        KalendarAkci.Add(new Akce(aktuálníAkce.jmeno, Akce.TUdalost.Naloz, časDokončeníVykládání + aktuálníAuto.dobaCesty));
                    }
                    else
                    {
                        // Pokud už není písek, simulace končí, protože všechny akce jsou dokončeny
                        Console.WriteLine($"Čas dovozu všeho písku: {časSimulace + aktuálníAuto.dobaVykládání} min.");
                        break;
                    }
                }
            }

           
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {   
            // Simulace končí v okamžik kdy jakékoliv auto vyloží písek, přičemž dosháne zvolené hmotnosti písku
            // vypisuju jednotlivé události a jejich časy


            // s inputem =>
            // Console.WriteLine("Zvol si hmotnost písku na převoz?");
            // int pisek = Convert.ToInt32(Console.ReadLine());

            // List<Auto> seznamAut = new List<Auto>();

            // Console.WriteLine("Kolik aut chcete zadat?");
            // int pocetAut = Convert.ToInt32(Console.ReadLine());

            // for (int i = 0; i < pocetAut; i++)
            // {
            //     Console.WriteLine($"Zadejte parametry pro auto {i + 1} ve formátu: nosnost doba_cesty doba_nakladani doba_vykladani");
            //     string[] vstup = Console.ReadLine().Split(' ');
            //     int nosnost = int.Parse(vstup[0]);
            //     int dobaCesty = int.Parse(vstup[1]);
            //     int dobaNakladani = int.Parse(vstup[2]);
            //     int dobaVykladani = int.Parse(vstup[3]);

            //     seznamAut.Add(new Auto(nosnost, dobaCesty, dobaNakladani, dobaVykladani));
            // }

            //

            // s defaultnimi hodnotami
            // int pisek = 1000;

            // List<Auto> seznamAut = new List<Auto>
            // {
            //     new Auto(10, 20, 55, 50), 
            //     new Auto(45, 28, 15, 254),
            //     new Auto(5, 50, 94, 29),
            //     new Auto(65, 29, 15, 25),
            //     new Auto(15, 55, 14, 59),
            //     new Auto(145, 20, 45, 245),
            //     new Auto(154, 23, 16, 95),
            // };

            //


            // input s různým počtem aut: od určitého počtu aut se čas simulace už nezrychluje

            int pisek = 1000;

            Simulace simulace = new Simulace();
            
            List<Auto> seznamAut = new List<Auto>
            {
                new Auto(10, 20, 55, 50), 
            };
            simulace.Spustit(pisek, seznamAut);

            List<Auto> seznamAut2 = new List<Auto>
            {
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
            };
            simulace.Spustit(pisek, seznamAut2);

            List<Auto> seznamAut3 = new List<Auto>
            {
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
            };
            simulace.Spustit(pisek, seznamAut3);

            List<Auto> seznamAut4 = new List<Auto>
            {
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
            };
            simulace.Spustit(pisek, seznamAut4);

            List<Auto> seznamAut5 = new List<Auto>
            {
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
                new Auto(10, 20, 55, 50), 
            };
            simulace.Spustit(pisek, seznamAut5);
          
        
            Console.ReadLine();

        }
    }
}
