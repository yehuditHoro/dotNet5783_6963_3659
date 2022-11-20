
namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public BO.Enums.eCategory Category { get; set; }

    public override string ToString() => $@"
    order item id: {ID},
    name: {Name},
    price: {Price},
    category: {Category}";
}

