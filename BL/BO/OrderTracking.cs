
namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public BO.eOrderStatus Status { get; set; }
    public List <Tuple<DateTime?, eOrderStatus>>? packageStatus{ get; set; } = new List<Tuple<DateTime?, eOrderStatus>>();

    public override string ToString() => $@"
    order item id: {ID},
    status: {Status},
    package status: {packageStatus}";
}

