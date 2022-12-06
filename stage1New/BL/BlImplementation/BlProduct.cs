using BlApi;
using DalApi;
using dalList;
namespace BlImplementation;

internal class BlProduct : BlApi.Iproduct
{
    IDal Dal = new DalList();
    /// <summary>
    /// the function returns all the products from the datasource
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BlFailedToGet"></exception>
    /// <exception cref="BlIdNotFound"></exception>
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
    /// <summary>
    /// the function returns all the products in the catalog 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BlFailedToGet"></exception>
    /// <exception cref="BlIdNotFound"></exception>
    /// 


    public IEnumerable<BO.ProductForList> GetByFilter(Func<BO.ProductForList, bool>? func = null)
    {
        IEnumerable<BO.ProductForList> filteredProducts = GetProducts();

        return func == null ? filteredProducts : filteredProducts.Where(func);

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
    /// <summary>
    /// the function get id from the manager and returns the specific product for the costumer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BlInvalidInputException"></exception>
    /// <exception cref="BlIdNotFound"></exception>
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
    /// <summary>
    /// the function get id from the customer and returns the specific product for the costumer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BlInvalidInputException"></exception>
    /// <exception cref="BlIdNotFound"></exception>
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
    /// <summary>
    /// gets the new product and add send it to the add function in the dalProduct
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="BlNullException"></exception>
    /// <exception cref="BlInvalidInputException"></exception>
    /// <exception cref="BlOutOfStockException"></exception>
    /// <exception cref="BlEntityDuplicate"></exception>
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
    /// <summary>
    /// gets the id of the product and sent it to the delete function in the dalProduct to
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BlIdNotFound"></exception>
    public void RemoveProduct(int id)
    {
        try
        {
            IEnumerable<Dal.DO.Product> AllProducts = Dal.product.ReadAll();
            foreach (Dal.DO.Product item in AllProducts)
            {
                if (item.ID == id)
                    Dal.product.Delete(item.ID);
            }
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }
    /// <summary>
    ///  the function gets the id of the product and update it
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="BlNullException"></exception>
    /// <exception cref="BlInvalidInputException"></exception>
    /// <exception cref="BlOutOfStockException"></exception>
    /// <exception cref="BlIdNotFound"></exception>
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


