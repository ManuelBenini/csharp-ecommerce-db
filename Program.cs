using csharp_ecommerce_db;
using System.Xml.Linq;

using (Ecommerce db = new Ecommerce())
{
    //Console.WriteLine("Caricare dati DB? Si(1) No(0)");
    //if (Convert.ToInt32(Console.ReadLine()) == 1)
    //{
    //    DefaultProduct(db);
    //    DefaultEmployee(db);
    //}

    // Create Customer
    Console.WriteLine("Inserire nome:");
    string name = Console.ReadLine();

    Console.WriteLine("\nInserire cognome");
    string surname = Console.ReadLine();

    Console.WriteLine("\nInserire email");
    string email = Console.ReadLine();

    Customer newCustomer = NewCustomer(db, name, surname, email);

    Order order = new Order { Customer = newCustomer };

    db.Add(order);

    // Search product & Create Order and Payment

    FindProduct(db, order);

    
}

Customer NewCustomer(Ecommerce db, string name, string surname, string email)
{
    Customer newCustomer = new Customer { Name = name, Surname = surname, Email = email };

    db.Add(newCustomer);
    db.SaveChanges();

    return newCustomer;
}

void FindProduct(Ecommerce db, Order order)
{
    Console.WriteLine("\nInserire nome prodotto:");
    string name = Console.ReadLine();

    Product product = db.Products.Where(product => product.Name == name).First();

    Console.WriteLine($"\nNome prodotto: {product.Name}");
    Console.WriteLine($"Descrizione: {product.Description}");
    Console.WriteLine($"Prezzo: {product.Price}");

    Console.WriteLine("\nAggiungere al carrello(1) O andare al riepilogo dell'ordine?(2)");

    switch (Convert.ToInt32(Console.ReadLine()))
    {
        case 1:
            AddProductToOrder(db, product, order);
            break;

        default:
            break;
    }
}

void AddProductToOrder(Ecommerce db, Product product, Order order)
{
    Payment payment = new Payment { Date = DateTime.Now, Amount = product.Price, Status = "Working" };

    var random = new Random().Next(1,3);

    Employee employee = db.Employees.Where(employee => employee.EmployeeId == random).First();

    order.Date = DateTime.Now;
    order.Amount += product.Price;
    order.Status = "Working";

    order.Employee = employee;
    order.ProductsOrdered.Add(product);
    db.Orders.Add(order);


    payment.Order = order;
    db.Payment.Add(payment);


    db.SaveChanges();
}






void DefaultProduct(Ecommerce db)
{
    Product newProduct1 = new Product { Name = "Scatola", Description = "La scatola di pandora", Price = 15 };
    Product newProduct2 = new Product { Name = "Tonno", Description = "Tonno in scatola", Price = 10 };
    Product newProduct3 = new Product { Name = "Cicca", Description = "Sigaretta", Price = 20 };

    db.Add(newProduct1);
    db.Add(newProduct2);
    db.Add(newProduct3);

    db.SaveChanges();
}

void DefaultEmployee(Ecommerce db)
{
    Employee newEmployee1 = new Employee { Name = "John", Surname = "Snow", Level = "Comandante" };
    Employee newEmployee2 = new Employee { Name = "Mirko", Surname = "Cannati", Level = "Impiegato" };
    Employee newEmployee3 = new Employee { Name = "Andrea", Surname = "Cancilla", Level = "Spazzino" };

    db.Add(newEmployee1);
    db.Add(newEmployee2);
    db.Add(newEmployee3);

    db.SaveChanges();
}
