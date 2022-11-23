using DalApi;
using dalList;

namespace BlImplementation;

internal class BlProduct : BlApi.Iproduct
{
    IDal Dal = new DalList();

    public IEnumerable<BO.ProductForList> GetProducts()
    {
        IEnumerable<Dal.DO.Product> getProducts = Dal.product.ReadAll();
        List<BO.ProductForList> boProducts = new List<BO.ProductForList>();
        foreach (Dal.DO.Product p in getProducts)
        {
            BO.ProductForList bp = new BO.ProductForList();
            bp.ID = p.ID;
            bp.Name = p.Name;
            bp.Price = p.Price;
            bp.Category = (BO.Enums.eCategory)p.Category;
            //bp.InStock = p.InStock;
            boProducts.Add(bp);
        }

        throw new NotImplementedException();
    }

    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public BO.Product GetProductItemsForManager(int id)
    {
        throw new NotImplementedException();
    }
    public BO.Product GetProductItemsForCustomer(int id)
    {
        throw new NotImplementedException();
    }

    public void AddProduct(BO.Product p)
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

