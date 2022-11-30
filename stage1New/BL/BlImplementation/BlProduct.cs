using BlApi;
using DalApi;
using dalList;
namespace BlImplementation;

internal class BlProduct : BlApi.Iproduct
{
    IDal Dal = new DalList();
    public IEnumerable<BO.ProductForList> GetProducts()
    {
        try
        {
            IEnumerable<Dal.DO.Product> getProducts = Dal.product.ReadAll();
            if (getProducts.Count() <= 0)
            {
                throw new BlFailedToGet();
            }
            List<BO.ProductForList> boProducts = new List<BO.ProductForList>();
            foreach (Dal.DO.Product p in getProducts)
            {
                BO.ProductForList bp = new BO.ProductForList();
                bp.ID = p.ID;
                bp.Name = p.Name;
                bp.Price = p.Price;
                bp.Category = (BO.Enums.eCategory)p.Category;
                boProducts.Add(bp);
            }
            return boProducts;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        try
        {
            IEnumerable<Dal.DO.Product> getCatalog = Dal.product.ReadAll();
            if (getCatalog.Count() <= 0)
            {
                throw new BlFailedToGet();
            }
            List<BO.ProductItem> Catalog = new List<BO.ProductItem>();
            foreach (Dal.DO.Product p in getCatalog)
            {
                BO.ProductItem productItem = new BO.ProductItem();
                productItem.ID = p.ID;
                productItem.Name = p.Name;
                productItem.Price = p.Price;
                productItem.Category = (BO.Enums.eCategory)p.Category;
                productItem.Amount = p.InStock;
                productItem.InStock = (p.InStock != 0 ? true : false);
                Catalog.Add(productItem);
            }
            return Catalog;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    public BO.Product GetProductItemsForManager(int id)
    {
        try
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
            else throw new BlInvalidInputException("invalid negative input");
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    public BO.Product GetProductItemsForCustomer(int id)
    {
        try
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
            else throw new BlInvalidInputException("invalid negative input");
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    public void AddProduct(BO.Product p)
    {
        try
        {
            if (p.Name == null)
                throw new BlNullException();
            if (p.Price < 0)
                throw new BlInvalidInputException("invalid negative input");
            if (p.InStock < 0)
                throw new BlOutOfStockException();
            Dal.DO.Product prod = new Dal.DO.Product();
            prod.ID = p.ID;
            prod.Name = p.Name;
            prod.Price = p.Price;
            prod.Category = (Dal.DO.eCategory)p.Category;
            prod.InStock = p.InStock;
            Dal.product.Add(prod);
        }
        catch (DalApi.EntityDuplicateException)
        {
            throw new BlEntityDuplicate();
        }
    }

    public void RemoveProduct(int id)
    {
        try
        {
            IEnumerable<Dal.DO.OrderItem> AllItems = Dal.orderItem.ReadAll();
            foreach (Dal.DO.OrderItem item in AllItems)
            {
                if (item.ID == id)
                    Dal.orderItem.Delete(item.ID);
            }
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    public void UpdateProduct(BO.Product p)
    {
        try
        {
            if (p.Name == null)
                throw new BlNullException();
            if (p.Price < 0)
                throw new BlInvalidInputException("invalid negative input");
            if (p.InStock < 0)
                throw new BlOutOfStockException();
            Dal.DO.Product prod = new Dal.DO.Product();
            prod.ID = p.ID;
            prod.Name = p.Name;
            prod.Price = p.Price;
            prod.Category = (Dal.DO.eCategory)p.Category;
            prod.InStock = p.InStock;
            Dal.product.Update(prod);
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }
}


