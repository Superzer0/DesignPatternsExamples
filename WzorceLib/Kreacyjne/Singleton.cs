using System;

namespace WzorceLib.Kreacyjne
{
    public sealed class Singleton
    {
        private static volatile Singleton _instance;
        private static readonly object Lock = new object();
        private readonly int _myID;
        private Singleton()
        {
            _myID = new Random().Next();
        }

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance =  new Singleton();
                            return _instance;
                        }
                    }
                }
                return _instance;
            }
            
        }

        public string GenerateSomeText()
        {
            return string.Format("I am Singleton and I am the only one. My ID : {0}", _myID);
        }
    }

    public class SingletonRun : IExampleRunnable
    {
        public void Run()
        {
            var singleton1 = Singleton.Instance;
            Console.WriteLine(singleton1.GenerateSomeText());

            var singleton2 = Singleton.Instance;
            Console.WriteLine(singleton2.GenerateSomeText());

        }
    }





}
