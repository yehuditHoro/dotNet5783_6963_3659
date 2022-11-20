
namespace BO;

internal class ProductItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public BO.Enums.eCategory Category { get; set; }
    public int Amount { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $@"
    order item id: {ID},
    name: {Name},
    price: {Price},
    category: {Category},
    amount: {Amount},
    in stock: {InStock}";
}
