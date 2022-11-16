using Dal.DO;
namespace dalList;

public class DataSource
{
    static public List<Product> ProductsList = new List<Product>();
    static public List<Order> OrdersList = new List<Order>();
    static public List<OrderItem> OrderItemsList = new List<OrderItem>();
    static internal readonly Random rand = new Random();
    static string[] customersNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    static string[] customersEmails = { "A@a", "B@b", "C@c", "D@d", "E@e", "F@f", "G@g", "H@h", "I@i", "J@j", "K@k", "L@l", "M@m", "N@n", "O@o", "P@p", "Q@q", "R@r", "S@s", "T@t", "U@u", "V@v", "W@w", "X@x", "Y@y", "Z@z" };
    static string[] customersAddresses = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    static string[] ProductName = { "dresses", "shirts", "pants", "shoes", "skirts", "socks", "sweaters", "tights", "vests", "coats" };

    static DataSource() { s_Initialize(); }
    static private void s_Initialize()
    {
        CreateProductsList();
        CreateOrdersList();
        CreateOrderItemList();
    }
    static public void CreateProductsList()
    {
        for (int i = 0; i < 10; i++)
        {
            Product newProduct = new Product();
            newProduct.ID = config.ProductId;
            newProduct.Name = ProductName[i % 10];
            newProduct.Price = (int)rand.Next(50, 450);
            newProduct.Category = (eCategory)(i % 5);
            newProduct.InStock = (int)rand.Next(1, 50);
            ProductsList.Add(newProduct);
        }
    }

    public static void CreateOrdersList()
    {
        TimeSpan shipDate = TimeSpan.FromDays(10);
        TimeSpan deliveryDate = TimeSpan.FromDays(6);
        for (int i = 0; i < 20; i++)
        {
            Order newOrder = new Order();
            newOrder.ID = config.OrderId;
            newOrder.CustomerName = customersNames[i % 26];
            newOrder.CustomerEmail = customersEmails[i % 26];
            newOrder.CustomerAddress = customersAddresses[i % 26];

            if (i % 10 < 8)  // 80% have ship date
                newOrder.OrderDate = DateTime.Now;
            else
                newOrder.OrderDate = DateTime.MinValue;
            newOrder.ShipDate = newOrder.OrderDate + shipDate;
            if (i % 10 < 6)// 60% from them have delivery date
                newOrder.DeliveryDate = newOrder.ShipDate + deliveryDate;
            else
                newOrder.DeliveryDate = DateTime.MinValue;

            OrdersList.Add(newOrder);
        }
    }
    static public void CreateOrderItemList()
    {
        for (int i = 0; i < 40; i++)
        {
            int num = (int)rand.Next(1, 4);
            int IndexOrder = (int)rand.Next(0, OrdersList.Count());
            for (int j = 0; j < num; j++)
            {
               int IndexProduct = (int)rand.Next(0, OrdersList.Count());
                OrderItem newOrderItems = new OrderItem();
                newOrderItems.ID = config.OrderItemId;
                newOrderItems.ProductId = ProductsList[IndexProduct].ID;
                newOrderItems.OrderId = OrdersList[IndexOrder].ID;
                newOrderItems.Amount = (int)rand.Next(1, 500);
                newOrderItems.Price = (ProductsList[IndexProduct].Price) * newOrderItems.Amount;
                OrderItemsList.Add(newOrderItems);
            }
        }
    }

    public static class config
    {
        private static int _productId = 0;
        public static int ProductId { get { return  _productId++; } }

        private static int _orderId = 100;
        public static int OrderId { get { return _orderId++; } }

        private static int _orderItemId = 1000;
        public static int OrderItemId { get { return _orderItemId++; } }

    }
}



