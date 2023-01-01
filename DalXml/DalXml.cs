using DalApi;
using Dal;
using System.Xml.Linq;

namespace DalXml
{
    sealed internal class DalXml : IDal
    {
        public Iproduct product { get; } = new Dal.DalProduct();
        public Iorder order { get; } = new Dal.DalOrder();
        public IorderItem orderItem { get; } = new Dal.DalOrderItem();


        public static List<Dal.DO.Product> ProductList = new();

        static readonly Random rand = new Random();

        public static List<Dal.DO.Product> AddListProducts()
        {
            XElement? root = XDocument.Load("..\\..\\Product.xml").Root;
            string[] productNames = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "g" };
            for (int i = 0; i < 10; i++)
            {
                Dal.DO.Product product = new();
                product.ID = config.ProductId;
                product.Name = productNames[i % 10];
                product.Price = (int)rand.Next(50, 450);
                product.Category = (Dal.DO.eCategory)(i % Enum.GetValues(typeof(Dal.DO.eCategory)).Length);
                if (i % 20 == 0)
                    product.InStock = 0;
                else
                    product.InStock = (int)rand.NextInt64(0, 1000);

                ProductsList.Add(product);
            }
        }


        public static class config
        {
            private static int _productId = 0;
            public static int ProductId { get { return _productId++; } }

            private static int _orderId = 100;
            public static int OrderId { get { return _orderId++; } }

            private static int _orderItemId = 1000;
            public static int OrderItemId { get { return _orderItemId++; } }

        }

    }
}