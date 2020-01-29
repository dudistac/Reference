using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankiUgyfelek
{[Serializable]
    class Ugyfel
    {


        //Ügyfelek adatai
        public string nev;
        public int kor;
        public string elerhetoseg;
        public string lakhely;
        public int szamlaszam;
        public double egyenleg;
        public double maxOsszeg; //A maximálisan felvehető összeg egy tranzakció folyamán
        private string szamlatipus;
        

        //A konsztruktor
        public Ugyfel(string uNev, int uKor, string uElerhetoseg, string uLakhely, int uSzamlaszam, double uEgyenleg, double uMaxOsszeg, string uSzamlatipus)
        {
            nev = uNev;
            kor = uKor;
            elerhetoseg = uElerhetoseg;
            lakhely = uLakhely;
            szamlaszam = uSzamlaszam;
            egyenleg = uEgyenleg;
            maxOsszeg = uMaxOsszeg;
            Szamlatipus = uSzamlatipus;
            
        }

        //A számlatípus kor alapján határozódik meg
        public string Szamlatipus
        {
            get { return szamlatipus; }
            set {
                if (kor < 24 && kor > 14)
                {
                    szamlatipus = "Diákszámla";
                }
                else if (kor>=24) 
                {
                    szamlatipus = "Érvényes";
                }
                else
                {
                    szamlatipus = "Érvénytelen";
                }
            }
        }

        //Kiírja az ügyfél legfontosabb adatait
        public void UgyfelAdatok() 
        {
            Console.WriteLine(String.Format("Az ügyfél neve: {0}, a lakhelye: {1}, a számlaszáma: {2}.",nev, lakhely, szamlaszam));
        }

        //Befizetés a számlára
        public void fBefizetes(double BefOsszeg) 
        {
            this.egyenleg += BefOsszeg;    
        }

        //Egyenleg lekérdezése
        public void fLekerdezes() 
        {
            Console.WriteLine("Az egyenleg: " + egyenleg + " Ft.");
        }

        //Pénz felvétel számláról
        public double fPenzFelvetel(double LevOsszeg) 
        {
            if (egyenleg < LevOsszeg)
            {
                Console.WriteLine("Nem vehető le az előírt pénzösszeg, mert nincs elég pénz a számlán!");
                return egyenleg;
            }else { 
                 //egyenleg -= LevOsszeg;
                Console.WriteLine("A tranzakció sikeresen végrehajtva!");
                return egyenleg-=LevOsszeg;
            }
        }

            

        //Adott esetben a hitelfelvétel feltétele az, hogy az ügyfélnek idősebbnek kell lennie 18-nál
        //ebből aódódan a számlatípusának érvényesnek kell lennie,
        //a biztos anyagi hátteret pedig a számlán található pénzösszeg mennyisége határozza meg
        public bool lehetEHitel() 
        {
            if (egyenleg >= 100000 && Szamlatipus=="Érvényes" && kor>=18) {
                Console.WriteLine("Hitel felvétele lehetséges!");
                return true;
            }
            Console.WriteLine("Az ügyfél nem vehet fel hitelt!");
            return false;
        }

        //Ebben az esetben a hitelvelvétel feltétele, hogy az igényelt összeg nem lehet nagyobb az egyenleg 2.5-szörösénél
        public void HitelFelvetel(double igenyeltOsszeg) 
        {
            if (lehetEHitel() == true && igenyeltOsszeg <= egyenleg * 2.5)
            {
                egyenleg += igenyeltOsszeg;
                Console.WriteLine("Az összeg sikeresen felvéve!");
            }
            else 
            { 
                Console.WriteLine("Az összeg felvétele sikertelen!");
            }
            
        }

        //A törlesztőrészlet számításánál a futamidőt hónapokban kell megadni, a kamatlábat kettő tizedesjegyig.
        public static double TorlesztoReszlet(double felvettOsszeg, int futamido, double kamatláb)
        {
            double kamatlab = kamatláb / 12;
            double annuitasFaktor = 1 / kamatlab - (1 / (kamatlab * Math.Pow(1 + kamatlab, futamido)));
            double torlesztoReszlet = felvettOsszeg / annuitasFaktor;
            return torlesztoReszlet;
        }

        public void fSzamlatipus()
        {
            Console.WriteLine(szamlatipus);
        }
        


    }
}
