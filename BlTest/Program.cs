// See https://aka.ms/new-console-template for more information
using BlApi;
using BlImplementation;
using dalList;

IBl ibl = new Bl();
BO.Enums.eMenuOptions choice;
BO.Cart gCart = new BO.Cart();

try
{
    do
    {
        dalList.DataSource ds = new DataSource();
        Console.WriteLine("enter 0 to exit \n enter 1 to see the product options \n enter 2 to see the order options \n enter 3 to see the cart options \n ");
        choice = (BO.Enums.eMenuOptions)Convert.ToInt32(Console.ReadLine());
        if (choice is < 0 or > (BO.Enums.eMenuOptions)4) 
            throw new BlInvalidInputException("invalid input, you should enter input between 0 to 3");
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
catch(BlIdNotFound msg) { Console.WriteLine(msg); } 
catch(BlEntityDuplicate msg) { Console.WriteLine(msg); }
catch (BlFailedToAdd msg) { Console.WriteLine(msg); }
catch (BlFailedToGet msg) { Console.WriteLine(msg); }
catch (BlFailedToUpdate msg) { Console.WriteLine(msg); }
catch (BlFailedToDelete msg) { Console.WriteLine(msg); }
catch (BlInvalidInputException msg) { Console.WriteLine(msg); }
catch (BlOutOfStockException msg) { Console.WriteLine(msg); }
catch (BlNullException msg) { Console.WriteLine(msg); }
catch (Exception msg) { Console.WriteLine(msg); }

/// create a new product
BO.Product newProduct(int id)
{

    BO.Product p = new();
    p.ID =id;
    Console.WriteLine("enter the name of the product");
    p.Name = Console.ReadLine();
    if (p.Name == null)
        throw new BlNullException();
    Console.WriteLine("enter the price of the product");
    p.Price = Convert.ToDouble(Console.ReadLine());
    if(p.Price < 0) 
        throw new BlInvalidInputException("invalid negative input");
    Console.WriteLine("enter the category of the product");
    string i = Console.ReadLine();
    if(!Enum.TryParse(i, out BO.Enums.eCategory i_))
    {
        throw new BlInvalidInputException("cant get the category of the item");
    }
    p.Category = i_; 
    if (p.Category is < 0 or > (BO.Enums.eCategory)4) 
        throw new BlInvalidInputException("invalid input, you should enter input between 0 to 4");
    Console.WriteLine("enter the amount in stock of the product");
    p.InStock = Convert.ToInt32(Console.ReadLine());
    if(p.InStock < 0) 
        throw new BlInvalidInputException("invalid negative input");
    return p;
}
/// show the product options
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
    if (num is < 0 or > (BO.Enums.eProductOptions)6)
        throw new BlInvalidInputException("invalid input, you should enter input between 0 to 6");
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
            if (id < 0) throw new BlInvalidInputException("invalid negative input");
            BO.Product p = ibl.product.GetProductItemsForManager(id);
            Console.WriteLine(p);
            break;
        case BO.Enums.eProductOptions.GetItemsCustomer:
            Console.WriteLine("enter the id of the product that you want to get");
            int Id = Convert.ToInt32(Console.ReadLine());
            if (Id < 0) throw new BlInvalidInputException("invalid negative input");
            p = ibl.product.GetProductItemsForCustomer(Id);
            Console.WriteLine(p);
            break;
        case BO.Enums.eProductOptions.Add:
            BO.Product product = newProduct(0);
            ibl.product.AddProduct(product);
            break;
        case BO.Enums.eProductOptions.Update:
            int pid = 10;
            //Console.WriteLine("enter the name");
            //string name = Console.ReadLine();
            //int pid;
            //List<BO.ProductForList> AllProducts = (List<BO.ProductForList>)ibl.product.GetProducts();
            //foreach (BO.ProductForList productItem in AllProducts)
            //{
            //   if(productItem.Name == name)
            //    {
            //         pid= productItem.ID;
            //    }
            //}
            BO.Product prod =newProduct(pid);
            ibl.product.UpdateProduct(prod);
            break;
        case BO.Enums.eProductOptions.Remove:
            Console.WriteLine("enter the id of the product that you want to delete");
            int ID = Convert.ToInt32(Console.ReadLine());
            if (ID < 0) throw new BlInvalidInputException("invalid negative input");
            ibl.product.RemoveProduct(ID);
            break;
    }
}
/// show the order options
void OrderOption()
{
    BO.Enums.eOrderOptions num;
    Console.WriteLine("enter 0 to get all the orders " +
        "\n enter 1 to get single order " +
        "\n enter 2 to update the ship date " +
        "\n enter 3 to update the delivery date ");
    num = (BO.Enums.eOrderOptions)Convert.ToInt32(Console.ReadLine());
    if (num is < 0 or > (BO.Enums.eOrderOptions)3)
        throw new BlInvalidInputException("invalid input, you should enter input between 0 to 3");
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
            if (id < 0) throw new BlInvalidInputException("invalid negative input");
            BO.Order order = ibl.order.GetOrder(id);
            Console.WriteLine(order);
            break;
        case BO.Enums.eOrderOptions.ShipedOrder:
            Console.WriteLine("enter the id of the order that you want to update her ship date");
            int Id = Convert.ToInt32(Console.ReadLine());
            if (Id < 0) throw new BlInvalidInputException("invalid negative input");
            ibl.order.ShipedOrder(Id);///failed here why???
            break;
        case BO.Enums.eOrderOptions.DeliveredOrder:
            Console.WriteLine("enter the id of the order that you want to update her delivery date");
            int ID = Convert.ToInt32(Console.ReadLine());
            if (ID < 0) throw new BlInvalidInputException("invalid negative input");
            ibl.order.DeliveredOrder(ID);
            break;
    }
}

