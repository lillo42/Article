using static System.Console;

namespace Animal 
{
    public interface IAnimal 
    {
        static int FeedCounter = 0;
        void Feed()
        {
            FeedCounter++;
            Write("Eating....");
            WriteLine($"Ate {FeedCounter} times");
        }

        void Walk()
        {
            WriteLine("Walkig....");
        }
        void Dance()
        {
            WriteLine("Dancing....");
        }
        
        void Sleep()
        {
            WriteLine("Sleeping....");
            Dream();
            WriteLine("Sleeping....");
        }

        protected abstract void Dream();
    }

    public interface IPlant : IAnimal
    {
        void IAnimal.Feed()
        {
            WriteLine("Taking a sun....");
        }

        void IAnimal.Sleep()
        {
            WriteLine("Wee don't sleep....");
        }

        void IAnimal.Dream() 
        {
            WriteLine("Wee don't sleep....");
        }
    }

    public interface ICarnivorousPlant : IPlant
    {
        const string GROUP = "";
        void IAnimal.Feed()
        {
            WriteLine("Eating....");
        }

        void IAnimal.Sleep()
        {
            base(IAnimal).Sleep();
        }

        abstract void IAnimal.Dream();
    }
}