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
                    return c;
                }
            }
        }
        Dal.DO.Product prod = Dal.product.Read(pId);
        if (prod.InStock > 0)
        {
            BO.OrderItem newP = new BO.OrderItem();
           
            newP.ID =(Dal.DataSource.config.OrderItemId);
            newP.Name = prod.Name;  
            newP.Price = prod.Price;
            newP.ProductID = pId;
            newP.Amount =1;
            newP.TotalPrice = prod.Price;
            c.Items.Add(newP);
            c.TotalPrice +=newP.Price;
            return c;
        }
        throw new NotImplementedException();
    }

    public void MakeAnOrder(BO.Cart c, string name, string email, string address)
    {
        throw new NotImplementedException();
    }

    public BO.Cart UpdateQuantity(BO.Cart c, int id, int quantity)
    {
        throw new NotImplementedException();
    }
}


