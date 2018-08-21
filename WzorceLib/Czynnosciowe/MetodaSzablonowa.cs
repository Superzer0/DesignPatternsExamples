using System;

namespace WzorceLib.Czynnosciowe
{
    public class PoloczenieBd
    {
        public string ParametryPolaczenie { get; private set; }

        public PoloczenieBd(string parametryPolaczenie)
        {
            ParametryPolaczenie = parametryPolaczenie;
        }

        public void WykonajZapytanie(string zapytanie)
        {
            Console.WriteLine("Polaczenie nastapilo z parametrami : {0}", ParametryPolaczenie);

            Console.WriteLine("Wykonano polecenie sql : {0}", zapytanie);
        }

    }


    public abstract class SzablonZapytania
    {
        public void WykonajZapyanie(string bazaDanych, string zapytanie)
        {
            var polaczenieBD = new PoloczenieBd(FormatujConnect(bazaDanych));
            var komendaBD = FormatujSelect(zapytanie);
            polaczenieBD.WykonajZapytanie(komendaBD);
        }

        protected abstract string FormatujConnect(string nazwaBazy);
        protected abstract string FormatujSelect(string zapytanie);
    }


    public class ZapytanieOracle : SzablonZapytania
    {
        protected override string FormatujConnect(string nazwaBazy)
        {
            return string.Format("Data Source={0};User Id=myUsername;Password=myPassword;Integrated Security=no;",
                 nazwaBazy);
        }

        protected override string FormatujSelect(string zapytanie)
        {
            return zapytanie + ";";
        }
    }


    public class ZapytanieSQLServer : SzablonZapytania
    {
        protected override string FormatujConnect(string nazwaBazy)
        {
            return string.Format("Server=myServerAddress;Database={0};User Id=myUsername; Password=myPassword;",
                nazwaBazy);
        }

        protected override string FormatujSelect(string zapytanie)
        {
            return zapytanie;
        }
    }


    public class MetodaSzablonowa : IExampleRunnable
    {
        public void Run()
        {
            var sql = "select * from table";

            SzablonZapytania zapytanie = new ZapytanieOracle();
            zapytanie.WykonajZapyanie("NEWORCL", sql);

            zapytanie = new ZapytanieSQLServer();
            zapytanie.WykonajZapyanie("MyDB", sql);
        }
    }
}
