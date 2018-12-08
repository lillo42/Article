using System.Collections.Generic;
using System.Linq;
using Basic.Model;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using static System.Console;

namespace Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IDocumentStore store = new DocumentStore()
            {
                Urls = new[] {"http://172.17.0.2:8080"},
                Database = "Northwind"
            }.Initialize())
            {
                AddCategories(store);
                ListCategoriesFromDatabase(store);

                AddCompany(store);
                ListCompany(store);
            }
        }

        private static void ListCompany(IDocumentStore store)
        {
            using (IDocumentSession session = store.OpenSession())
            {

                WriteLine(session.Query<Company>().Count(x => x.ExternalId == "ALFKI"));
                WriteLine(session.Query<Company>().FirstOrDefault(x => x.ExternalId == "ALFKI"));
            }
        }

        private static void AddCompany(IDocumentStore store)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                if (session.Query<Company>().Any())
                {
                    return;
                }

                var company = new Company
                {
                    Name = "Wolski Zajazd",
                    ExternalId = "ALFKI",
                    Phone = "(26) 642-7012",
                    Fax = "(26) 642-7011",
                    Contact = new Contact
                    {
                        Name = "Maria Anders",
                        Title = "Sales Representative"
                    },
                    Address = new Address
                    {
                        Line1 = "Obere Str. 57",
                        City = "Berlin",
                        PostalCode = "12209",
                        Country = "Germany",
                        Location = new Location
                        {
                            Latitude = 53.24939,
                            Longitude = 14.43286
                        }
                    }
                };

                session.Store(company);
                session.SaveChanges();
            }
        }

        private static void ListCategoriesFromDatabase(IDocumentStore store)
        {
            WriteLine("List from database categories..");
            using (IDocumentSession session = store.OpenSession())
            {
                foreach (Category category in session.Query<Category>())
                {
                    WriteLine(category);
                }
            }
        }

        private static void AddCategories(IDocumentStore store)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                if (session.Query<Category>().Any())
                {
                    return;
                }

                WriteLine("Saving Categories...");

                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Seafood",
                        Description = "Seaweed and fish"
                    },
                    new Category
                    {
                        Name = "Condiments",
                        Description = "Sweet and savory sauces, relishes, spreads, and seasonings"
                    },
                    new Category
                    {
                        Name = "Confections",
                        Description = "Desserts, candies, and sweet breads"
                    },
                    new Category
                    {
                        Name = "Dairy Products",
                        Description = "Cheeses"
                    },
                    new Category
                    {
                        Name = "Grains / Cereals",
                        Description = "Breads, crackers, pasta, and cereal"
                    },
                    new Category
                    {
                        Name = "Meat / Poultry",
                        Description = "Prepared meats"
                    },
                    new Category
                    {
                        Name = "Produce",
                        Description = "Dried fruit and bean curd"
                    }
                };

                foreach (Category category in categories)
                {
                    session.Store(category);
                }

                session.SaveChanges();

                WriteLine("List save categories...");

                foreach (Category category in categories)
                {
                    WriteLine(category);
                }
            }
        }
    }
}