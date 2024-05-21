using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktiskais14
{
    class StudijuKurss
    {
        // Klases īpašības
        public string Nosaukums { get; set; }
        public int Kreditpunkti { get; set; }
        public double EKreditpunkti { get { return Kreditpunkti * 1.5; } } // Papildus kredītpunkti
        public bool IrObligats { get; set; }

        // Klases funkcija, kas nolasa datus no lietotāja
        public void ReadData()
        {
            Console.Write("Ievadiet kursa nosaukumu: ");
            Nosaukums = Console.ReadLine();
            Console.Write("Ievadiet kredītpunktu skaitu: ");
            Kreditpunkti = int.Parse(Console.ReadLine());
            Console.Write("Vai kursam ir obligātais statuss (true/false): ");
            IrObligats = bool.Parse(Console.ReadLine());
        }

        // Klases funkcija, kas izvada datus uz ekrāna
        public void PrintData()
        {
            Console.WriteLine($"Nosaukums: {Nosaukums}");
            Console.WriteLine($"Kredītpunkti: {Kreditpunkti}");
            Console.WriteLine($"Papildus kredītpunkti: {EKreditpunkti}");
            Console.WriteLine($"Obligātais statuss: {IrObligats}");
        }
    }

    class Program
    {
        // Aizpilda masīvu ar klases StudijuKurss objektiem, izmantojot ReadData funkciju
        static void FillArray(StudijuKurss[] kurss)
        {
            for (int i = 0; i < kurss.Length; i++)
            {
                kurss[i] = new StudijuKurss();
                kurss[i].ReadData();
            }
        }

        // Izvada visu masīva elementu datus uz ekrāna, izmantojot PrintData funkciju
        static void PrintArray(StudijuKurss[] kurss)
        {
            foreach (var kurs in kurss)
            {
                kurs.PrintData();
            }
        }

        // Saglabā masīva datus failā
        // saņem kā parametru StudijuKurss tipa
        // masīvu un saglabā masīva vērtības.txt failā, kur katru īpašību atdala ar „ ; ” vai citu simbolu;
        static void PrintArrayToFile(StudijuKurss[] kurss)
        {
            using (StreamWriter writer = new StreamWriter("kurss_info.txt"))
            {
                foreach (var kurs in kurss)
                {
                    // Katru īpašību atdala ar ';' un rindiņu beigās ieraksta jaunu rindiņu
                    writer.WriteLine($"{kurs.Nosaukums};{kurs.Kreditpunkti};{kurs.EKreditpunkti};{kurs.IrObligats}");
                }
            }
            Console.WriteLine("Informācija saglabāta failā kurss_info.txt");
        }

        // Nolasa masīva datus no faila un atgriež StudijuKurss masīvu
        // nolasa saglabāto .txt failu un saglāba vērtības StudijuKurss tipa masīvā(StudijuKurss[]).
        static StudijuKurss[] ReadArrayFromFile()
        {
            string[] lines = File.ReadAllLines("kurss_info.txt");
            StudijuKurss[] kurss = new StudijuKurss[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(';');
                kurss[i] = new StudijuKurss
                {
                    Nosaukums = data[0],
                    Kreditpunkti = int.Parse(data[1]),
                    IrObligats = bool.Parse(data[3])
                };
            }
            return kurss;
        }

        static void Main(string[] args)
        {
            // Izveido pirmo StudijuKurss masīvu
            StudijuKurss[] pirmaisKurss = new StudijuKurss[2];

            // Aizpilda un saglabā informāciju failā
            FillArray(pirmaisKurss);
            PrintArrayToFile(pirmaisKurss);

            // Izveido otro StudijuKurss masīvu un nolasa informāciju no faila
            StudijuKurss[] otraisKurss = ReadArrayFromFile();

            // Izvada informāciju uz ekrāna
            PrintArray(otraisKurss);
        }
    }
}
