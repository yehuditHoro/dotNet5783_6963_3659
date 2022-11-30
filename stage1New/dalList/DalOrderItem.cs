
using Dal.DO;
namespace dalList;
using System.Collections.Generic;
using DalApi;
public class DalOrderItem : IorderItem
{
    public int Add(OrderItem newOrderItem)
    {
        
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            if (DataSource.OrderItemsList[i].ID == newOrderItem.ID)
            {
                throw new EntityDuplicateException("this order item already exist");
            }
        }
        DataSource.OrderItemsList.Add(newOrderItem);
        //DataSource.config.indexOrderItem++;
        return newOrderItem.ID;
    }

    public OrderItem Read(int id)
    {
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                return DataSource.OrderItemsList[i];
            }
        }
        throw new EntityDuplicateException("this id doesn't exist");
    }

    public IEnumerable<OrderItem> ReadAll()
    {
        List <OrderItem> allOrderItems = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            allOrderItems.Add(DataSource.OrderItemsList[i]);
        }
        return allOrderItems;
    }
    
    public void Update(OrderItem newOrderItem)
    {
        int id = newOrderItem.ID;
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                DataSource.OrderItemsList[i] = newOrderItem;
                return;
            }
        }
        throw new EntityDuplicateException("this order item doesn't exist");
    }

    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                DataSource.OrderItemsList.Remove(DataSource.OrderItemsList[i]);
                return;
            }
        }
        throw new EntityDuplicateException("this order item doesn't exist");
    }
}
