using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzorceLib.Strukturalne
{
    public class Towar
    {
        public double Cena { get; set; }
        public string Nazwa { get; set; }
        public int Ilosc { get; set; }
    }

    public class Zamowienie
    {
        private IObliczPodatek _algorytmPodatku;
        private KrajRozliczania _krajRozliczania;
        public Zamowienie(Towar[] towary)
        {
            Towary = towary;
            _algorytmPodatku = new PodatekPolska();

        }

        public KrajRozliczania KrajRozliczania
        {
            get { return _krajRozliczania; }
            set
            {
                if (value != _krajRozliczania)
                {
                    _krajRozliczania = value;
                    if (KrajRozliczania.Niemcy == value)
                    {
                        _algorytmPodatku = new PodatekNiemcy();
                    }
                    else if (KrajRozliczania.Niemcy == value)
                    {
                        _algorytmPodatku = new PodatekNiemcy();
                    }

                }
            }
        }

        public Towar[] Towary { get; private set; }

        public double ObliczPodatek()
        {
            return Towary.Aggregate(0.0, (c, t) => (c + _algorytmPodatku.KwotaPodatku(t)));
        }

    }

    public interface IObliczPodatek
    {
        double KwotaPodatku(Towar przedmiotSprzedarzy);
    }

    public enum KrajRozliczania
    {
        Niemcy, Polska
    }

    public class PodatekNiemcy : IObliczPodatek
    {
        public double KwotaPodatku(Towar przedmiotSprzedarzy)
        {
            return (przedmiotSprzedarzy.Cena * przedmiotSprzedarzy.Ilosc * 0.2) + 7;
        }
    }

    public class PodatekPolska : IObliczPodatek
    {
        public double KwotaPodatku(Towar przedmiotSprzedarzy)
        {
            return (przedmiotSprzedarzy.Cena * przedmiotSprzedarzy.Ilosc * 0.3) + 5;
        }
    }

    public class Strategia : IExampleRunnable
    {
        public void Run()
        {
            var towary = new[]
            {
                new Towar() {Cena = 10, Ilosc = 2, Nazwa = "Czekolada"},
                new Towar() {Cena = 5, Ilosc = 1, Nazwa = "Długopis"},
                new Towar() {Cena = 6, Ilosc = 2, Nazwa = "Ołowek"},
                new Towar() {Cena = 7, Ilosc = 3, Nazwa = "Zeszyt"},
                new Towar() {Cena = 8, Ilosc = 4, Nazwa = "Skarpetki"}
            };


            var zamowienie = new Zamowienie(towary);

            zamowienie.KrajRozliczania = KrajRozliczania.Polska;
            var podatek = zamowienie.ObliczPodatek();
            Console.WriteLine("Podatek do zapłacenia w Polsce : {0}", podatek);


            zamowienie.KrajRozliczania = KrajRozliczania.Niemcy;
            podatek = zamowienie.ObliczPodatek();
            Console.WriteLine("Podatek do zapłacenia w Niemczech : {0}", podatek);

        }
    }

}
