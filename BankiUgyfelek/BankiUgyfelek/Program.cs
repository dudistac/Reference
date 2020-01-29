using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace BankiUgyfelek
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ma kettő ügyfél jelentkezett be. Adataik a következőek:
            Ugyfel ElsoUgyfel = new Ugyfel("Gaspar Elek", 26,"+36/70-123-4567", "Budapest", 11900000, 507125.00, 75000.00, "Ervenyes");
            Ugyfel MasodikUgyfel = new Ugyfel("Bármi Áron", 15, "Nincs", "Debrecen", 11900001, 52400, 10000, "Érvényes");

            //Első ügyfelet érdekli a nyílvántartott adatai és 10000 Ft-nyi hitelt szeretne felvenni 4 évre 6 %-os kamattal, ha lehetséges akkor kíváncsi a törlesztőrészletre
            ElsoUgyfel.UgyfelAdatok();
            ElsoUgyfel.HitelFelvetel(10000);
            Console.WriteLine("A törlesztőrészlet az " + Ugyfel.TorlesztoReszlet(10000, 48, 0.06) + " Ft havonta.");
            ElsoUgyfel.fLekerdezes();

            //Második ügyfél afelől érdeklődik, hogy milyen típusú a számlája. Vehet-e fel hitelt. Illetve fel szeretne tölteni 2500 Ft-ot a számlájára.
            MasodikUgyfel.fSzamlatipus();
            MasodikUgyfel.lehetEHitel();
            MasodikUgyfel.fBefizetes(2500);
            MasodikUgyfel.fLekerdezes();

            //Az első ügyfélnek hány nap múlva nem maradna pénze ha naponta 50000 Ft-ot költene?
            int napokSzama = 0;

            while (ElsoUgyfel.egyenleg > 0)
            {
                ElsoUgyfel.egyenleg -= 50000;
                napokSzama++;
            }
            Console.WriteLine(napokSzama+ " nap múlva fogy majd el a pénze.");

            //A ma bejelentkezett ügyfeleket számlaszám alapján eltároljuk és kiírjuk.
            List<int> maiUgyfelek = new List<int>();
            maiUgyfelek.Add(ElsoUgyfel.szamlaszam);
            maiUgyfelek.Add(MasodikUgyfel.szamlaszam);
            for (int j = 0; j < 2; j++) 
            {
                Console.WriteLine(maiUgyfelek[j]);
            }




            Console.ReadLine();
        }
    }
}
