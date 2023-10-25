using System;
using System.Threading.Tasks;

class Program
{
    static PizzeriaManager pizzeriaManager;

    static async Task Main(string[] args)
    {
        pizzeriaManager = new PizzeriaManager();

        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("\n\nBienvenue à la Pizzeria!");
            Console.WriteLine("Sélectionnez une option:");
            Console.WriteLine("1. Nouvelle commande");
            Console.WriteLine("2. Vérifier une commande");
            Console.WriteLine("3. Quitter");
            Console.Write("Entrez votre choix: ");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    await pizzeriaManager.HandleCustomerCall();
                    break;
                case "2":
                    Console.Write("Entrez l'ID de la commande: ");
                    string input = Console.ReadLine();

                    // int orderId;
                    // if (int.TryParse(input, out orderId))
                    // {
                    //     var order = pizzeriaManager.GetOrderById(orderId);
                    //     if (order != null)
                    //     {
                    //         // Affichez les détails de la commande
                    //         Console.WriteLine($"Commande ID: {order.OrderID}");
                    //         // ... afficher d'autres détails ...
                    //     }
                    //     else
                    //     {
                    //         Console.WriteLine("Aucune commande trouvée avec cet ID.");
                    //     }
                    // }
                    // else
                    // {
                    //     Console.WriteLine("ID invalide, veuillez entrer un nombre.");
                    // }
                    break;
                case "3":
                    Console.WriteLine("Au revoir!");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Option non reconnue, veuillez réessayer.");
                    break;
            }

            // Petite pause pour la lisibilité avant de continuer la boucle
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }
}
