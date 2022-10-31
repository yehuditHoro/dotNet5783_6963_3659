
namespace Dal.DO;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Enums.eCategory Category { get; set;}
    public int InStock { get; set; }

    public override string ToString() => $@"
    Product ID: {ID}, 
    Name: {Name},
    Price: {Price},
    Category: {Category},
    Amount in stock: {InStock}";
}
