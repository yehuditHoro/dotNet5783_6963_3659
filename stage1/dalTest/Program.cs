
namespace dalTest;
using dalList;
namespace dalfacade.DO;

public void main()
{
    Console.WriteLine("enter 0 to exit \n enter 1 to see the product options \n enter 2 to see the order options \n enter 3 to see the order item options \n ")
    options choice;
    choice = (options)Convert.ToInt32(Console.ReadLine());
    switch (choice) {
        case 'EXIT':
            break;
        case 'PRODUCT':
            ProductOption();
            break;
        case 'ORDER':
            OrderOption();
            break;
        case 'ORDERITEM':
            OrderItemOption();
            break;
    }

    public Product newProduct()
    {
        DataSource.config.indexProduct++;
        Product product = new Product();
        Console.WriteLine("enter the name of the product");
        product.Name = Console.ReadLine();
        Console.WriteLine("enter the price of the product");
        product.Price = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("choose the category: \n enter 0 to men \n enter 1 to women \n enter 2 to boys \n enter 3 to girls \n enter 4 to babies");
        product.Category = (eCtegory)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter the amount in the stock");
        product.InStock = Convert.ToInt32(Console.ReadLine());
        return product;
    }

    public void ProductOption(){
        Console.WriteLine("enter 0 to add a product \n enter 1 to see cpesific product \n enter 2 to see all the products \n enter 3 to update product \n enter 4 to delete product");
        int num = (CRUD)Convert.ToInt32(Console.ReadLine());
        switch(num){
            case 'ADD':
                Product product = newProduct();
                DalProduct.Create(product);
                break;
            case 'READ':
                Console.WriteLine("enter the id of the product you want to see");
                int id = Convert.ToInt32(Console.ReadLine());
                DalProduct.Rrad(id);
                break;
            case 'READALL':
                DalProduct.RradAll();
                break;
            case 'UPDATE':
                Product product = newProduct();
                DalProduct.Update(product);
                break;
            case 'DELETE':
                Console.WriteLine("enter the id of the product you want to delete");
                int id = Convert.ToInt32(Console.ReadLine());
                DalProduct.Delete(id);
                break;
        }
    }

    public Order newOrder()
    {
        DataSource.config.indexOrder++;
        Order order = new Order();
        Console.WriteLine("enter the customer name of the order");
        order.CustomerName = Console.ReadLine();
        Console.WriteLine("enter the customer email of the order");
        order.CustomerEmail = Console.ReadLine();
        Console.WriteLine("enter the customer address of the order");
        order.CustomerAddress = Console.ReadLine();
        return product;
    }

    public void OrderOption()
    {
        Console.WriteLine("enter 0 to add an order \n enter 1 to see cpesific order \n enter 2 to see all the orders \n enter 3 to update order \n enter 4 to delete order");
        int num = (CRUD)Convert.ToInt32(Console.ReadLine());
        switch(num){
            case 'ADD':
                Order order = newOrder();
                DalOrder.Create(order);
                break;
            case 'READ':
                Console.WriteLine("enter the id of the order you want to see");
                int id = Convert.ToInt32(Console.ReadLine());
                DalOrder.Rrad(id);
                break;
            case 'READALL':
                DalOrder.RradAll();
                break;
            case 'UPDATE':
                Order order = newOrder();
                DalOrder.Update(order);
                break;
            case 'DELETE':
                Console.WriteLine("enter the id of the order you want to delete");
                int id = Convert.ToInt32(Console.ReadLine());
                DalOrder.Delete(id);
                break;
        }
    }
}