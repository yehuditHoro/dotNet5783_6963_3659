using Dalfaced.DO;
namespace DO;
namespace Dal;

internal static class DataSource
{
    static internal int numOfProducts = 50;
    static internal int numOfOrders = 100;
    static internal int numOfOrderItems = 200;
    static internal Products[] ProductsList = new Product[numOfProducts];
    static internal Orders[] ProductsList = new Order[numOfOrders];
    static internal OrderItems[] ProductsList = new OrderItem[numOfOrderItems];
    static internal readonly Random random = new Random();

    static string[] ProductName = {"dresses", "shirts", "pants", "shoes", 
        "skirts", "socks", "sweaters", "tights", "vests", "coats"}


    public void CreateOrdersList
    { 
        TimeSpan shipDate = TimeSpan.FromDays(6);
        TimeSpan deliveryDate = TimeSpan.FromDays(10);
        for (int i = 0; i < 20; i++){
            Order newOrder = new Order();
            newOrder.ID = config.orderId;
            newOrder.CustomerName = customersNames[i % 26];
            newOrder.CustomerEmail = customersEmails[i % 26];
            newOrder.CustomerAdress = customersAdresses[i % 26];
            newOrder.OrderDate = DateTime.MinValue;
            newOrder.ShipDate = newOrder.OrderDate + shipDate;
            newOrder.DeliveryDate = newOrder.ShipDate + deliveryDate;
            Orders[config.indexOrder++] = newOrder;
        }
    }

}

