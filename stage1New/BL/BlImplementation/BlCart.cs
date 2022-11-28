using DalApi;
using dalList;


namespace BlImplementation;

internal class BlCart : BlApi.Icart
{
    IDal Dal = new DalList();
    public BO.Cart addToCart(BO.Cart c, int pId)
    {
        foreach (BO.OrderItem oi in c.Items)
        {
            if (oi.ID == pId)
            {
                if (Dal.product.Read(pId).InStock > 0)
                {
                    oi.Amount++;
                    oi.TotalPrice += oi.Price;
                    c.TotalPrice += oi.Price;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        Dal.DO.Product prod = Dal.product.Read(pId);
        if (prod.InStock > 0)
        {
            BO.OrderItem newP = new BO.OrderItem();

            newP.ID = (dalList.DataSource.config.OrderItemId);
            newP.Name = prod.Name;
            newP.Price = prod.Price;
            newP.ProductID = pId;
            newP.Amount = 1;
            newP.TotalPrice = prod.Price;
            c.Items.Add(newP);
            c.TotalPrice += newP.Price;
        }
        return c;
    }

    public BO.Cart UpdateQuantity(BO.Cart c, int id, int quantity)
    {
        foreach (BO.OrderItem oi in c.Items)
        {
            if (quantity == 0)
            {
                c.Items.Remove(oi);
                c.TotalPrice -= oi.TotalPrice;
            }
            if (oi.ID == id)
            {
                if (quantity > oi.Amount)
                {
                    if (Dal.product.Read(id).InStock > 0)
                    {
                        c.TotalPrice += oi.Price * (quantity - oi.Amount);
                        oi.TotalPrice = quantity * oi.Price;
                        oi.Amount = quantity;
                    }
                }
                if (quantity < oi.Amount)
                {
                    c.TotalPrice -= oi.Price * (oi.Amount - quantity);
                    oi.TotalPrice = quantity * oi.Price;
                    oi.Amount = quantity;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        return c;
    }

    public void MakeAnOrder(BO.Cart c, string name, string email, string address)
    {
        try
        {
            if (address == null || name == null || IsValidEmail(email) == false)
                throw new NotImplementedException();
            foreach (BO.OrderItem oi in c.Items)
            {
                Dal.product.Read(oi.ID);
                if (oi.Amount < 0 || oi.Amount < Dal.product.Read(oi.ID).InStock)
                    throw new NotImplementedException();
            }
            Dal.DO.Order newOrder = new();
            newOrder.ID = (dalList.DataSource.config.OrderId);
            newOrder.CustomerName = name;
            newOrder.CustomerEmail = email;
            newOrder.CustomerAddress = address;
            newOrder.OrderDate = DateTime.Now;
            newOrder.ShipDate = DateTime.MinValue;
            //newOrder.PaymentDate = DateTime.MinValue;
            newOrder.DeliveryDate = DateTime.MinValue;
            int id = Dal.order.Add(newOrder);
            /// לעשות את הפונקציה האחרונה בקארט
            List<Dal.DO.OrderItem> allItems = dalList.DataSource.OrderItemsList;
            //foreach (Dal.DO.OrderItem item in allItems)///רץ על כל הפריטים
            //{
            //    if (item.OrderId == newOrder.ID)
            //    {
            //        Dal.DO.OrderItem cartItem = new();
            //        cartItem.ID = item.ID;
            //        cartItem.OrderId = newOrder.ID;
            //        cartItem.
            //    }
            //}
            foreach (BO.OrderItem item in c.Items)///רץ על הסל קניה
            {
                Dal.DO.OrderItem cartItem = new();
                cartItem.ID=dalList.DataSource.config.OrderItemId;
                cartItem.Amount = item.Amount;  
                cartItem.Price = item.Price;
                cartItem.OrderId=newOrder.ID;
                cartItem.ProductId = item.ID;
                Dal.orderItem.Add(cartItem);
                Dal.product.UpdateAmount(item.ID, item.Amount);

            }
        }
        catch
        {

        }

    }

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


