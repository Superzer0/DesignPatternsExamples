using System;
using WzorceLib;
using WzorceLib.Czynnosciowe;
using WzorceLib.Kreacyjne;
using WzorceLib.Strukturalne;
using WzorceProjekoweZadania.ExpressionTrees;
using WzorceProjekoweZadania.LazyFun;

namespace WzorceProjekoweZadania
{
    static class Program
    {
        static void Main(string[] args)
        {
            IExampleRunnable runner;
            //runner = new ();
            runner = new MostRunner();
            
            runner.Run();
            Console.ReadKey();
        }
    }
}
