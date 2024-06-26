using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dalverseny
{
    class Versenyzo
    {
        private int rajtSzam;
        private string nev;
        private string szak;
        private int pontSzam;

        public Versenyzo(int rajtSzam, string nev, string szak)
        {
            this.rajtSzam = rajtSzam;
            this.nev = nev;
            this.szak = szak;
        }

        public void PontotKap(int pont)
        {
            pontSzam += pont;
        }

        public override string ToString()
        {
            return rajtSzam + "\t" + nev + "\t" + szak + (szak.Length < 8 ? "\t\t" : "\t")
                + (pontSzam.ToString().Length == 2 ? "" : " ") + pontSzam + " pont";
        }

        public int RajtSzam
        {
            get { return rajtSzam; }
        }
        public string Nev
        {
            get { return nev; }
        }
        public string Szak
        {
            get { return szak; }
        }
        public int PontSzam
        {
            get { return pontSzam; }
        }
    }

    class VezerloOsztaly
    {
        private int zsuriLetszam = 5;
        private int pontHatar = 10;
        private int hossz = 47;

        public void Start()
        {
            
            AdatBevitel();

            Kiiratas("\nRésztvevők\n");
            Verseny();
            Kiiratas("\nEredmények\n");

            Eredmenyek();
            Keresesek();
        }

        private List<Versenyzo> versenyzok = new List<Versenyzo>();

        private void AdatBevitel()
        {
            Versenyzo versenyzo;
            string nev, szak;
            int sorszam = 1;

            StreamReader olvasoCsatorna = new StreamReader("versenyzok.txt");

            while (!olvasoCsatorna.EndOfStream)
            {
                nev = olvasoCsatorna.ReadLine();
                szak = olvasoCsatorna.ReadLine();

                versenyzo = new Versenyzo(sorszam, nev, szak);
                versenyzok.Add(versenyzo);
                sorszam++;
            }
            olvasoCsatorna.Close();
        }

        private void Kiiratas(string cim)
        {
            Console.Write(cim);
            Console.Write("+");
            for (int i = 0; i < hossz; i++)
            {
                Console.Write("─"); ;
            }
            Console.WriteLine("+");
            foreach (Versenyzo v in versenyzok)
            {
                Console.WriteLine("│ " + v + " │");
            }
            Console.Write("+");
            for (int i = 0; i < hossz; i++)
            {
                Console.Write("─"); ;
            }
            Console.WriteLine("+");
        }

        private void Verseny()
        {
            Random rand = new Random();
            int pont;
            foreach (Versenyzo v in versenyzok)
            {
                for (int i = 1; i < zsuriLetszam; i++)
                {
                    pont = rand.Next(pontHatar);
                    v.PontotKap(pont);
                }
            }
        }

        private void Eredmenyek()
        {
            Nyertes();
            Sorrend();
        }

        private void Nyertes()
        {
            int max = versenyzok[0].PontSzam;

            foreach (Versenyzo v in versenyzok)
            {
                if (v.PontSzam > max)
                {
                    max = v.PontSzam;
                }
            }

            Console.WriteLine("\nA legjobb(ak)");
            Console.Write("+");
            for (int i = 0; i < hossz; i++)
            {
                Console.Write("─"); ;
            }
            Console.WriteLine("+");
            foreach (Versenyzo v in versenyzok)
            {
                if (v.PontSzam == max)
                {
                    Console.WriteLine("│ " + v + " │");
                }
            }
            Console.Write("+");
            for (int i = 0; i < hossz; i++)
            {
                Console.Write("─"); ;
            }
            Console.WriteLine("+");
        }

        private void Sorrend()
        {
            Versenyzo temp;
            for (int i = 0; i < versenyzok.Count - 1; i++)
            {
                for (int j = i + 1; j < versenyzok.Count; j++)
                {
                    if (versenyzok[i].PontSzam < versenyzok[j].PontSzam)
                    {
                        temp = versenyzok[i];
                        versenyzok[i] = versenyzok[j];
                        versenyzok[j] = temp;
                    }
                }
            }
            Kiiratas("\nEredménytábla\n");
        }

        private void Keresesek()
        {
            Console.WriteLine("\nÉnekesek keresése\n");
            Console.Write("\nKeres valakit? (i/n) ");
            char valasz;
            while (!char.TryParse(Console.ReadLine(), out valasz))
            {
                Console.Write("Egy karaktert írjon. ");
            }

            string miAlapjan;
            bool vanIlyen;

            while (valasz == 'i' || valasz == 'I')
            {
                Console.Write("Mi alapján szeretne keresni? (név/szak/pontszám) ");
                miAlapjan = Console.ReadLine();
                if (miAlapjan.ToLower() == "név" || miAlapjan.ToLower() == "nev")
                {
                    string nev;
                    Console.Write("\nNév: ");
                    nev = Console.ReadLine();
                    vanIlyen = false;

                    foreach (Versenyzo v in versenyzok)
                    {
                        if (v.Nev.ToLower() == nev.ToLower())
                        {
                            vanIlyen = true;
                            break;
                        }
                    }
                    
                    if (vanIlyen)
                    {
                        Console.Write("+");
                        for (int i = 0; i < hossz; i++)
                        {
                            Console.Write("─"); ;
                        }
                        Console.WriteLine("+");
                    }

                    foreach (Versenyzo v in versenyzok)
                    {
                        if (v.Nev.ToLower() == nev.ToLower())
                        {
                            Console.WriteLine("│ " + v + " │");
                        }
                    }

                    if (vanIlyen)
                    {
                        Console.Write("+");
                        for (int i = 0; i < hossz; i++)
                        {
                            Console.Write("─"); ;
                        }
                        Console.WriteLine("+");
                    }
                    else
                    {
                        Console.WriteLine("Ezzel a névvel senki sem indult.");
                    }

                }
                else if (miAlapjan.ToLower() == "szak")
                {
                    string szak;
                    Console.Write("\nSzak: ");
                    szak = Console.ReadLine();
                    vanIlyen = false;

                    foreach (Versenyzo v in versenyzok)
                    {
                        if (v.Szak.ToLower() == szak.ToLower())
                        {
                            vanIlyen = true;
                            break;
                        }
                    }

                    if (vanIlyen)
                    {
                        Console.Write("+");
                        for (int i = 0; i < hossz; i++)
                        {
                            Console.Write("─"); ;
                        }
                        Console.WriteLine("+");
                    }

                    foreach (Versenyzo v in versenyzok)
                    {
                        if (v.Szak.ToLower() == szak.ToLower())
                        {
                            Console.WriteLine("│ " + v + " │");
                        }
                    }

                    if (vanIlyen)
                    {
                        Console.Write("+");
                        for (int i = 0; i < hossz; i++)
                        {
                            Console.Write("─"); ;
                        }
                        Console.WriteLine("+");
                    }
                    else
                    {
                        Console.WriteLine("Erről a szakról senki sem indult.");
                    }
                }
                else if (miAlapjan.ToLower() == "pontszám" || miAlapjan.ToLower() == "pontszam")
                {
                    int pontszam;
                    Console.Write("\nPontszám: ");

                    while (!Int32.TryParse(Console.ReadLine(), out pontszam))
                    {
                        Console.Write("Valós számot adjon meg. ");
                    }
                    vanIlyen = false;

                    foreach (Versenyzo v in versenyzok)
                    {
                        if (v.PontSzam == pontszam)
                        {
                            vanIlyen = true;
                            break;
                        }
                    }

                    if (vanIlyen)
                    {
                        Console.Write("+");
                        for (int i = 0; i < hossz; i++)
                        {
                            Console.Write("─"); ;
                        }
                        Console.WriteLine("+");
                    }

                    foreach (Versenyzo v in versenyzok)
                    {
                        if (v.PontSzam == pontszam)
                        {
                            Console.WriteLine("│ " + v + " │");
                        }
                    }

                    if (vanIlyen)
                    {
                        Console.Write("+");
                        for (int i = 0; i < hossz; i++)
                        {
                            Console.Write("─"); ;
                        }
                        Console.WriteLine("+");
                    }
                    else
                    {
                        Console.WriteLine("Ezzel a pontszámmal senki sem indult.");
                    }
                }
                else
                {
                    Console.WriteLine("Hibás keresési feltétel.");
                }

                

                Console.Write("\nKeres még valakit? (i/n) ");
                while (!char.TryParse(Console.ReadLine(), out valasz))
                {
                    Console.Write("Egy karaktert írjon. ");
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            new VezerloOsztaly().Start();

            Console.ReadKey();
        }
    }
}