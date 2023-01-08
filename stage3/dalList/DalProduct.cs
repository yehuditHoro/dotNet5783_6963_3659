using Dal.DO;
namespace dalList;
using DalApi;
public class DalProduct : Iproduct
{
    /// <summary>
    /// add a new product to the product list
    /// </summary>
    /// <param name="newProduct"></param>
    /// <returns></returns>
    /// <exception cref="EntityDuplicateException"></exception>
    public int Add(Product newProduct)
    {
        newProduct.ID = DataSource.config.ProductId;
        DataSource.ProductsList.Add(newProduct);
        return newProduct.ID;
    }
    /// <summary>
    /// get the specific product with the specific id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public Product Read(int id)
    {
        return DataSource.ProductsList.Find(p => p.ID == id);
        throw new EntityNotFoundException("this id doesn't exist");
    }
    /// <summary>
    ///  returns all the products in the products list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> ReadAll(Func<Product, bool>? func = null)
    {
        try
        {
            List<Product> allProducts = new(DataSource.ProductsList);
            return func == null ? allProducts : allProducts.Where(func);
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new EntityNotFoundException("entity not found");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public Product ReadSingle(Func<Product, bool> func)
    {
        return DataSource.ProductsList.Where(func).ToList()[0];
        throw new EntityNotFoundException("product not found");
    }
    /// <summary>
    /// update the product in the products list
    /// </summary>
    /// <param name="newProduct"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void Update(Product newProduct)
    {
        int idx = DataSource.ProductsList.FindIndex(O => O.ID == newProduct.ID);
        if (idx == -1)
            throw new EntityNotFoundException("this product doesn't exist");
        DataSource.ProductsList[idx] = newProduct;
        return;
    }

    /// <summary>
    /// gets id and delete the specific item of the products list
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void Delete(int id)
    {
        Product? p = DataSource.ProductsList.Find(O => O.ID == id);
        if (p == null)
            throw new EntityNotFoundException("This product does not exist");
        DataSource.ProductsList.Remove((Product)p);
        return;
    }
    /// <summary>
    /// gets id and new amount and change the amount of the item
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void UpdateAmount(int id, int amount)
    {
        for (int i = 0; i < DataSource.ProductsList.Count(); i++)
        {
            if (DataSource.ProductsList[i].ID == id)
            {
                Product p = DataSource.ProductsList[i];
                p.InStock -= amount;
                DataSource.ProductsList[i] = p;
                return;
            }
        }
        throw new EntityNotFoundException("this product doesn't exist");
    }
}
