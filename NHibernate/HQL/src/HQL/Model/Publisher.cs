using System.Collections.Generic;

namespace HQL.Model
{
    public class Publisher
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new LinkedList<Book>();

        public override string ToString() => Name;
    }
}
