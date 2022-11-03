﻿
namespace Dal.DO;

public class OrderItem
{
    //public int ID { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }


    public override string ToString() => $@"
    product id: {ProductId}, 
    order id: {OrderId},
    price:{Price},
    amount:{Amount}   ";
}
