public class Pizza : Product
{
    public Size PizzaSize { get; set; }

    public enum Size{
        Small,
        Medium,
        Large
    }

    public Pizza(string name, Size size, decimal price)
    : base(name, price){
        this.PizzaSize = size;
    }
}