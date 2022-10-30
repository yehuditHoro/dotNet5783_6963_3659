using Dalfaced.DO;
namespace DO;

internal static class DataSource
{
    static internal int numOfProducts = 50;
    static internal int numOfOrders = 100;
    static internal int numOfOrderItems = 200;
    static internal Product[] ProductsList = new Product[numOfProducts];
    static internal Order[] ProductsList = new Order[numOfOrders];
    static internal OrderItem[] ProductsList = new OrderItem[numOfOrderItems];
    static internal readonly Random random = new Random();

}






using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalFacade.DO;

namespace DalList;
static internal class DataSource
{
    static internal int NumOfProducts = 50;
    static internal int NumOfOrders = 100;
    static internal int NumOfOrderItems = 200;
    static internal Product[] ProductList = new Product[NumOfProducts];
    static internal Order[] Orders = new Order[NumOfOrders];
    static internal OrderItem[] OrderItems = new OrderItem[NumOfOrderItems];
    static internal readonly Random rand = new Random();
    static internal string[] productNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    //orders
    static string[] customerName = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    static string[] customerEmail = { "A@", "B@", "C@", "D@", "E@", "F@", "G@", "H@", "I@", "J@", "K@", "L@", "M@", "N@", "O@", "P@", "Q@", "R@", "S@", "T@", "U@", "V@", "W@", "X@", "Y@", "Z@" };
    static string[] customerAdress = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    static DataSource() { s_Initialize(); }
    static void s_Initialize()
    {
        CreateProductsList();
        CreateOrdersList();
        CreateOrderItemList();
    }
    static public void CreateProductsList()
    {
        for (int i = 0; i < 50; i++)
        {
            Product newProduct = new Product();
            newProduct.ID = Config.ProductID;
            newProduct.Name = productNames[i % 26];
            newProduct.Price = (int)rand.NextInt64(1, 100000);
            newProduct.Category = (Category)(i % 5);
            newProduct.InStock = (int)rand.NextInt64(1, 50);
            ProductList[Config.indexProduct++] = newProduct;
        }
    }
    static public void CreateOrdersList()
    {
        TimeSpan t_ShipDate = TimeSpan.FromDays(10);
        TimeSpan t_DeliveryDate = TimeSpan.FromDays(30);
        for (int i = 0; i < 100; i++)
        {
            Order newOrders = new Order();
            newOrders.ID = Config.OrderID;
            newOrders.CustomerName = customerName[i % 26];
            newOrders.CustomerEmail = customerEmail[i % 26];
            newOrders.CustomerAdress = customerAdress[i % 26];
            newOrders.OrderDate = DateTime.MinValue;
            newOrders.ShipDate = (Orders[i].OrderDate + t_ShipDate);
            newOrders.DeliveryDate = (Orders[i].ShipDate + t_DeliveryDate);
            Orders[Config.indexOrder++] = newOrders;
        }
    }


    static public void CreateOrderItemList()
    {
        for (int i = 0; i < 40; i++)
        {
            int indexOrders = (int)rand.NextInt64(Orders.Length);
            int numOfProduct = (int)rand.NextInt64(1, 4);

            for (int j = 0; j < numOfProduct; j++)
            {
                int indexProduct = (int)rand.NextInt64(ProductList.Length);
                OrderItem newOrderItems = new OrderItem();
                newOrderItems.ID = Config.OrderItemID;
                newOrderItems.ProductID = ProductList[indexProduct].ID;
                newOrderItems.OrderID = Orders[indexOrders].ID;
                newOrderItems.Amount = (int)rand.NextInt64(1, ProductList[indexProduct].InStock);
                newOrderItems.Price = (ProductList[indexProduct].Price) * newOrderItems.Amount;
                OrderItems[Config.indexOrderItem++] = newOrderItems;
            }
        }
    }

    internal static class Config
    {
        internal static int indexOrder = 0;
        internal static int indexProduct = 0;
        internal static int indexOrderItem = 0;

        private static int orderItemID = 1000000;
        public static int OrderItemID { get { return orderItemID++; } }

        private static int orderID = 3000000;
        public static int OrderID { get { return orderID++; } }

        private static int productID = 6000000;
        public static int ProductID { get { return productID++; } }

    }
}

