
//namespace dalTest;
using dalList;
using Dal.DO;

eOptions choice;
void main()
{
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
}
main();
Product newProduct()
{
    DataSource.config.indexProduct++;
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
            DalProduct.Create(product);
            break;
        case eCRUD.READ:
            Console.WriteLine("enter the id of the product you want to see");
            int id = Convert.ToInt32(Console.ReadLine());
            DalProduct.Read(id);
            break;
        case eCRUD.READALL:
            DalProduct.ReadAll();
            break;
        case eCRUD.UPDATE:
            Product p = newProduct();
            DalProduct.Update(p);
            break;
        case eCRUD.DELETE:
            Console.WriteLine("enter the id of the product you want to delete");
            id = Convert.ToInt32(Console.ReadLine());
            DalProduct.Delete(id);
            break;
    }
}

Order newOrder()
{
    DataSource.config.indexOrder++;
    Order order = new Order();
    Console.WriteLine("enter the customer name of the order");
    order.CustomerName = Console.ReadLine();
    Console.WriteLine("enter the customer email of the order");
    order.CustomerEmail = Console.ReadLine();
    Console.WriteLine("enter the customer address of the order");
    order.CustomerAddress = Console.ReadLine();
    order.OrderDate = DateTime.Today;
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
            DalOrder.Create(order);
            break;
        case eCRUD.READ:
            Console.WriteLine("enter the id of the order you want to see");
            int id = Convert.ToInt32(Console.ReadLine());
            DalOrder.Read(id);
            break;
        case eCRUD.READALL:
            DalOrder.ReadAll();
            break;
        case eCRUD.UPDATE:
            Order o = newOrder();
            DalOrder.Update(o);
            break;
        case eCRUD.DELETE:
            Console.WriteLine("enter the id of the order you want to delete");
            id = Convert.ToInt32(Console.ReadLine());
            DalOrder.Delete(id);
            break;
    }

}

OrderItem newOrderItem()
{
    DataSource.config.indexOrderItem++;
    OrderItem orderItem = new OrderItem();
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
            OrderItem orderItem = newOrderItem();
            dalList.DalOrderItem.Create(orderItem);
            break;
        case eCRUD.READ:
            Console.WriteLine("enter the id of the order item you want to see");
            int id = Convert.ToInt32(Console.ReadLine());
            DalOrderItem.Read(id);
            break;
        case eCRUD.READALL:
            DalOrderItem.ReadAll();
            break;
        case eCRUD.UPDATE:
            OrderItem oi = newOrderItem();
            DalOrderItem.Update(oi);
            break;
        case eCRUD.DELETE:
            Console.WriteLine("enter the id of the order item you want to delete");
            id = Convert.ToInt32(Console.ReadLine());
            DalOrderItem.Delete(id);
            break;
    }
}
