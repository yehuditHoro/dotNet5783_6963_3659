using Dal.DO;
namespace dalList;
using DalApi;
public class DalProduct:Iproduct
{
    public int Add(Product newProduct)
    {
        for (int i = 0; i < DataSource.ProductsList.Count(); i++)
        {
            if (DataSource.ProductsList[i].ID == newProduct.ID)
            {
                throw new EntityDuplicateException("this product already exist");
            }
        }
        DataSource.ProductsList.Add(newProduct);
        return newProduct.ID;
    }

    public Product Read(int id)
    {
        for (int i = 0; i < DataSource.ProductsList.Count(); i++)
        {
            if (DataSource.ProductsList[i].ID == id)
            {
                return DataSource.ProductsList[i];
            }
        }
        throw new EntityDuplicateException("this id doesn't exist");
    }

    public  IEnumerable<Product> ReadAll()
    {
        List<Product> allProducts = new List<Product>();
        for (int i = 0; i < DataSource.ProductsList.Count(); i++)
        {
            allProducts[i] = DataSource.ProductsList[i];
            
        }
        return allProducts;
    }
    
    public void Update(Product newProduct)
    {
        int id = newProduct.ID;
        for (int i = 0; i < DataSource.ProductsList.Count(); i++)
        {
            if (DataSource.ProductsList[i].ID == id)
            {
                DataSource.ProductsList[i] = newProduct;
                return;
            }
        }
        throw new EntityDuplicateException("this product doesn't exist");
    }

    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.ProductsList.Count(); i++)
        {
            if (DataSource.ProductsList[i].ID == id)
            {
                DataSource.ProductsList.Remove(DataSource.ProductsList[i]);
                return;
            }
        }
        throw new EntityDuplicateException("this product doesn't exist");
    }

    public void UpdateAmount(int id,int amount)
    {
        for (int i = 0; i < DataSource.ProductsList.Count(); i++)
        {
            if (DataSource.ProductsList[i].ID == id)
            {
                Product p = DataSource.ProductsList[i];
                p.InStock-=amount;
                DataSource.ProductsList[i] = p;
                return;
            }
        }
        throw new EntityDuplicateException("this product doesn't exist");
    }
}
