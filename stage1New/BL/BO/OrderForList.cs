
namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public BO.Enums.eOrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
       ID: {ID}, 
       Customer name: {CustomerName},
       Status: {Status},
       Amount Of Items {AmountOfItems},
       Total price: {TotalPrice}";

}


