
using BO;
namespace BlApi;

public interface Iproduct
{
    public IEnumerable<ProductForList?> GetProducts(BO.eCategory? category = null);
    public IEnumerable<ProductItem?> GetCatalog(BO.eCategory? category = null);
    public Product GetProductItemsForManager(int id);
    public ProductItem GetProductItemsForCustomer(int id, BO.Cart c);
    public void AddProduct(Product p);
    public void RemoveProduct(int id);
    public void UpdateProduct(Product p);
}
