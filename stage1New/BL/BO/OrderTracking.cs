
namespace BO;

internal class OrderTracking
{
    public int ID { get; set; }
    public BO.Enums.OrderStatus Status { get; set; }

    public override string ToString() => $@"
    order item id: {ID},
    status: {Status}";
}

