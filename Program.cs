using csharp_ecommerce_db;
using System.Xml.Linq;

using (Ecommerce db = new Ecommerce())
{
    // Create
    Console.WriteLine("Inserire nome:");
    string name = Console.ReadLine();

    Console.WriteLine("\nInserire cognome");
    string surname = Console.ReadLine();

    Console.WriteLine("\nInserire cognome");
    string email = Console.ReadLine();

    Customer newCustomer = NewCustomer(db, name, surname, email);

    // Read
    //Console.WriteLine("Recupero lista di Studenti");
    //List<Student> students = db.Students.OrderBy(student => student.Name).ToList<Student>();
}

Customer NewCustomer(Ecommerce db, string name, string surname, string email)
{
    Customer newCustomer = new Customer { Name = name, Surname = surname, Email = email };

    db.Add(newCustomer);
    db.SaveChanges();

    return newCustomer;
}
