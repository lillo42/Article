using static System.Console;

namespace Animal 
{
    public abstract class BaseAnimal : IAnimal
    {
        void IAnimal.Dream()
        {
            throw new System.NotImplementedException();
        }
    }

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
        override void Feed() 
        {
            WriteLine("Taking a sun....");
        }
    }
}