/// show the cart options
void CartOption()
{
    BO.Enums.eCartOptions num;
    Console.WriteLine("enter 0 to add product to the cart " +
        "\n enter 1 to update the quantity of a product " +
        "\n enter 2 to make an order ");
    num = (BO.Enums.eCartOptions)Convert.ToInt32(Console.ReadLine());
    if (num is < 0 or > (BO.Enums.eCartOptions)2)
        throw new BlInvalidInputException("invalid input, you should enter input between 0 to 2");
    switch (num)
    {
        case BO.Enums.eCartOptions.Add:
            Console.WriteLine("enter the product id that you want add to your cart");
            int productId = Convert.ToInt32(Console.ReadLine());
            if (productId < 0) throw new BlInvalidInputException("invalid negative input");
            gCart = ibl.cart.addToCart(gCart, productId);
            break;
        case BO.Enums.eCartOptions.UpdateQuantity:
            Console.WriteLine("enter the product id that you want to update his quantity");
            int PId = Convert.ToInt32(Console.ReadLine());
            if (PId < 0) throw new BlInvalidInputException("invalid negative input");
            Console.WriteLine("enter the amount that you want to change to");
            int quantity = Convert.ToInt32(Console.ReadLine());
            if (quantity < 0) throw new BlInvalidInputException("invalid negative input");
            gCart = ibl.cart.UpdateQuantity(gCart, PId, quantity);
            break;
        case BO.Enums.eCartOptions.MakeOrder:
            Console.WriteLine("enter customer name");
            string Name = Console.ReadLine();
            if (Name == null) throw new BlNullException();
            Console.WriteLine("enter customer email");
            string Email = Console.ReadLine();
            if (Email == null) throw new BlNullException();
            Console.WriteLine("enter customer address");
            string Address = Console.ReadLine();
            if (Address == null) throw new BlNullException();
            ibl.cart.MakeAnOrder(gCart, Name, Email, Address);
            break;
    }
}