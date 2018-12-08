namespace Basic.Model
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name} - {Description}";
        }
    }
}