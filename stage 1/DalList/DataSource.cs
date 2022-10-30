using Dalfaced.DO;
namespace DO;
namespace Dal;
internal static class DataSource
{
    static internal int numOfProducts = 50;
    static internal int numOfOrders = 100;
    static internal int numOfOrderItems = 200;
    static internal Product[] ProductsList = new Product[numOfProducts];
    static internal Order[] ProductsList = new Order[numOfOrders];
    static internal OrderItem[] ProductsList = new OrderItem[numOfOrderItems];
    static internal readonly Random random = new Random();
    static string[] customersNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    static string[] customersEmails = { "A@a", "B@b", "C@c", "D@d", "E@e", "F@f", "G@g", "H@h", "I@i", "J@j", "K@k", "L@l", "M@m", "N@n", "O@o", "P@p", "Q@q", "R@r", "S@s", "T@t", "U@u", "V@v", "W@w", "X@x", "Y@y", "Z@z" };
    static string[] customersAdresses = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    static string[] ProductName = {"dresses", "shirts", "pants", "shoes",
        "skirts", "socks"}

    static DataSource() { s_Initialize(); }
    static void s_Initialize()
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
            newProduct.Price = (int)rand NextInt64(50, 450)///(int)
            newProduct.Category = (Category)(i % 5);
            newProduct.InStock = (int)rand NextInt64(1, 50)
            ProductsList[config.indexProduct++] = newProduct;
        }
    }
    static public void CreateOrderItemList()
    {
        for (int i = 0; i < 40; i++)
        {
            int num = (int)rand NextInt64(1,4);
            int OrderId=(int)rand NextInt64(config.indexOrderItem)
            for (int j = 0; j < num; j++)
            {
                int indexProduct = (int)rand.NextInt64(ProductsList.Length);
                OrderItem newOrderItems = new OrderItem();
                newOrderItems.ID = Config.OrderItemID;
                newOrderItems.ProductID = ProductList[indexProduct].ID;
                newOrderItems.OrderID = Orders[indexOrder].ID;
                newOrderItems.Amount = (int)rand.NextInt64(1, ProductList[indexProduct].InStock);
                newOrderItems.Price = (ProductList[indexProduct].Price) * newOrderItems.Amount;
                OrderItems[config.indexOrderItem++] = newOrderItems;


            }
        }
    }

    internal static class config()
    {
    internal static int indexProduct = 0;
    internal static int indexOrder = 0;
    internal static int indexOrderItem = 0;

    private static int productId = 0;
    public static int ProductId { get { return productId++} }

    private static int orderId = 100;
    public static int OrderId { get { return orderId++} }

    private static int orderItem = 1000;
    public static int OrderItem { get { return orderItem++} }

}
}



