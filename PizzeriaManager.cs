using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;


public class PizzeriaManager{
    private Database _database;
    public PizzeriaManager()
    {
        _database = new Database();
    }

    public void CreateOrder(Order order)
    {
        _database.Orders.Add(order);
        _database.SaveChanges();
    }

    public void AddCustomer(Customer customer)
    {
        _database.Customers.Add(customer);
        _database.SaveChanges();
    }

    public Customer CreateCustomer(){
            Console.Write("Enter customer's first name: ");
            string? firstName = Console.ReadLine();
            Console.Write("Enter customer's last name: ");
            string? lastName = Console.ReadLine();
            Console.Write("Enter address: ");
            string? address = Console.ReadLine();
            Console.Write("Enter phone number: ");
            string? phoneNumber = Console.ReadLine();

            var newCustomer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber
            };
            _database.Customers.Add(newCustomer);
            _database.SaveChanges();
            return newCustomer;
        
    }

    public void UpdateOrderStatus(int orderId, Order.OrderStatus newStatus)
    {
        var order = _database.Orders.Find(o => o.OrderId == orderId);
        if (order != null)
        {
            order.Status = newStatus;
            _database.SaveChanges();
        }
    }

    public async Task HandleCustomerCall(){

        Console.WriteLine("Is it your first order here ? Y/N");
        string? rep = Console.ReadLine();
        if (rep?.ToLower() == "y"){
            Customer newCustomer = CreateCustomer();
            Console.WriteLine($"Thank you {newCustomer.FirstName}! Let's proceed with your order !");
            CreateOrder(newCustomer);

        } else if (rep?.ToLower()=="n"){
            Console.WriteLine("Enter your phone number :");
            string? phonenumber = Console.ReadLine();
            var existingCustomer = _database.Customers.FirstOrDefault(c => c.PhoneNumber == phonenumber);
            
            if (existingCustomer != null){
            Console.WriteLine($"Welcome back, {existingCustomer.FirstName}! We are preparing your new order.");
            CreateOrder(existingCustomer);
            } else{
            Console.WriteLine("Sorry, we couldn't find your record. Let's create a new account for you.");
            Customer customer = CreateCustomer();
            CreateOrder(customer);
            }

        } else{
        Console.WriteLine("Invalid response. Please start again.");
        HandleCustomerCall();
        }
    }

    public async Task<Order> CreateOrder(Customer customer){
        Order order = new Order { Products = new List<Product>(), Customer = customer }; 

        bool ordering = true;

        while (ordering)
        {
            Console.WriteLine("Please choose an option: \n1. Add Pizza\n2. Add Drink\n3. Complete Order");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Choose a type of pizza : ");
                    string? pizzaType = Console.ReadLine();
                    Console.WriteLine("Choose a pizza size: (1. Small, 2. Medium, 3. Large)");
                    string? sizeInput = Console.ReadLine();
                    Pizza.Size pizzaSize; 

                    switch (sizeInput){
                        case "1":
                            pizzaSize = Pizza.Size.Small;
                            break;
                        case "2":
                            pizzaSize = Pizza.Size.Medium;
                            break;
                        case "3":
                            pizzaSize = Pizza.Size.Large;
                            break;
                        default:
                            Console.WriteLine("Invalid size. Defaulting to small.");
                            pizzaSize = Pizza.Size.Small;
                            break;
                    }
                    Pizza pizza = new Pizza(pizzaType, pizzaSize, DeterminePizzaPrice(pizzaSize));
                    order.Products.Add(pizza);
                    break;

                case "2":
                    Console.WriteLine("Select your drink :");
                    string? drinkName = Console.ReadLine();
                    Drink drink = new Drink(drinkName, DetermineDrinkPrice(drinkName));
                    order.Products.Add(drink);
                    break;

                case "3":
                    ordering = false; 
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
        _database.Orders.Add(order);
        _database.SaveChanges();

        await SendOrderNotificationsAsync(order);

        return order;
    }

private Task SendOrderNotificationsAsync(Order order)
{
    string customerMessage = $"Cher {order.Customer.FirstName}, votre commande a été reçue.";
    string kitchenMessage = $"Nouvelle commande de {order.Customer.FirstName} pour {order.Products.Count} produit(s).";
    string deliveryMessage = $"Préparez-vous pour une nouvelle livraison à l'adresse {order.Customer.Address} dans 5 minutes.";

    // Simuler l'envoi de messages
    Console.WriteLine($"Notification envoyée au client : {customerMessage}");
    Console.WriteLine($"Notification envoyée à la cuisine : {kitchenMessage}");

    // Ici, nous simulons simplement un délai. En réalité, vous pourriez vouloir faire quelque chose de plus utile.
    Task delayTask = Task.Delay(TimeSpan.FromMinutes(1)).ContinueWith(t => 
    {
        Console.WriteLine($"Notification envoyée au livreur : {deliveryMessage}");
    });

    return delayTask; // cela retourne immédiatement une tâche en cours, qui se complète après le délai
}




    private decimal DeterminePizzaPrice(Pizza.Size size)
    {
        switch (size)
        {
            case Pizza.Size.Small:
                return 9.99m;
            case Pizza.Size.Medium:
                return 11.99m;
            case Pizza.Size.Large:
                return 13.99m;
            default:
                throw new ArgumentOutOfRangeException(nameof(size), "The size is not valid or not supported.");
        }
    }

    private decimal DetermineDrinkPrice(string name){
        return 2.0m;
    }

    public Order GetOrderById(int id){
    return _database.Orders.FirstOrDefault(o => o.OrderId == id);
    }


    // Ajoutez ici d'autres méthodes pour gérer les employés, les statistiques, etc.
}