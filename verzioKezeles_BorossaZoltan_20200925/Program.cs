﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarcosProjekt
{
    class Program
    {
        public static List<Harcos> harcosok;
        static void beolvas(string fajl)
        {
            StreamReader sr = new StreamReader(fajl);
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(';');
                harcosok.Add(new Harcos(sor[0], Convert.ToInt32(sor[1])));
            }
            sr.Close();
        }

        public static Harcos felhasznaloLetrehozas()
        {
            string felhasznaloNev;
            int statusz;
            Console.WriteLine("\n\nAdja meg a karaktere felhasználó nevét!");
            felhasznaloNev = Console.ReadLine();
            Console.WriteLine("Adja meg a karaktere státusz sablonját!");
            Console.WriteLine("1.\t HP:18/18 - DMG: 4\n" +
                "2.\t HP:15/15 - DMG: 5\n" +
                "3.\t HP:11/11 - DMG: 6\n");
            do
            {
                statusz = Convert.ToInt32(Console.ReadLine());
                if (statusz>3 || statusz<1)
                {
                    Console.WriteLine("HIBÁS adat!");
                }
            } while (!(statusz >0 && statusz<4));
            Console.WriteLine("------------------------------------------------------------------------------------------");
            return new Harcos(felhasznaloNev, statusz);
        }

        public static void osszesHarcosKiir()
        {
            for (int i = 0; i < harcosok.Count; i++)
            {
                Console.WriteLine((i+1)+".\t"+harcosok[i]);
            }
        }

        public static string betujel = " ";
        public static void menu()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("\n\nMit szeretnél tenni? (üsse be a megfelelő betűjelet)\n");
            Console.WriteLine("\ta.) Megküzdeni egy harcossal" +
                "\n\tb.) Gyógyulni" +
                "\n\tc.) Kilépni");
            do
            {
                betujel = Console.ReadLine();

                if (!(betujel.Equals("a")|| betujel.Equals("b") || betujel.Equals("c")))
                {
                    Console.WriteLine("Nincs ilyen menüpont, adja meg újra!");
                }
                if (betujel.Equals("a"))
                {
                    int melyikHarcos = 0;
                    do
                    {
                        Console.WriteLine("Melyik harcossal szeretne megküzdeni?");
                        melyikHarcos = Convert.ToInt32(Console.ReadLine());


                        if (melyikHarcos > harcosok.Count || melyikHarcos<1)
                        {
                            Console.WriteLine("Nincs ilyen sorszámú harcos");
                        }
                        else
                        {
                            harcosok[harcosok.Count - 1].Megkuzd(harcosok[melyikHarcos-1]);
                        }
                        harmadikKorben();
                    } while (!(melyikHarcos > 0 && melyikHarcos <= harcosok.Count-1));
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                }
                if (betujel.Equals("b"))
                {
                    harcosok[harcosok.Count - 1].Gyogyul();
                }


            } while (!(betujel=="a" || betujel =="b" || betujel == "c"));


        }
        public static int korokszama = 0;
        public static void harmadikKorben()
        {
            
            if (korokszama == 2)
            {
                int szam;
                do
                {
                    Random r = new Random();
                    szam = r.Next(harcosok.Count - 1);
                } while (harcosok[szam].Eletero == 0);
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("Téged megtámadott: "+harcosok[szam].Nev);
                Console.WriteLine("------------------------------------------------------------------------------------------");
                harcosok[szam].Megkuzd(harcosok[harcosok.Count-1]);
                for (int i = 0; i < harcosok.Count; i++)
                {
                    harcosok[i].Gyogyul();
                }
                korokszama = 0;
            }
            korokszama++;


        }


        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------------------------------------");
            harcosok = new List<Harcos>() {new Harcos("Szabi", 2) , new Harcos("Zsombi", 1), new Harcos("Zoli", 3) };
            beolvas("harcosok.csv");
            
            for (int i = 0; i < harcosok.Count; i++)
            {
                Console.WriteLine(harcosok[i]);
            }
            Console.WriteLine("------------------------------------------------------------------------------------------");
            harcosok.Add(felhasznaloLetrehozas());
            Console.WriteLine(harcosok[harcosok.Count-1]);
            do
            {
                osszesHarcosKiir();
                menu();
                
            } while (betujel!="c");
            


            Console.ReadKey();
        }

    }
}
