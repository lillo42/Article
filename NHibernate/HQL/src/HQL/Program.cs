using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using HQL.Model;
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
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    @"Data Source=localhost;Initial Catalog=NHTest; User Id=sa;Password=Hello@123")
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
                foreach (Book book in session.Query<Book>())
                {
                    WriteLine(book);
                }

                foreach (Publisher p in session.Query<Publisher>())
                {
                    WriteLine(p);
                }
            }

            using (ISession session = sessionFactory.OpenSession())
            {
                session.CreateQuery($"DELETE FROM {nameof(Book)} B WHERE B.{nameof(Book.Id)} = 10")
                    .ExecuteUpdate();

                session.CreateQuery($"DELETE FROM {nameof(Book)} B WHERE B.{nameof(Book.Id)} = :id")
                    .SetParameter("id", 2)
                    .ExecuteUpdate();

                foreach (Book book in session.Query<Book>())
                {
                    WriteLine(book);
                }

                foreach (Publisher p in session.Query<Publisher>())
                {
                    WriteLine(p);
                }
            }

            ReadLine();
        }
    }
}