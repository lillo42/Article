namespace Animal 
{
    public class Animal : IAnimal
    {
        void IAnimal.Dream()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Plant : IPlant
    {

    }

    public class CarnivorousPlant : ICarnivorousPlant
    {
        void IAnimal.Dream()
        {
            throw new System.NotImplementedException();
        }
    }
}