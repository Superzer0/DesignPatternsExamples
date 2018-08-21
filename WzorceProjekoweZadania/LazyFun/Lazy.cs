using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WzorceLib;
using System.Collections.Concurrent;

namespace WzorceProjekoweZadania.LazyFun
{

    public class Foo
    {
        public int ID { get; set; }
        public Foo()
        {
            Thread.Sleep(new Random().Next(1000,2000));
            Console.WriteLine("Foo:: Constructor is called A : {0}", ID);
        }
    }


    public class Lazy : IExampleRunnable
    {
        public static void Runnable(object obj)
        {
            Lazy<Foo> flazy = obj as Lazy<Foo>;
            Foo f = flazy.Value;
            Console.WriteLine("I am " + Thread.CurrentThread.GetHashCode());
            Console.WriteLine("but won : " + f.ID);
        }

        void IExampleRunnable.Run()
        {
            Lazy<Foo> f = new Lazy<Foo>(() =>
            {
                return new Foo() { ID = Thread.CurrentThread.GetHashCode() };

            }, LazyThreadSafetyMode.ExecutionAndPublication);

            ThreadPool.QueueUserWorkItem(new WaitCallback(Runnable), f);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Runnable), f);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Runnable), f);

            ISet<int> set = new SortedSet<int>();
            var col = new Collection<int>();
            col.Add(5);

            IProducerConsumerCollection<int> producerConsumerCollection = new ConcurrentBag<int>();
            IReadOnlyCollection<int> r = new ReadOnlyCollection<int>(new List<int>());
            
        }
    }
}
