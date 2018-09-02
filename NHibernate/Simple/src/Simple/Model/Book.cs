using System.Collections.Generic;
using System.Text;

namespace Simple.Model
{
    public class Book
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<Collaborator> Collaborators { get; set; } = new LinkedList<Collaborator>();
        
        public override string ToString()
        {
            var collaborators = new StringBuilder();

            foreach (Collaborator collaborator in Collaborators)
            {
                collaborators.AppendLine();
                collaborators.Append("Collaborator:");
                collaborators.Append(collaborator);
            }
            
            return $"Id:{Id} - Name: {Name} - Collaborators: {collaborators}";
        }
    }
}