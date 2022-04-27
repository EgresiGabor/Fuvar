using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuvar
{
    class Program
    {
        static void Main(string[] args)
        {
            //2. feladat
            List<Fuvar> fuvarok = new List<Fuvar>();
            using (StreamReader fajl = new StreamReader("fuvar.csv", Encoding.UTF8))
            {
                fajl.ReadLine();
                while (!fajl.EndOfStream)
                {
                    fuvarok.Add(new Fuvar(fajl.ReadLine()));
                }
            }

            //3. feladat
            Console.WriteLine($"3. feladat: {fuvarok.Count} fuvar");

            //4. feladat
            Console.WriteLine($"4. feladat: {fuvarok.Count(x=>x.Azonosito == 6185)} fuvar alatt: {fuvarok.Where(x => x.Azonosito == 6185).Sum(y=>y.Viteldij)}$");

            //5. feladat
            Console.WriteLine("5. feladat:");
            fuvarok.GroupBy(x => x.Fizetesmod).Select(y => new
            {
                Fizetesimod = y.Key,
                Fuvarszam = y.Count()
            }).ToList().ForEach(z => Console.WriteLine($"\t{z.Fizetesimod}: {z.Fuvarszam} fuvar"));

            //6. feladat
            Console.WriteLine($"6. feladat: {String.Format("{0:0.00}",fuvarok.Sum(x=>x.Tavolsag) * 1.6)}km");

            //7. feladat
            Fuvar leghosszabbFuvar = fuvarok.Where(x => x.Idotartam == fuvarok.Max(y => y.Idotartam)).First();
            Console.WriteLine($"7. feladat: Leghosszabb fuvar:\n\r\tFuvar hossza: {leghosszabbFuvar.Idotartam} másodperc\n\r\tTaxi azonosító: {leghosszabbFuvar.Azonosito}\n\r\tMegtett távolság: {String.Format("{0:0.0}", leghosszabbFuvar.Tavolsag * 1.6)} km\n\r\tViteldíj: {leghosszabbFuvar.Viteldij}$");

            //8. feladat
            using (StreamWriter fajl = new StreamWriter("hibak.txt", false, Encoding.UTF8))
            {
                fajl.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");
                fuvarok.Where(x=>x.Idotartam > 0 && x.Viteldij > 0 && x.Tavolsag == 0).OrderBy(x => x.Indulas).ToList().ForEach(y=>fajl.WriteLine($"{y.Azonosito};{y.Indulas.ToString("yyyy-MM-dd HH:mm:ss")};{y.Idotartam};{y.Tavolsag};{y.Viteldij};{y.Borravalo};{y.Fizetesmod}"));
            }
            Console.WriteLine("8. feladat: hibak.txt");
            Console.ReadKey();
        }
        class Fuvar
        {
            int azonosito;
            DateTime indulas;
            int idotartam;
            double tavolsag, viteldij, borravalo;
            string fizetesmod;

            public int Azonosito { get => azonosito; set => azonosito = value; }
            public DateTime Indulas { get => indulas; set => indulas = value; }
            public int Idotartam { get => idotartam; set => idotartam = value; }
            public double Tavolsag { get => tavolsag; set => tavolsag = value; }
            public double Viteldij { get => viteldij; set => viteldij = value; }
            public double Borravalo { get => borravalo; set => borravalo = value; }
            public string Fizetesmod { get => fizetesmod; set => fizetesmod = value; }

            public Fuvar(string adatsor)
            {
                string[] adatok = adatsor.Split(';');
                Azonosito = int.Parse(adatok[0].Trim());
                Indulas = DateTime.Parse(adatok[1].Trim());
                Idotartam = int.Parse(adatok[2].Trim());
                Tavolsag = double.Parse(adatok[3].Trim());
                Viteldij = double.Parse(adatok[4].Trim());
                Borravalo = double.Parse(adatok[5].Trim());
                Fizetesmod = adatok[6].Trim();
            }
        }
    }
}
