namespace Basic.Model
{
    public class Contact
    {
        public string Name { get; set; }
        public string Title { get; set; }
        
        public override string ToString() 
            => $"{Name} - {Title}";
    }
}