
using BO;
namespace BlApi;

public interface Iproduct
{
    public IEnumerable<ProductForList> GetProducts();
    public IEnumerable<ProductItem> GetCatalog();
    public Product GetProductItemsForManager(int id);
    public Product GetProductItemsForCustomer(int id);
    public void AddProduct(Product p);
    public void RemoveProduct(int id);   
    public void UpdateProduct(Product p);  
}
