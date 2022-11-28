using DalApi;
using dalList;
namespace BlImplementation;

internal class BlProduct : BlApi.Iproduct
{
    IDal Dal = new DalList();
    public IEnumerable<BO.ProductForList> GetProducts()
    {
        IEnumerable<Dal.DO.Product> getProducts = Dal.product.ReadAll();
        if (getProducts.Count() <= 0)
        {
            throw new Exception();
        }
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
        return boProducts;
    }

    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        IEnumerable<Dal.DO.Product> getCatalog = Dal.product.ReadAll();
        if (getCatalog.Count() <= 0)
        {
            throw new Exception();
        }
        List<BO.ProductItem> Catalog = new List<BO.ProductItem>();
        foreach (Dal.DO.Product p in getCatalog)
        {
            BO.ProductItem productItem = new BO.ProductItem();
            productItem.ID = p.ID;
            productItem.Name = p.Name;
            productItem.Price = p.Price;
            productItem.Category = (BO.Enums.eCategory)p.Category;
            productItem.InStock = (p.InStock != 0 ? true : false);
            Catalog.Add(productItem);
        }
        return Catalog;
    }

    public BO.Product GetProductItemsForManager(int id)
    {
        if (id > 0)
        {
            Dal.DO.Product p = Dal.product.Read(id);
            BO.Product prod = new BO.Product();
            prod.ID = p.ID;
            prod.Name = p.Name;
            prod.Price = p.Price;
            prod.Category = (BO.Enums.eCategory)p.Category;
            prod.InStock = p.InStock;
            return prod;
        }
        throw new Exception();
    }

    public BO.Product GetProductItemsForCustomer(int id)
    {
        /// לשאול את המורה למה הכפילות?
        if (id > 0)
        {
            Dal.DO.Product p = Dal.product.Read(id);
            BO.Product prod = new BO.Product();
            prod.ID = p.ID;
            prod.Name = p.Name;
            prod.Price = p.Price;
            prod.Category = (BO.Enums.eCategory)p.Category;
            prod.InStock = p.InStock;
            return prod;
        }

        throw new Exception();
    }

    public void AddProduct(BO.Product p)
    {
        if (p.ID > 0 && p.Name != null && p.Price > 0 && p.InStock > 0)
        {
            Dal.DO.Product prod = new Dal.DO.Product();
            prod.ID = p.ID;
            prod.Name = p.Name;
            prod.Price = p.Price;
            prod.Category = (Dal.DO.eCategory)p.Category;
            prod.InStock = p.InStock;
            Dal.product.Add(prod);
        }
        else
        {
            throw new Exception();
        }
    }

    public void RemoveProduct(int id)
    {
        IEnumerable<Dal.DO.OrderItem> AllItems = Dal.orderItem.ReadAll();
        foreach (Dal.DO.OrderItem item in AllItems)
        {
            if (item.ID == id)
                Dal.orderItem.Delete(item.ID);
        }
        throw new Exception();
    }

    public void UpdateProduct(BO.Product p)
    {
        if (p.ID > 0 && p.Name != null && p.Price > 0 && p.InStock > 0)
        {
            Dal.DO.Product prod = new Dal.DO.Product();
            prod.ID = p.ID;
            prod.Name = p.Name;
            prod.Price = p.Price;
            prod.Category = (Dal.DO.eCategory)p.Category;
            prod.InStock = p.InStock;
            Dal.product.Update(prod);
        }
        else
        {
            throw new Exception();
        }
    }    
}


