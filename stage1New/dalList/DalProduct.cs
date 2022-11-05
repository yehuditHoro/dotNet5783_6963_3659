using Dal.DO;
namespace dalList;

public class DalProduct
{
    public static int Create(Product newProduct)
    {
        for (int i = 0; i < DataSource.config.indexProduct; i++)
        {
            if (DataSource.ProductsList[i].ID == newProduct.ID)
            {
                throw new Exception("this product already exist");
            }
        }
        DataSource.ProductsList[DataSource.config.indexProduct++] = newProduct;
        return newProduct.ID;
    }

    public static Product Read(int id)
    {
        for (int i = 0; i < DataSource.config.indexProduct; i++)
        {
            if (DataSource.ProductsList[i].ID == id)
            {
                return DataSource.ProductsList[i];
            }
        }
        throw new Exception("this id doesn't exist");
    }

    public static Product[] ReadAll()
    {
        Product[] allProducts = new Product[DataSource.config.indexProduct];
        for (int i = 0; i < DataSource.ProductsList.Length; i++)
        {
            allProducts[i] = DataSource.ProductsList[i];
        }
        return allProducts;
    }

    public static void Update(Product newProduct)
    {
        int id = newProduct.ID;
        for (int i = 0; i < DataSource.config.indexProduct; i++)
        {
            if (DataSource.ProductsList[i].ID == id)
            {
                DataSource.ProductsList[i] = newProduct;
                return;
            }
        }
        throw new Exception("this product doesn't exist");
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.config.indexProduct; i++)
        {
            if (DataSource.ProductsList[i].ID == id)
            {
                DataSource.ProductsList[i] = DataSource.ProductsList[DataSource.config.indexProduct];
                DataSource.config.indexProduct--;
                return;
            }
        }
        throw new Exception("this product doesn't exist");
    }
}
