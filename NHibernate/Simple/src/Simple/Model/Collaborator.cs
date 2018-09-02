using System.Collections.Generic;

namespace Simple.Model
{
    public class Collaborator
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new LinkedList<Book>();
        
        public override string ToString()
        {
            return $"Id:{Id} - Name {Name}";
        }
    }
}