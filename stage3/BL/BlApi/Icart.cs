using BO;
namespace BlApi;

public interface Icart
{
    public Cart addToCart(Cart c, int id);
    public Cart UpdateQuantity(Cart c, int id, int quantity);
    public void MakeAnOrder(Cart c,string name,string email,string address);
}
