
namespace DO;

internal class Order
{
    public int ID { get; set }
    public string CustomerName { get; set }
    public string CustomerEmail { get; set }
    public string CustometAdress { get; set}
    public DateTime OrderDate { get; set}
    public DateTime ShipDate { get; set}
    public DateTime DeliveryDate { get; set}

    public override string ToString() => $@"
    Product ID: {ID}, 
    Customer Name: {CustomerName},
    Customer Email: {CustomerEmail},
    Customet Adress: {CustometAdress},
    Order Date: {OrderDate},
    Ship Date: {ShipDate},
    Delivery Date: {DeliveryDate}";
}
