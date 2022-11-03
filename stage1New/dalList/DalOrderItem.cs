﻿
using Dal.DO;
namespace dalList;


public class DalOrderItem
{
    public static int Create(OrderItem newOrderItem)
    {
        for (int i = 0; i < DataSource.config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemsList[i].ID == newOrderItem.ID)
            {
                throw new Exception("this Order item already exist");
            }
        }
        DataSource.OrderItemsList[DataSource.config.indexOrderItem++] = newOrderItem;
        return newOrderItem.ID;
    }

    public static OrderItem Read(int id)
    {
        for (int i = 0; i < DataSource.config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                return DataSource.OrderItemsList[i];
            }
        }
        throw new Exception("this id doesn't exist");
    }

    public static OrderItem[] ReadAll()
    {
        Order[] allOrderItems = new Order[DataSource.config.indexOrderItem];
        for (int i = 0; i < DataSource.OrderItemsList.Length; i++)
        {
            allOrders[i] = DataSource.OrderItemsList[i];
        }
        return allOrderItems;
    }

    public static void Update(OrderItem newOrderItem)
    {
        int id = newOrderItem.ID;
        for (int i = 0; i < DataSource.config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                DataSource.OrderItemsList[i] = newOrderItem;
                return;
            }
        }
        throw new Exception("this Order item doesn't exist");
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                DataSource.OrderItemsList[i] = DataSource.OrderItemsList[DataSource.config.indexOrderItem];
                DataSource.config.indexOrderItem--;
                return;
            }
        }
        throw new Exception("this Order item doesn't exist");
    }
}
