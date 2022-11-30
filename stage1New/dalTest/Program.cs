
//namespace dalTest;
using dalList;
using Dal.DO;
using DalApi;

eOptions choice;
IDal idal = new DalList();

try
    {
        DataSource ds = new DataSource();
        do
        {
            Console.WriteLine("enter 0 to exit \n enter 1 to see the product options \n enter 2 to see the order options \n enter 3 to see the order item options \n ");
            choice = (eOptions)Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case eOptions.EXIT:
                    break;
                case eOptions.PRODUCT:
                    ProductOption();
                    break;
                case eOptions.ORDER:
                    OrderOption();
                    break;
                case eOptions.ORDERITEM:
                    OrderItemOption();
                    break;
            }
        } while (choice != 0);
    }
    catch (Exception msg) { Console.WriteLine(msg); }

Product newProduct()
{
    Product product = new Product();
    Console.WriteLine("enter the name of the product");
    product.Name = Console.ReadLine();
    Console.WriteLine("enter the price of the product");
    product.Price = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("choose the category: \n enter 0 to men \n enter 1 to women \n enter 2 to boys \n enter 3 to girls \n enter 4 to babies");
    product.Category = (eCategory)Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("enter the amount in the stock");
    product.InStock = Convert.ToInt32(Console.ReadLine());
    return product;
}


void ProductOption()
{
    eCRUD num;
    Console.WriteLine("enter 0 to add a product \n enter 1 to see cpesific product \n enter 2 to see all the products \n enter 3 to update product \n enter 4 to delete product");
    num = (eCRUD)Convert.ToInt32(Console.ReadLine());
    switch (num)
    {
        case eCRUD.ADD:
            Product product = newProduct();
            product.ID = 0;
            idal.product.Add(product);
            break;
        case eCRUD.READ:
            Console.WriteLine("enter the id of the product you want to see");
            int id = Convert.ToInt32(Console.ReadLine());
            Product specificProduct=idal.product.Read(id);
            Console.WriteLine(specificProduct.ToString());
            break;
        case eCRUD.READALL:
            Product[] allProducts= (Product[])idal.product.ReadAll();
            for(int i = 0; i < allProducts.Length;i++)
            {
                Console.WriteLine(allProducts[i].ToString());
            }
            break;
        case eCRUD.UPDATE:
            Console.WriteLine("enter the id of the product you want to update");
            id = Convert.ToInt32(Console.ReadLine());
            Product updateProduct = idal.product.Read(id);
            Console.WriteLine(updateProduct.ToString());
            Product p = newProduct();
            p.ID = id;
            idal.product.Update(p);
            break;
        case eCRUD.DELETE:
            Console.WriteLine("enter the id of the product you want to delete");
            id = Convert.ToInt32(Console.ReadLine());
            idal.product.Delete(id);
            break;
    }
}

Order newOrder()
{
    Order order = new Order();
    order.ID = DataSource.config.OrderId;
    Console.WriteLine("enter the customer name of the order");
    order.CustomerName = Console.ReadLine();
    Console.WriteLine("enter the customer email of the order");
    order.CustomerEmail = Console.ReadLine();
    Console.WriteLine("enter the customer address of the order");
    order.CustomerAddress = Console.ReadLine();
    order.OrderDate = DateTime.Now;
    TimeSpan shipDate = TimeSpan.FromDays(14);
    order.ShipDate = order.OrderDate + shipDate;
    TimeSpan delivDate = TimeSpan.FromDays(6);
    order.DeliveryDate = order.ShipDate + delivDate;
    return order;
}

void OrderOption()
{
    eCRUD num;
    Console.WriteLine("enter 0 to add an order \n enter 1 to see cpesific order \n enter 2 to see all the orders \n enter 3 to update order \n enter 4 to delete order");
    num = (eCRUD)Convert.ToInt32(Console.ReadLine());
    switch (num)
    {
        case eCRUD.ADD:
            Order order = newOrder();
            idal.order.Add(order);
            break;
        case eCRUD.READ:
            Console.WriteLine("enter the id of the order you want to see");
            int id = Convert.ToInt32(Console.ReadLine());
            Order specificOrder = idal.order.Read(id);
            Console.WriteLine(specificOrder.ToString());
            break;
        case eCRUD.READALL:
            Order[] allOrders = (Order[])idal.order.ReadAll();
            for (int i = 0; i < allOrders.Length; i++)
            {
                Console.WriteLine(allOrders[i].ToString());
            }
            break;
        case eCRUD.UPDATE:
            Console.WriteLine("enter the id of the order you want to update");
            id = Convert.ToInt32(Console.ReadLine());
            Order updateOrder = idal.order.Read(id);
            Console.WriteLine(updateOrder.ToString());
            Order o = newOrder();
            o.ID = id;
            idal.order.Update(o);
            break;
        case eCRUD.DELETE:
            Console.WriteLine("enter the id of the order you want to delete");
            id = Convert.ToInt32(Console.ReadLine());
            idal.order.Delete(id);
            break;
    }

}

OrderItem NewOrderItem()
{
   
    OrderItem orderItem = new OrderItem();
    orderItem.ID = DataSource.config.OrderItemId;
    Console.WriteLine("enter the product id ");
    orderItem.ProductId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("enter the order id ");
    orderItem.OrderId = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("enter the price of the item");
    orderItem.Price = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("enter the amoubt of the item");
    orderItem.Amount = Convert.ToInt32(Console.ReadLine());
    return orderItem;
}

void OrderItemOption()
{
    eCRUD num;
    Console.WriteLine("enter 0 to add an order item \n enter 1 to see cpesific order item \n enter 2 to see all the order items \n enter 3 to update order item \n enter 4 to delete order item");
    num = (eCRUD)Convert.ToInt32(Console.ReadLine());
    switch (num)
    {
        case eCRUD.ADD:
            OrderItem orderItem = NewOrderItem();
            idal.orderItem.Add(orderItem);
            break;
        case eCRUD.READ:
            Console.WriteLine("enter the id of the order item you want to see");
            int id = Convert.ToInt32(Console.ReadLine());
            OrderItem specificOrderItem = idal.orderItem.Read(id);
            Console.WriteLine(specificOrderItem.ToString());
            break;
        case eCRUD.READALL:
            OrderItem[] allOrderItems = (OrderItem[])idal.orderItem.ReadAll();
            for (int i = 0; i < allOrderItems.Length; i++)
            {
                Console.WriteLine(allOrderItems[i].ToString());
            }
            break;
        case eCRUD.UPDATE:
            Console.WriteLine("enter the id of the order item you want to update");
            id = Convert.ToInt32(Console.ReadLine());
            OrderItem updateOrderItem = idal.orderItem.Read(id);
            Console.WriteLine(updateOrderItem.ToString());
            OrderItem oi = NewOrderItem();
            oi.ID = id;
            idal.orderItem.Update(oi);
            break;
        case eCRUD.DELETE:
            Console.WriteLine("enter the id of the order item you want to delete");
            id = Convert.ToInt32(Console.ReadLine());
            idal.orderItem.Delete(id);
            break;
    }
}
