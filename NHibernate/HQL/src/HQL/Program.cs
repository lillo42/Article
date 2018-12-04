using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using HQL.Model;
using NHibernate.Linq;
using static System.Console;

namespace HQL
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Create and configuration database");

            ISessionFactory sessionFactory = Fluently
                .Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString(
                        @"Server=localhost;Database=nhibernate; User Id=postgres;Password=Hello@123")
                    .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                .ExposeConfiguration(config => new SchemaUpdate(config).Execute(true, true))
                .BuildSessionFactory();

            WriteLine("Saving Author and Book");

            using (ISession session = sessionFactory.OpenSession())
            {
                var publisher = new Publisher
                {
                    Name = "Amazing Pixel"
                };

                session.Save(publisher);

                WriteLine($"Creating Author:{publisher}");

                var author = new Author
                {
                    Name = "Affonso Solano"
                };

                WriteLine($"Creating Author:{author}");

                session.Save(author);

                var book = new Book
                {
                    Name = "O Espadachim de Carvão",
                    Author = author,
                    Publisher = publisher
                };

                WriteLine($"Creating Book:{book}");

                session.Save(book);
                session.Flush();
                WriteLine("Saved on database");
                WriteLine($"Author: {author} - Book: {book}");
            }

            using (ISession session = sessionFactory.OpenSession())
            {
                var publisher = new Publisher
                {
                    Name = "Leya"
                };

                session.Save(publisher);

                var author = new Author
                {
                    Name = "Eduardo Spohr"
                };

                WriteLine($"Creating Author:{author}");

                session.Save(author);

                var jovemNerd = new Collaborator
                {
                    Name = "Jovem Nerd"
                };

                WriteLine($"Creating Collaborator:{jovemNerd}");
                session.Save(jovemNerd);

                var azaghal = new Collaborator
                {
                    Name = "Azaghal"
                };

                WriteLine($"Creating Collaborator:{azaghal}");
                session.Save(azaghal);

                var book = new Book
                {
                    Name = "A Batalha do Apocalipse",
                    Author = author,
                    Publisher = publisher,
                };

                book.Collaborators.Add(jovemNerd);
                book.Collaborators.Add(azaghal);

                WriteLine($"Creating Book:{book}");

                session.Save(book);
                session.Flush();
                WriteLine("Saved on database");
                WriteLine($"Author: {author} - Book: {book}");
            }

            using (ISession session = sessionFactory.OpenSession())
            {
                WriteLine("List Publisher:");

                foreach (Publisher p in session.Query<Publisher>())
                {
                    WriteLine(p);
                }

                WriteLine("List Book:");

                foreach (Book book in session.Query<Book>())
                {
                    WriteLine(book);
                }

                WriteLine("List Publisher:");

                foreach (Publisher p in session.Query<Publisher>())
                {
                    WriteLine(p);
                }
            }

            using (ISession session = sessionFactory.OpenSession())
            {
                session.CreateQuery($"DELETE FROM {nameof(Author)} A WHERE A.{nameof(Author.Id)} = 10")
                    .ExecuteUpdate();

                session.CreateQuery($"DELETE FROM {nameof(Author)} A WHERE A.{nameof(Author.Id)} = :id")
                    .SetParameter("id", 10)
                    .ExecuteUpdate();

                foreach (Book book in session.Query<Book>())
                {
                    WriteLine(book);
                    session.Delete(book);
                }

                session.Flush();

                foreach (Publisher p in session.Query<Publisher>())
                {
                    WriteLine(p);
                }

                session.CreateQuery($"DELETE FROM {nameof(Collaborator)}")
                    .ExecuteUpdate();

                session.CreateQuery($"DELETE FROM {nameof(Publisher)} P")
                    .ExecuteUpdate();

                session.CreateQuery($"DELETE FROM {nameof(Author)} P")
                    .ExecuteUpdate();
                
                session.Flush();
            }
        }
    }
}