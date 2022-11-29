// See https://aka.ms/new-console-template for more information
using BlApi;
using BlImplementation;

IBl ibl = new Bl();
BO.Enums.eMenuOptions choice;
try
{
    do
    {
        Console.WriteLine("enter 0 to exit \n enter 1 to see the product options \n enter 2 to see the order options \n enter 3 to see the cart options \n ");
        choice = (BO.Enums.eMenuOptions)Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case BO.Enums.eMenuOptions.EXIT:
                break;
            case BO.Enums.eMenuOptions.PRODUCT:
                ProductOption();
                break;
            case BO.Enums.eMenuOptions.ORDER:
                OrderOption();
                break;
            case BO.Enums.eMenuOptions.CART:
                CartOption();
                break;
        }
    } while (choice != 0);
}
catch (Exception msg) { Console.WriteLine(msg); }

BO.Product newProduct()
{
    BO.Product p = new();
    Console.WriteLine("enter the name of the product");
    p.Name = Console.ReadLine();
    Console.WriteLine("enter the price of the product");
    p.Price = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("enter the category of the product");
    p.Category = (BO.Enums.eCategory)Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("enter the amount in stock of the product");
    p.InStock = Convert.ToInt32(Console.ReadLine());
    return p;
}

void ProductOption()
{
    BO.Enums.eProductOptions num;
    Console.WriteLine("enter 0 to get all the products " +
        "\n enter 1 to get the catalog " +
        "\n enter 2 to get the items for manager " +
        "\n enter 3 to get the items for customer " +
        "\n enter 4 to add product " +
        "\n enter 5 to update product " +
        "\n enter 6 to delete product");
    num = (BO.Enums.eProductOptions)Convert.ToInt32(Console.ReadLine());
    switch (num)
    {
        case BO.Enums.eProductOptions.GetProducts:
            List<BO.ProductForList> allProducts = (List<BO.ProductForList>)ibl.product.GetProducts();
            foreach (BO.ProductForList productItem in allProducts)
            {
                Console.WriteLine(productItem);
            }
            break;
        case BO.Enums.eProductOptions.GetCatalog:
            List<BO.ProductItem> catalog = (List<BO.ProductItem>)ibl.product.GetCatalog();
            foreach (BO.ProductItem catalogItem in catalog)
            {
                Console.WriteLine(catalogItem);
            }
            break;
        case BO.Enums.eProductOptions.GetItemsManager:
            Console.WriteLine("enter the id of the product that you want to get");
            int id = Convert.ToInt32(Console.ReadLine());
            BO.Product p = ibl.product.GetProductItemsForManager(id);
            Console.WriteLine(p);
            break;
        case BO.Enums.eProductOptions.GetItemsCustomer:
            Console.WriteLine("enter the id of the product that you want to get");
            int Id = Convert.ToInt32(Console.ReadLine());
            p = ibl.product.GetProductItemsForCustomer(Id);
            Console.WriteLine(p);
            break;
        case BO.Enums.eProductOptions.Add:
            //how to increase the id?
            BO.Product product = newProduct();
            ibl.product.AddProduct(product);
            break;
        case BO.Enums.eProductOptions.Update:
            BO.Product prod = newProduct();
            ibl.product.UpdateProduct(prod);
            break;
        case BO.Enums.eProductOptions.Remove:
            Console.WriteLine("enter the id of the product that you want to delete");
            int ID = Convert.ToInt32(Console.ReadLine());
            ibl.product.RemoveProduct(ID);
            break;
    }
}

void OrderOption()
{
    BO.Enums.eOrderOptions num;
    Console.WriteLine("enter 0 to get all the orders " +
        "\n enter 1 to get single order " +
        "\n enter 2 to update the ship date " +
        "\n enter 3 to update the delivery date ");
    num = (BO.Enums.eOrderOptions)Convert.ToInt32(Console.ReadLine());
    switch (num)
    {
        case BO.Enums.eOrderOptions.GetOrders:
            List<BO.OrderForList> allOrders = (List<BO.OrderForList>)ibl.order.GetOrdersList();
            foreach (BO.OrderForList orderItem in allOrders)
            {
                Console.WriteLine(orderItem);
            }
            break;
        case BO.Enums.eOrderOptions.GetOrder:
            Console.WriteLine("enter the id of the order that you want to get");
            int id = Convert.ToInt32(Console.ReadLine());
            BO.Order order = ibl.order.GetOrder(id);
            Console.WriteLine(order);
            break;
        case BO.Enums.eOrderOptions.ShipedOrder:
            Console.WriteLine("enter the id of the order that you want to update her ship date");
            int Id = Convert.ToInt32(Console.ReadLine());
            ibl.order.ShipedOrder(Id);
            break;
        case BO.Enums.eOrderOptions.DeliveredOrder:
            Console.WriteLine("enter the id of the order that you want to update her delivery date");
            int ID = Convert.ToInt32(Console.ReadLine());
            ibl.order.DeliveredOrder(ID);
            break;
    }
}

BO.Cart newCart()
{
    BO.Cart cart = new();
    Console.WriteLine("enter the customer name");
    cart.CustomerName = Console.ReadLine();
    Console.WriteLine("enter the customer email");
    cart.CustomerEmail = Console.ReadLine();
    Console.WriteLine("enter the customer address");
    cart.CustomerAddress = Console.ReadLine();
    Console.WriteLine("");
    //המשתמש אמור להכניס את כל הפריטים ומחיר כולל?
    return cart;
}

void CartOption()
{
    BO.Enums.eCartOptions num;
    Console.WriteLine("enter 0 to get all the orders " +
        "\n enter 1 to get single order " +
        "\n enter 2 to update the ship date " +
        "\n enter 3 to update the delivery date ");
    num = (BO.Enums.eCartOptions)Convert.ToInt32(Console.ReadLine());
    switch (num)
    {
        case BO.Enums.eCartOptions.Add:
            Console.WriteLine("enter the product id that you want add to your cart");
            int productId = Convert.ToInt32(Console.ReadLine());
            BO.Cart c = newCart();
            ibl.cart.addToCart(c, productId);
            break;
        case BO.Enums.eCartOptions.UpdateQuantity:
            Console.WriteLine("enter the product id that you want to update his quantity");
            int PId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter the amount that you want to change to");
            int quantity = Convert.ToInt32(Console.ReadLine());
            BO.Cart cart = newCart();
            ibl.cart.UpdateQuantity(cart, PId, quantity);
            break;
        case BO.Enums.eCartOptions.MakeOrder: //???????????
            Console.WriteLine("enter the id of the order that you want to update her ship date");
            int Id = Convert.ToInt32(Console.ReadLine());
            ibl.order.ShipedOrder(Id);
            break;
    }
}