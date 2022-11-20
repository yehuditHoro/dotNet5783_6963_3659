
namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public BO.OrderItem Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
       Customer name: {CustomerName}, 
       Customer email: {CustomerEmail},
       Customer address: {CustomerAddress},
       Items: {Items},
       Total price: {TotalPrice}";
}
