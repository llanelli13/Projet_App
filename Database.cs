using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class Database
{
    private const string OrdersPath = "BDD/orders.json";
    private const string CustomersPath = "BDD/customers.json";
    private const string EmployeesPath = "BDD/employees.json";

    public List<Order> Orders { get; set; }
    public List<Customer> Customers { get; set; }
    public List<Employee> Employees { get; set; }

    public Database()
    {
        Orders = LoadData<Order>(OrdersPath) ?? new List<Order>();
        Customers = LoadData<Customer>(CustomersPath) ?? new List<Customer>();
        Employees = LoadData<Employee>(EmployeesPath) ?? new List<Employee>();
    }

    public void SaveChanges()
    {
        SaveData(OrdersPath, Orders);
        SaveData(CustomersPath, Customers);
        SaveData(EmployeesPath, Employees);
    }

private static void SaveData<T>(string filePath, List<T> data)
{
    var settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented
    };

    var jsonData = JsonConvert.SerializeObject(data, settings);
    File.WriteAllText(filePath, jsonData);
}


private static List<T> LoadData<T>(string filePath)
{
    if (!File.Exists(filePath)) return null;
    var jsonData = File.ReadAllText(filePath);
    var settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto
    };
    return JsonConvert.DeserializeObject<List<T>>(jsonData, settings); 
}

}