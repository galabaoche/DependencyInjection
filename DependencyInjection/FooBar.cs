using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IGux
    {
        IFoo Foo { get; }
        IBar Bar { get; }
        IBaz Baz { get; }
    }

    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }
    public class Gux : IGux
    {
        public IFoo Foo { get; private set; }
        public IBar Bar { get; private set; }
        public IBaz Baz { get; private set; }
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

        }
        public Gux(IFoo foo, IBar bar, IBaz baz)
        {
            this.Foo = foo;
            this.Bar = bar;
            this.Baz = baz;
        }
    }

    public interface IFoobar { }
    public class Foobar1 : IFoobar { }
    public class Foobar2 : IFoobar { }

    public interface IBazGux<T1, T2>
    {
        T1 BazT { get; }
        T2 GuxT { get; }
    }

    public class BazGux<T1, T2> : IBazGux<T1, T2>
    {
        public T1 BazT { get; private set; }
        public T2 GuxT { get; private set; }

        public BazGux(T1 bazT, T2 guxT)
        {
            this.BazT = bazT;
            this.GuxT = guxT;
        }
    }

    public interface IFooDispose { }
    public interface IBarDispose { }
    public interface IBazDispose { }

    public class FooDispose : Disposable, IFooDispose { }
    public class BarDispose : Disposable, IBarDispose { }
    public class BazDispose : Disposable, IBazDispose { }

    public class Disposable : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("{0}.Dispose()", this.GetType());
        }
    }
    public interface IFoobarbaz : IDisposable
    { }

    public class Foobarbaz : IFoobarbaz
    {
        ~Foobarbaz()
        {
            Console.WriteLine("Foobar.Finalize()");
        }

        public void Dispose()
        {
            Console.WriteLine("Foobar.Dispose()");
        }
    }
}
