using BlApi;
using DalApi;
using System.Diagnostics;
using System.Xml.Linq;

namespace BlImplementation;

internal class BlProduct : BlApi.Iproduct
{
    private IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// the function returns all the products from the datasource
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BlFailedToGet"></exception>
    /// <exception cref="BlIdNotFound"></exception>
    public IEnumerable<BO.ProductForList?> GetProducts(BO.eCategory? category)
    {
        try
        {
            IEnumerable<Dal.DO.Product> getProducts;
            if (category == null)
                getProducts = dal.product.ReadAll();
            else { getProducts = dal.product.ReadAll(x => (BO.eCategory)x.Category == category); }
            if (getProducts.Count() <= 0)
            {
                throw new BlFailedToGet();
            }
            var boProducts = from Dal.DO.Product p in getProducts
                             select new BO.ProductForList
                             {
                                 ID = p.ID,
                                 Name = p.Name,
                                 Price = p.Price,
                                 Category = (BO.eCategory)p.Category
                             };
            return boProducts;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// the function returns all the products in the catalog 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BlFailedToGet"></exception>
    /// <exception cref="BlIdNotFound"></exception>
    /// 
    public IEnumerable<BO.ProductItem?> GetCatalog(BO.eCategory? category)
    {
        try
        {
            IEnumerable<Dal.DO.Product> getCatalog;
            if (category == null)
                getCatalog = dal.product.ReadAll();
            else
                getCatalog= dal.product.ReadAll(x => (BO.eCategory)x.Category == category);
            if (getCatalog.Count() <= 0)
            {
                throw new BlFailedToGet();
            }
            var Catalog = from Dal.DO.Product p in getCatalog
                          select new BO.ProductItem
                          {
                              ID = p.ID,
                              Name = p.Name,
                              Price = p.Price,
                              Category = (BO.eCategory)p.Category,
                              Amount = p.InStock,
                              InStock = (p.InStock != 0 ? true : false),
                          };
            return Catalog;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
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
            if (id >= 0)
            {
                Dal.DO.Product p = dal.product.ReadSingle(x => x.ID == id);
                BO.Product prod = new BO.Product();
                prod.ID = p.ID;
                prod.Name = p.Name;
                prod.Price = p.Price;
                prod.Category = (BO.eCategory)p.Category;
                prod.InStock = p.InStock;
                return prod;
            }
            else throw new BlInvalidInputException("invalid negative input");
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
        catch (Exception e)
        {
            throw new Exception("can't get this product " + e.Message);
        }
    }

    /// <summary>
    /// the function get id from the customer and returns the specific product for the costumer
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BlInvalidInputException"></exception>
    /// <exception cref="BlIdNotFound"></exception>
    public BO.ProductItem GetProductItemsForCustomer(int id, BO.Cart c)
    {
        try
        {
            if (id >= 0)
            {
                Dal.DO.Product p = dal.product.ReadSingle(x => x.ID == id);
                if (c.Items.Count() <= 0)
                    throw new BlNullException();
                BO.ProductItem pi = (from oi in c.Items
                                     where oi.ProductID == id
                                     select new BO.ProductItem
                                     {
                                         ID = oi.ID,
                                         Name = oi.Name,
                                         Price = oi.Price,
                                         Category = (BO.eCategory)p.Category,
                                         Amount = oi.Amount,
                                         InStock = p.InStock > 0 ? true : false
                                     }).First() ?? throw new BlNullException();
                return pi;
            }
            else throw new BlInvalidInputException("invalid negative input");
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
        catch (Exception e)
        {
            throw new Exception("can't get this product " + e.Message);
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
            if (p.Name == "")
                throw new BlNullException();
            if (p.Price < 0)
                throw new BlInvalidInputException("invalid negative input");
            if (p.InStock < 0)
                throw new BlOutOfStockException();
            if (p.ID < 0)
                throw new BlInvalidInputException("invalid negative id");
            Dal.DO.Product prod = new Dal.DO.Product();
            prod.ID = p.ID;
            prod.Name = p.Name;
            prod.Price = p.Price;
            prod.Category = (Dal.DO.eCategory)p.Category;
            prod.InStock = p.InStock;
            dal?.product.Add(prod);
        }
        catch (DalApi.EntityDuplicateException)
        {
            throw new BlEntityDuplicate();
        }
        catch (Exception e)
        {
            throw new Exception("can't add this product " + e.Message);
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
            IEnumerable<Dal.DO.OrderItem> AllOrderItems = dal.orderItem.ReadAll();
            AllOrderItems = AllOrderItems.Where(item => item.ProductId == id);
            if (AllOrderItems.Count() > 0)
                foreach (Dal.DO.OrderItem item in AllOrderItems)
                {
                    if (dal.order.ReadSingle(o => o.ID == item.OrderId).DeliveryDate > DateTime.Now)
                        throw new Exception("the product is already ordered");
                }
            dal.product.Delete(id);
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
        catch (Exception e)
        {
            throw new Exception("can't delete this product " + e.Message);
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
            dal?.product.Update(prod);
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
        catch (Exception e)
        {
            throw new Exception("can't update this product " + e.Message);
        }
    }
}


