using System;

namespace MicrosoftDI.Sample.Services
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IGux { }

    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }
    public class Gux : IGux
    {
        public Gux(IFoo foo)
        {
            Console.WriteLine("Gux(IFoo)");
        }

        public Gux(IFoo foo, IBar bar)
        {
            Console.WriteLine("Gux(IFoo, IBar)");
        }

        public Gux(IBar bar, IBaz baz)
        {
            Console.WriteLine("Gux(IFoo, IBar, IBaz)");
        }
    }
}
