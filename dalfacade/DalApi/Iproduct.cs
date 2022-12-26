
using Dal.DO;
namespace DalApi;

public interface Iproduct:Icrud<Product>
{
    public void UpdateAmount(int id, int amount);
}
