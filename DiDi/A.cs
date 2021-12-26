using System;

namespace DiDi
{
    public class A : IA
    {
        public A(IB b) { }
        public void showA()
       {
            Console.WriteLine("A");
        }
    }
}