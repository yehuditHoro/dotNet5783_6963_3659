
using Dal.DO;
namespace Dal;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DalApi;
public class DalOrderItem : IorderItem
{
    /// <summary>
    /// add a new order item to the order item list
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="EntityDuplicateException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.ID = DataSource.config.OrderItemId;
        DataSource.OrderItemsList.Add(newOrderItem);
        return newOrderItem.ID;
    }


    /// <summary>
    ///  returns all the order items in the order items list
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func = null)
    {
        try
        { 
            List<OrderItem> allOrderItems = new (DataSource.OrderItemsList);
            return func == null ? allOrderItems : allOrderItems.Where(func);
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new EntityNotFoundException("entity not found");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem ReadSingle(Func<OrderItem, bool> func)
    {
        return DataSource.OrderItemsList.Where(func).ToList()[0];
        throw new EntityNotFoundException("order item not found");
    }

    /// <summary>
    /// update the order item in the order item list
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem newOrderItem)
    {
        int idx = DataSource.OrderItemsList.FindIndex(O => O.ID == newOrderItem.ID);
        if (idx == -1)
            throw new EntityNotFoundException("this order item doesn't exist");
        DataSource.OrderItemsList[idx] = newOrderItem;
        return;
    }

    /// <summary>
    /// gets id and delete the specific item of the order item list
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="EntityNotFoundException"></exception>   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        OrderItem? oi = DataSource.OrderItemsList.Find(O => O.ID == id);
        if (oi == null)
            throw new EntityNotFoundException("This order item does not exist");
        DataSource.OrderItemsList.Remove((OrderItem)oi);
        return;
    }
}
