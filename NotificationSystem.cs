using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class NotificationSystem
{
    public void NotifyCustomer(Order order, string message)
    {
        Console.WriteLine($"Notification to {order.Customer.FirstName}: {message}");
    }

    public void NotifyClerk(Order order, string message)
    {
        Console.WriteLine($"Notification to {order.Clerk.FirstName}: {message}");
    }

}
