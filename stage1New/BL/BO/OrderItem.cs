
namespace BO;

public class OrderItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    order item id: {ID},
    name: {Name},
    product id: {ProductID}, 
    price: {Price},
    amount: {Amount}
    total price: {TotalPrice}";
}

