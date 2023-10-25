using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Converters;

public class Order
{
    public int OrderId { get; private set; } // Cela doit être un non-nullable int maintenant que nous le gérons différemment.
    public List<Product> Products { get; set; }
    public Customer Customer { get; set; }
    public Employee Clerk { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public OrderStatus Status { get; set; }

    private static int cpt;
    private static readonly string idStoragePath = "OrderId.txt"; // Chemin du fichier

    static Order()
    {
        // Charger la valeur du compteur à partir du fichier lors de la première utilisation de cette classe
        if (File.Exists(idStoragePath))
        {
            string lastId = File.ReadAllText(idStoragePath);
            cpt = int.Parse(lastId);
        }
        else
        {
            cpt = 0; // Ou tout autre valeur initiale désirée
        }
    }

    public Order(List<Product> products, Customer customer, Employee clerk)
    {
        NewOrderAssigned(); // Appel à la méthode qui s'occupe de la logique d'attribution d'ID

        this.Products = products;
        this.Customer = customer; // Assurez-vous d'assigner le bon objet. Il y avait une erreur ici.
        this.Clerk = clerk;
        this.Status = OrderStatus.InPreparation;
    }

    public Order()
    {
        NewOrderAssigned(); // Appel à la méthode qui s'occupe de la logique d'attribution d'ID
        this.Status = OrderStatus.InPreparation;
    }

    private void NewOrderAssigned()
    {
        cpt++; // Incrémente pour chaque nouvelle commande
        this.OrderId = cpt;

        // Sauvegarde la valeur actuelle de cpt pour la prochaine exécution du programme
        File.WriteAllText(idStoragePath, cpt.ToString());
    }

    public enum OrderStatus
    {
        InPreparation,
        InDelivery,
        Closed
    }
}
