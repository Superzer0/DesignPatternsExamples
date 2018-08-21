using System;
using System.Diagnostics;

namespace WzorceLib.Strukturalne
{
    public class Wektor3D<T> where T : struct
    {
        public Wektor3D(T a, T b, T c)
        {
            A = a;
            B = b;
            C = c;
        }

        public T A { get; private set; }
        public T B { get; private set; }
        public T C { get; private set; }

        public static Wektor3D<T> operator +(Wektor3D<T> v1, Wektor3D<T> v2)
        {
            return new Wektor3D<T>
                (Sum(v1.A, v2.A), Sum(v1.B, v2.B), Sum(v1.C, v2.C));
        }

        public static Wektor3D<T> operator *(Wektor3D<T> v1, Wektor3D<T> v2)
        {
            return new Wektor3D<T>
                (Multiply(v1.A, v2.A), Multiply(v1.B, v2.B), Multiply(v1.C, v2.C));
        }

        private static T Sum(T a, T b)
        {
            return (dynamic)a + (dynamic)b;
        }

        private static T Multiply(T a, T b)
        {
            return (dynamic)a * (dynamic)b;
        }
    }


    public class Wektor2D
    {
        public int A
        {
            get { return _wektor3D.A; }
        }

        public int B
        {
            get { return _wektor3D.B; }
        }

        private Wektor3D<int> _wektor3D;

        public Wektor2D(int a, int b)
        {
            _wektor3D = new Wektor3D<int>(a, b, 0);
        }

        public static Wektor2D operator +(Wektor2D v1, Wektor2D v2)
        {
            var resultVector = new Wektor3D<int>(v1.A, v1.B, 0) + new Wektor3D<int>(v2.A, v2.B, 0);
            return new Wektor2D(resultVector.A, resultVector.B);
        }

        public static Wektor2D operator *(Wektor2D v1, Wektor2D v2)
        {
            var resultVector = new Wektor3D<int>(v1.A, v1.B, 0) * new Wektor3D<int>(v2.A, v2.B, 0);
            return new Wektor2D(resultVector.A, resultVector.B);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", A, B);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var wektor = obj as Wektor2D;
            if (wektor == null)
            {
                return false;
            }

            return (A == wektor.A) && (B == wektor.B);
        }

        public override int GetHashCode()
        {
            return A ^ B;
        }
    }

    public class FasadaRunner : IExampleRunnable
    {
        public void Run()
        {
            var v1 = new Wektor2D(1, 2);
            var v2 = new Wektor2D(2, 3);

            var sumVectorAssertion = new Wektor2D(3, 5);
            var sumResult = v1 + v2;

            Console.WriteLine("{0} + {1} = {2}", v1, v2, sumResult);
            Debug.Assert(sumVectorAssertion.Equals(sumResult), "Dodawanie wektorow 2d niepoprawne");



            var multiplyVectorResult = new Wektor2D(2, 6);
            var multiplyResult = v1 * v2;

            Console.WriteLine("{0} * {1} = {2}", v1, v2, multiplyResult);
            Debug.Assert(multiplyVectorResult.Equals(v1 * v2), "Mnozenie wektorow 2d niepoprawne");

        }
    }

}
