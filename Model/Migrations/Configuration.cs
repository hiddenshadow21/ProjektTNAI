namespace Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Model.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Model.AppDbContext context)
        {
            if (!context.Authors.Any(x => x.FirstName == "Jan" && x.LastName == "Kowalski"))
            {
                context.Authors.Add(
                    new Entities.Author()
                    {
                        FirstName = "Jan",
                        LastName = "Kowalski"
                    });
                context.SaveChanges();
            }

            if (!context.Books.Any(x => x.Name == "Nowoczesne Aplikacje"))
            {
                var authorId = context.Authors.Where(x => x.FirstName == "Jan" && x.LastName == "Kowalski")
                    .Select(x => x.ID).First();
                context.Books.Add(
                 new Entities.Book()
                 {
                     Name = "Nowoczesne Aplikacje",
                     AuthorID = authorId,
                     Amount = 1
                 });
                context.SaveChanges();
            }

            if(!context.Borrowers.Any(x=> x.FirstName == "Kan" && x.LastName == "Jowalski"))
            {
                context.Borrowers.Add(
                    new Entities.Borrower()
                    {
                        FirstName = "Kan",
                        LastName = "Jowalski"
                    });
                context.SaveChanges();
            }

            if(!context.Loans.Any(x => x.ID == 1))
            {
                var borrowerId = context.Borrowers.Where(x => x.FirstName == "Kan" && x.LastName == "Jowalski")
                    .Select(x => x.ID).First();
                var bookId = context.Books.Where(x => x.Name == "Nowoczesne Aplikacje")
                    .Select(x => x.ID).First();
                context.Loans.Add(
                    new Entities.Loan()
                    { 
                        BorrowerID = borrowerId,
                        BookID = bookId,
                        LoanStart = DateTime.Now
                    });
            }
        }
    }
}
