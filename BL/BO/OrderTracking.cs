
namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public BO.Enums.eOrderStatus Status { get; set; }
    public List <(DateTime, Enums.eOrderStatus)> packageStatus{ get; set; }  

    public override string ToString() => $@"
    order item id: {ID},
    status: {Status},
    package status: {packageStatus}";
}

