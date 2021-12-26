namespace DiDi
{
    class Program
        {
            static void Main(string[] args)
            {
                var container = new DiDi();
                container.AddUnstable<IA, A>();
                container.AddUnstable<IB, B>();
                container.Get<IB>();
            }
        }
}