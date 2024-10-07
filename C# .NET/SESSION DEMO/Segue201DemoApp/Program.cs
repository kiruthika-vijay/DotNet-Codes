using Segue201DemoApp.entity;
using System.Data.Entity;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;
namespace Segue201DemoApp
{
    public class Program
    {
        #region Method Definition for Json Serialization - Product
        public static string SerializeToJson(Product product)
        {
            return JsonSerializer.Serialize(product);
        }
        public static Product DeSerializeToJson(string json)
        {
            return JsonSerializer.Deserialize<Product>(json);
        }
        #endregion

        #region Method Definition for Json Serialization - Customers
        public static string SerializeToJsonCustomer(Customers customers)
        {
            return JsonSerializer.Serialize(customers);
        }
        public static Customers DeSerializeToJsonCustomer(string json)
        {
            return JsonSerializer.Deserialize<Customers>(json);
        }
        #endregion

        #region Method Definition for XML Serialization - Customers
        public static string SerializeToXml(Customers customers)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customers));
            using (StringWriter sw = new StringWriter())
            {
                xmlSerializer.Serialize(sw, customers);
                return sw.ToString();
            }
        }
        public static Customers DeSerializeToXml(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customers));
            using (StringReader sr = new StringReader(xml))
            {
                return (Customers)xmlSerializer.Deserialize(sr);
            }
        }
        #endregion

        #region Method Definition for XML Serialization - Stores
        public static string SerializeToXmlStores(Stores stores)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Stores));
            using (StringWriter sw = new StringWriter())
            {
                xmlSerializer.Serialize(sw, stores);
                return sw.ToString();
            }
        }
        public static Stores DeSerializeToXmlStores(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Stores));
            using (StringReader sr = new StringReader(xml))
            {
                return (Stores)xmlSerializer.Deserialize(sr);
            }
        }
        #endregion

        public delegate string CreateDelegate(string name);
        public delegate int SumDelegate(int[] arr);
        static void Main(string[] args)
        {
            string constr = @"Server=DESKTOP-L411A1U;Database=BikeStores;Trusted_Connection=True;TrustServerCertificate=True";

            #region Anonymous methods using Delegates 
            //CreateDelegate obj = delegate (string name)
            //{
            //    return "Hello! " + name + " for Anonymous Delegate Method.";
            //};
            //CreateDelegate obj1 = (name) =>
            //{
            //    return "Hello! " + name + " for Lambda Delegate Expression.";
            //};
            //string msg = obj.Invoke("Kiruthika");
            //string msg1 = obj1.Invoke("Kiruthika");
            //Console.WriteLine(msg);
            //Console.WriteLine();
            //Console.WriteLine(msg1);
            #endregion

            #region Lambda Expressions - WhereCond
            //List<entity.Product> products = new List<entity.Product>()
            //{
            //    new entity.Product { ProductId = 1, ProductName = "Laptop", Price = 12000 },
            //    new entity.Product { ProductId = 2, ProductName = "SmartPhone", Price = 1000 },
            //    new entity.Product { ProductId = 3, ProductName = "Notepad", Price = 8000 },
            //    new entity.Product { ProductId = 4, ProductName = "Tablet", Price = 7000 }
            //};
            //var productList = products.Where(p => p.Price > 3000);
            //Console.WriteLine("Products under 3K");
            //foreach (var product in productList)
            //{
            //    Console.WriteLine(product.ProductName);
            //}
            #endregion

            #region Exercise 1 - Lambda WhereCond
            List<Student> students = new List<Student>()
            {
                new Student { Id = 1, Name = "Asha", Age = 21},
                new Student { Id = 2, Name = "Birlin", Age = 17},
                new Student { Id = 3, Name = "Cathrine", Age = 32},
                new Student { Id = 4, Name = "Zubair", Age = 25},
                new Student { Id = 5, Name = "Zulina", Age = 12}
            };
            var studentList = students.Where(s => s.Age >= 18);
            Console.WriteLine("Students above 18 years old");
            foreach (var student in studentList)
            {
                Console.WriteLine(student.Name);
            }
            #endregion

            #region Exercise 2 - Lambda OrderBy
            //List<entity.Employee> employee = new List<entity.Employee>()
            //{
            //    new entity.Employee { EmpId = 1, FirstName = "Asha", LastName = "Paul"},
            //    new entity.Employee { EmpId = 2, FirstName = "Birlin", LastName = "Franklin"},
            //    new entity.Employee { EmpId = 3, FirstName = "Cathrine", LastName = "Peter"},
            //    new entity.Employee { EmpId = 4, FirstName = "Zubair", LastName = "Xavier"},
            //    new entity.Employee { EmpId = 5, FirstName = "Zulina", LastName = "Sharin"}
            //};
            //var employeeList = employee.OrderBy(e => e.LastName);
            //Console.WriteLine("Employees of the Company by LastName");
            //foreach (var emp in employeeList)
            //{
            //    Console.WriteLine(emp.FirstName + " " + emp.LastName);
            //}
            #endregion

            #region Exercise 3 - Lambda SumArray
            int[] arr = { 1, 3, 2, 7, 8, 4 };
            SumDelegate sum = (arr) => arr.Sum(x => x);
            Console.WriteLine("Sum of the Array elements");
            var totalSum = sum.Invoke(arr);
            Console.WriteLine(totalSum);
            #endregion

            #region Exercise 4 - Lambda UpperCaseString
            //String[] str = { "Meena", "Joseph", "Kanishka", "Reena", "Rahul" };
            //CreateDelegate upperCase = (str) => str.ToUpper();
            //Console.WriteLine("Uppercased String Values");
            //foreach (String name in str)
            //{
            //    var upperCaseString = upperCase.Invoke(name);
            //    Console.WriteLine(upperCaseString);
            //}
            #endregion

            #region Exercise 5 - Query Syntax
            //List<entity.Student> students = new List<entity.Student>()
            //{
            //    new entity.Student { Id = 1, Name = "Asha", Age = 21},
            //    new entity.Student { Id = 2, Name = "Birlin", Age = 17},
            //    new entity.Student { Id = 3, Name = "Cathrine", Age = 32},
            //    new entity.Student { Id = 4, Name = "Zubair", Age = 25},
            //    new entity.Student { Id = 5, Name = "Zulina", Age = 12}
            //};
            //var studentList = from stud in students
            //                  where stud.Age >= 18
            //                  select stud;
            //foreach (var student in studentList)
            //{
            //    Console.WriteLine(student.Name);
            //}
            #endregion

            #region EXERCISE 6 - Query Syntax 2
            //List<entity.Product> products = new List<entity.Product>()
            //{
            //    new entity.Product { ProductId = 1, ProductName = "Laptop", Price = 12000, Category = "Electronics" },
            //    new entity.Product { ProductId = 2, ProductName = "SmartPhone", Price = 10000, Category = "Electronics" },
            //    new entity.Product { ProductId = 3, ProductName = "Notepad", Price = 8000, Category = "Electronics" },
            //    new entity.Product { ProductId = 4, ProductName = "Jeans", Price = 700, Category = "Clothing" },
            //    new entity.Product { ProductId = 5, ProductName = "T-Shirt", Price = 300, Category = "Clothing" },
            //    new entity.Product { ProductId = 6, ProductName = "Mixer", Price = 6500, Category = "Appliances" },
            //    new entity.Product { ProductId = 7, ProductName = "LG TV", Price = 50000, Category = "Appliances" }
            //};
            //var prodByCatGrp = from product in products
            //                   group product by product.Category into productGroup
            //                   select productGroup;
            //foreach (var catGrp in prodByCatGrp)
            //{
            //    Console.WriteLine($"Category : {catGrp.Key}");
            //    foreach (var prod in catGrp)
            //    {
            //        Console.WriteLine($"Product Name : {prod.ProductName}");
            //    }
            //};
            #endregion

            #region EXERCISE 7 - DB Operation Logic
            //using (DataConnectContext.ProductDataContext db = new DataConnectContext.ProductDataContext(constr))
            //{
            //    var ProductList = from product in db.Products
            //                      where product.Price > 500
            //                      select product;
            //    Console.WriteLine("Products higher than 500Rs");
            //    foreach (var item in ProductList)
            //    {
            //        Console.WriteLine($"Product Name : {item.ProductName}");
            //    }
            //}
            #endregion

            #region EXERCISE 8 - DB Connection Example 2
            //using (DataConnectContext.EmployeeDataContext db = new DataConnectContext.EmployeeDataContext(constr))
            //{
            //    var emp11 = new entity.Employee { EmpId = 11, Name = "Joseph Richard", Salary = 92000 };
            //    var emp12 = new entity.Employee { EmpId = 12, Name = "Rithika Senthil", Salary = 35000 };
            //    var emp13 = new entity.Employee { EmpId = 13, Name = "Sanjana Roy", Salary = 49000 };
            //    var emp14 = new entity.Employee { EmpId = 14, Name = "Lokesh Kumar", Salary = 53500 };
            //    var emp15 = new entity.Employee { EmpId = 15, Name = "Vijay Kumar", Salary = 99000 };

            //    db.Employees.InsertOnSubmit(emp11);
            //    db.Employees.InsertOnSubmit(emp12);
            //    db.Employees.InsertOnSubmit(emp13);
            //    db.Employees.InsertOnSubmit(emp14);
            //    db.Employees.InsertOnSubmit(emp15);
            //    db.SubmitChanges();

            //    Console.WriteLine("New employees have been added.");
            //}

            //using (DataConnectContext.EmployeeDataContext db = new DataConnectContext.EmployeeDataContext(constr))
            //{
            //    var EmployeeList = from employee in db.Employees
            //                       where employee.Salary > 50000
            //                       select employee;
            //    Console.WriteLine("Employee with Salary above $50000");
            //    foreach (var item in EmployeeList)
            //    {
            //        Console.WriteLine($"Employee Name : {item.Name}");
            //    }
            //}
            #endregion

            #region EXERCISE 9 - DB Connection Example 3
            //using (DataConnectContext.SalesDataContext db = new DataConnectContext.SalesDataContext(constr))
            //{
            //    var SalesList = from order in db.Orders
            //                    join stores in db.Stores
            //                    on order.store_id equals stores.store_id
            //                    select new
            //                    {
            //                        Order = order,
            //                        StoreName = stores.store_name
            //                    };
            //    Console.WriteLine("All Orders with Their Store Name");
            //    foreach (var item in SalesList)
            //    {
            //        Console.WriteLine($"Order ID : {item.Order.order_id},\nStore Name : {item.StoreName}");
            //    }
            //}
            #endregion

            #region EXERCISE 10 - DB Connection Example 4
            //using (DataConnectContext.SalesDataContext db = new DataConnectContext.SalesDataContext(constr))
            //{
            //    var CustomerList = from customer in db.Customers
            //                       where customer.state == "CA"
            //                       select customer;
            //    Console.WriteLine("Customers Located at California State");
            //    foreach (var item in CustomerList)
            //    {
            //        Console.WriteLine($"Customer ID : {item.customer_id},\nCustomer Name : {item.first_name + ' ' + item.last_name},\nPhone Number : {item.phone},\nEmail : {item.email},\nAddress : {item.street + ',' + item.city + ',' + item.state + ',' + item.zip_code}");
            //    }
            //}
            #endregion

            #region EXERCISE 11 - Custom Exceptions
            //method.OrderProcessor orderProcessor = new method.OrderProcessor();
            //try
            //{
            //    double orderAmount = -50;
            //    orderProcessor.PlaceOrder(orderAmount);
            //}
            //catch (exception.InvalidOrderException ex)
            //{
            //    Console.WriteLine($"Error : {ex.Message}");
            //}
            #endregion

            #region EXERCISE 12 - Custom Exceptions Example 2
            method.AgeProcessor ageProcessor = new method.AgeProcessor();
            try
            {
                int age = 23;
                ageProcessor.ValidateAge(age);
            }
            catch (exception.InvalidAgeException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            #endregion

            #region EXERCISE 13 - SERIALIZATION & DE-SERIALIZATION Using Text BinaryFormatter .NET6
            //Student student = new Student();
            //student.Id = 1;
            //student.Name = "Test";

            //var formatter = new BinaryFormatter();
            //Stream stream = new FileStream(@"D:\HEXA-SEGUE 201 - .NET FSD\C# .NET\SESSION DEMO\sample.json", FileMode.Create, FileAccess.Write);
            //formatter.Serialize(stream, student);
            //stream.Close();

            //stream = new FileStream(@"D:\HEXA-SEGUE 201 - .NET FSD\C# .NET\SESSION DEMO\sample.json", FileMode.Open, FileAccess.Read);
            //Student objnew = (Student)formatter.Deserialize(stream);
            //Console.WriteLine(objnew.Name);
            //stream.Close();
            #endregion

            #region Sample GPT code using JSON for Exercise 13
            //Student student1 = new Student
            //{
            //    Id = 1,
            //    Name = "test"
            //};

            //// Path for serialization
            //string filePath = @"D:\HEXA-SEGUE 201 - .NET FSD\C# .NET\SESSION DEMO\student.json";

            //// Serialize the Student object to a file
            //try
            //{
            //    string jsonString = JsonSerializer.Serialize(student1);

            //    File.WriteAllText(filePath, jsonString);

            //    Console.WriteLine("Student object has been serialized to JSON successfully.");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Serialization error: {ex.Message}");
            //}

            // Deserialize the Student object from a file
            //try
            //{
            //    string jsonString = File.ReadAllText(filePath);

            //    Student objnew1 = JsonSerializer.Deserialize<Student>(jsonString);
            //    Console.WriteLine($"Deserialized student name: {objnew1.Name}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Deserialization error: {ex.Message}");
            //}
            #endregion

            #region EXERCISE 14 - SERIALIZATION & DE-SERIALIZATION Using Text JsonSerializer .NET6 & .NET8
            //Product product = new Product { ProductId = 1, ProductName = "Sketch Board", Price = 540, Category = "Stationary" };
            //string json = SerializeToJson(product);
            //Console.WriteLine("Serialized Json : \n" + json);
            //Console.WriteLine();
            //Product deserializedProduct = DeSerializeToJson(json);
            //Console.Write("Deserialized Json : ");
            //Console.WriteLine(deserializedProduct.ProductId);
            #endregion

            #region EXERCISE 15 - SERIALIZATION & DE-SERIALIZATION Using XmlSerializer .NET6 & .NET8
            //Customers customer = new Customers() { customer_id = 1, first_name = "Will", last_name = "Smith", email = "willsmith@gmail.com", phone = "9093903245", street = "XYZ Street", city = "Chennai", state = "Tamil Nadu", zip_code = "603903" };
            //string xml = SerializeToXml(customer);
            //Console.WriteLine("Serialized XML : " + xml);
            //Console.WriteLine();

            //Customers deCustomer = DeSerializeToXml(xml);
            //Console.Write("Deserialized XML : ");
            //Console.WriteLine(deCustomer.first_name);
            #endregion

            #region EXERCISE 16 - Json Serialization Example 1
            //var order = new List<Orders>
            //{
            //    new Orders {OrderID = 1, OrderAmount = 99.56},
            //    new Orders {OrderID = 2, OrderAmount = 320.45},
            //    new Orders {OrderID = 3, OrderAmount = 123.89}
            //}; 
            //Customers cust = new Customers() { CustomerID = 1, CustomerName = "Swetha", orders = order };
            //string json = SerializeToJsonCustomer(cust);
            //Console.WriteLine("Serialized Json for Customer : " + json);
            //Console.WriteLine();

            //Customers decust = DeSerializeToJsonCustomer(json);
            //Console.WriteLine("Deserialized Json for Customer : ");
            //Console.Write($" Customer ID : {decust.CustomerID} \n Customer Name : {decust.CustomerName} \n Order Details : \n");
            //foreach(var ord in decust.orders)
            //{
            //    Console.WriteLine($" Order ID : {ord.OrderID}, Order Amount : {ord.OrderAmount}");
            //}
            #endregion

            #region EXERCISE 17 - XML Serialization Example 1
            //var product = new List<Product>
            //{
            //    new Product {ProductId = 1, ProductName = "Boat Speakers"},
            //    new Product {ProductId = 2, ProductName = "Pedigree Kit"},
            //    new Product {ProductId = 3, ProductName = "Brain Dev Games"}
            //};
            //Stores store = new Stores() { StoreId = 1, StoreName = "Bela Supplies", Products = product };
            //string xml = SerializeToXmlStores(store);
            //Console.WriteLine("Serialized XML for Stores : " + xml);
            //Console.WriteLine();

            //Stores destore = DeSerializeToXmlStores(xml);
            //Console.WriteLine("Deserialized XML for Stores : ");
            //Console.Write($" Store ID : {destore.StoreId} \n Store Name : {destore.StoreName} \n Product Details : \n");
            //foreach (var prod in destore.Products)
            //{
            //    Console.WriteLine($" Product ID : {prod.ProductId}, Product Name : {prod.ProductName}");
            //}
            #endregion

        }
    }
}