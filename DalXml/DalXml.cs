using DalApi;
using System.Xml.Linq;
using System.Linq;

namespace Dal;

sealed internal class DalXml : IDal
{
    private DalXml()
    {
        AddListProducts();
        CreateOrdersList();
        CreateOrderItemList();
    }
    public static IDal Instance { get; } = new DalXml();
    public Iproduct product { get; } = new Dal.DalProduct();
    public Iorder order { get; } = new Dal.DalOrder();
    public IorderItem orderItem { get; } = new Dal.DalOrderItem();

    private List<Dal.DO.Order> OrdersList = new();
    private List<Dal.DO.Product> ProductsList = new();
    private List<Dal.DO.OrderItem> OrderItemsList = new();

    readonly Random rand = new Random();

    public void AddListProducts()
    {
        XElement? root = XDocument.Load("..\\xml\\Config.xml").Root;
        string[] productsNames = { "dresses", "shirts", "pants", "shoes", "skirts", "socks", "sweaters", "tights", "vests", "coats" };
        for (int i = 0; i < 10; i++)
        {
            Dal.DO.Product p = new();
            p.ID = Convert.ToInt32(root?.Descendants("config").Elements("productId")?.FirstOrDefault()?.Value);
            p.Name = productsNames[i % 10];
            p.Price = (int)rand.Next(50, 450);
            p.Category = (Dal.DO.eCategory)(i % Enum.GetValues(typeof(Dal.DO.eCategory)).Length);
            if (i % 20 == 0)
                p.InStock = 0;
            else
                p.InStock = (int)rand.NextInt64(0, 1000);
            ProductsList.Add(p);
            product.Add(p);
        }
    }

    public void CreateOrdersList()
    {
        XElement? root = XDocument.Load("..\\xml\\Config.xml").Root;
        string[] customersNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string[] customersAddresses = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string[] customersEmails = { "A@a", "B@b", "C@c", "D@d", "E@e", "F@f", "G@g", "H@h", "I@i", "J@j", "K@k", "L@l", "M@m", "N@n", "O@o", "P@p", "Q@q", "R@r", "S@s", "T@t", "U@u", "V@v", "W@w", "X@x", "Y@y", "Z@z" };
        TimeSpan shipDate = TimeSpan.FromDays(10);
        TimeSpan deliveryDate = TimeSpan.FromDays(6);
        for (int i = 0; i < 20; i++)
        {
            Dal.DO.Order newOrder = new();
            newOrder.ID = Convert.ToInt32(root?.Descendants("config").Elements("orderId")?.FirstOrDefault()?.Value);
            newOrder.CustomerName = customersNames[i % 26];
            newOrder.CustomerEmail = customersEmails[i % 26];
            newOrder.CustomerAddress = customersAddresses[i % 26];
            if (i % 10 < 8)  // 80% have ship date
                newOrder.OrderDate = DateTime.Now;
            else
                newOrder.OrderDate = DateTime.MinValue;
            newOrder.ShipDate = newOrder.OrderDate + shipDate;
            if (i % 10 < 6) // 60% from them have delivery date
                newOrder.DeliveryDate = newOrder.ShipDate + deliveryDate;
            else
                newOrder.DeliveryDate = DateTime.MinValue;
            OrdersList.Add(newOrder);
        }
    }

    public void CreateOrderItemList()
    {
        XElement? root = XDocument.Load("..\\xml\\Config.xml").Root;
        for (int i = 0; i < 40; i++)
        {
            int num = (int)rand.Next(1, 4);
            int IndexOrder = (int)rand.Next(0, OrdersList.Count());
            for (int j = 0; j < num; j++)
            {
                int IndexProduct = (int)rand.Next(0, ProductsList.Count());
                Dal.DO.OrderItem newOrderItems = new();
                newOrderItems.ID = Convert.ToInt32(root?.Descendants("config").Elements("orderItemId")?.FirstOrDefault()?.Value);
                newOrderItems.ProductId = ProductsList[IndexProduct].ID;
                newOrderItems.OrderId = OrdersList[IndexOrder].ID;
                newOrderItems.Amount = (int)rand.Next(1, 500);
                newOrderItems.Price = (ProductsList[IndexProduct].Price) * newOrderItems.Amount;
                OrderItemsList.Add(newOrderItems);
            }
        }
    }
}
