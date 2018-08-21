using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace WzorceLib.Strukturalne
{

    public abstract class Figura
    {
        public abstract void Wyswietl();
    }

    public class Punkt : Figura
    {
        public override void Wyswietl()
        {
            Console.WriteLine("Rysuję punkt z {0}", GetType());
        }
    }

    public class Linia : Figura
    {
        public override void Wyswietl()
        {
            Console.WriteLine("Rysuję linie z {0}", GetType());
        }
    }


    public class Kwadrat : Figura
    {
        public override void Wyswietl()
        {
            Console.WriteLine("Rysuję kwadrat z {0}", GetType());
        }
    }


    public class Okrag : Figura
    {
        private XXOkrag _xxOkrag = new XXOkrag();

        public override void Wyswietl()
        {
            _xxOkrag.wyswietlaj();
        }
    }


    
    public class XXOkrag
    {
        public void wyswietlaj()
        {
            Console.WriteLine("Rysuję okrag z {0}", GetType());
        }
    }



    public class AdapterRunner : IExampleRunnable
    {
        public void Run()
        {
            var listaFigur = new List<Figura>()
            {
                new Kwadrat(),
                new Linia(),
                new Punkt(),
                new Okrag()
            };

            listaFigur.ForEach(f => f.Wyswietl());
        }
    }

}
