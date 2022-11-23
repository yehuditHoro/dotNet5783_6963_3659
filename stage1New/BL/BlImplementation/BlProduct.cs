using DalApi;
using dalList;

namespace BlImplementation;

internal class BlProduct : BlApi.Iproduct
{
    IDal Dal = new DalList();

    public void AddProduct(BO.Product p)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.ProductItem> GetCatalog(BO.ProductItem catalog)
    {
        throw new NotImplementedException();
    }

    public BO.Product GetProductItemsForCustomer(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Product GetProductItemsForManager(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.ProductForList> GetProducts(BO.ProductForList productList)
    {
        throw new NotImplementedException();
    }

    public void RemoveProduct(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(BO.Product p)
    {
        throw new NotImplementedException();
    }
}

