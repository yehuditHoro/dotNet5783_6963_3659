using DalApi;
using Dal;
using BlApi;
using System.Security.Cryptography;

namespace BlImplementation;

internal class BlCart : BlApi.Icart
{
    IDal? Dal = DalApi.Factory.Get();
    /// <summary>
    /// the function add product to the cart
    /// </summary>
    /// <param name="c"></param>
    /// <param name="pId"></param>
    /// <returns></returns>
    /// <exception cref="BlIdNotFound"></exception>
    public BO.Cart addToCart(BO.Cart c, int pId)
    {
        try
        {
            Dal.DO.Product prod = Dal.product.ReadSingle(x => x.ID == pId);
            if (c.Items.Count() != 0)
            {
                BO.OrderItem? oi = c.Items.Find(x => x.ProductID == pId);
                if (oi?.ProductID == pId)
                {
                    if (prod.InStock > 0)
                    {
                        oi.Amount++;
                        oi.TotalPrice += oi.Price;
                        c.TotalPrice += oi.Price;
                        return c;
                    }
                    else throw new BlOutOfStockException();
                }
            }
            if (prod.InStock > 0)
            {
                BO.OrderItem newP = new BO.OrderItem();
                newP.ID = prod.ID;
                newP.Name = prod.Name;
                newP.Price = prod.Price;
                newP.ProductID = pId;
                newP.Amount = 1;
                newP.TotalPrice = prod.Price;
                c.Items?.Add(newP);
                c.TotalPrice += newP.Price;
            }
            else throw new BlOutOfStockException();
        }
        catch (Exception e)
        {
            throw new Exception("can't add this product " + e.Message);
        }
        return c;
    }

    /// <summary>
    /// the function gets the the id and the new quantity and update it
    /// </summary>
    /// <param name="c"></param>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    /// <exception cref="BlIdNotFound"></exception>
    public BO.Cart UpdateQuantity(BO.Cart c, int id, int quantity)
    {
        try
        {
            BO.OrderItem? oi = c.Items.Find(x => x.ProductID == id);
            if (oi?.ID == id)
            {
                if (quantity == 0)
                {
                    c.Items.Remove(oi);
                    c.TotalPrice -= oi.TotalPrice;
                }
                else if (quantity > oi.Amount)
                {
                    if (Dal?.product.ReadSingle(x => x.ID == id).InStock > 0)
                    {
                        c.TotalPrice += oi.Price * (quantity - oi.Amount);
                        oi.TotalPrice = quantity * oi.Price;
                        oi.Amount = quantity;
                    }
                }
                else if (quantity < oi.Amount)
                {
                    c.TotalPrice -= oi.Price * (oi.Amount - quantity);
                    oi.TotalPrice = quantity * oi.Price;
                    oi.Amount = quantity;
                }
            }
            else
            {
                throw new BlIdNotFound();
            }
        }
        catch (Exception e)
        {
            throw new Exception("can't update the quantity " + e.Message);
        }
        return c;
    }

    /// <summary>
    /// the function add the order with all the items of the cart and add it to the order list
    /// </summary>
    /// <param name="c"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="address"></param>
    /// <exception cref="Exception"></exception>
    public void MakeAnOrder(BO.Cart c, string name, string email, string address)
    {
        try
        {
            if (address == null || name == null)
                throw new BlNullException();
            if (IsValidEmail(email) == false)
                throw new BlInvalidInputException("the email is not correct");
            foreach (BO.OrderItem oi in c.Items)
            {
                //Dal?.product.ReadSingle(x => x.ID == oi.ID);
                if (oi.Amount < 0)
                    throw new BlInvalidInputException("invalid negative input for the amount");
                if (oi.Amount > Dal?.product.ReadSingle(x => x.ID == oi.ProductID).InStock)
                    throw new BlOutOfStockException();
            }
            Dal.DO.Order newOrder = new();
            newOrder.CustomerName = name;
            newOrder.CustomerEmail = email;
            newOrder.CustomerAddress = address;
            newOrder.OrderDate = DateTime.Now;
            newOrder.ShipDate = null;
            newOrder.DeliveryDate = null;
            int id = Dal.order.Add(newOrder);
            foreach (BO.OrderItem item in c.Items)
            {
                Dal.DO.OrderItem cartItem = new();
                cartItem.ID = 0;
                cartItem.Amount = item.Amount;
                cartItem.Price = item.Price;
                cartItem.OrderId = id;
                cartItem.ProductId = item.ProductID;
                Dal.orderItem.Add(cartItem);
                Dal.product.UpdateAmount(item.ID, item.Amount);
            }
        }
        catch (Exception e)
        {
            throw new Exception("cant make the order " + e.Message);
        }
    }

    /// <summary>
    /// the function do a validation on the email address
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();
        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}


