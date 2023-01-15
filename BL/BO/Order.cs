
namespace BO;

public class Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }      //לשים סימן שאלה?
    public BO.Enums.eOrderStatus Status { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        string order =
        $@"
        Order ID: {ID}, 
        Customer Name: {CustomerName},
        Customer Email: {CustomerEmail},
        Customet Adress: {CustomerAddress},
        Order Date: {OrderDate},
        Status: {Status},
        Ship Date: {ShipDate},
        Delivery Date: {DeliveryDate},
        Total Price {TotalPrice},
        Items: ";
        foreach (OrderItem item in Items)
        {
            order += $@"{item}" + "\n";
        }
        return order;
    }
}

