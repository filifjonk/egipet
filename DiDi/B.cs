using System;

namespace DiDi
{
    public class B : IB
    {
        public B(IA a)
        {
        }

        public void showB()
        {
            Console.WriteLine("B");
        }
    }
}