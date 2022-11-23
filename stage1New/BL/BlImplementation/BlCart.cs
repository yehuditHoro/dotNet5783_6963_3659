using DalApi;
using dalList;

namespace BlImplementation;

internal class BlCart : BlApi.Icart
{
    IDal Dal = new DalList();
    public BO.Cart addToCart(BO.Cart c, int id)
    {
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


