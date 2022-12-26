using DalApi;
using Dal.DO;

namespace dalList;
internal class DalOrder : Iorder
{
    /// <summary>
    /// add a new order to the order list
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns></returns>
    /// <exception cref="EntityDuplicateException"></exception>
    public int Add(Order newOrder)
    {
        newOrder.ID = DataSource.config.OrderId;
        DataSource.OrdersList.Add(newOrder);
        return newOrder.ID;
    }
    /// <summary>
    /// get the specific order with the specific id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public Order Read(int id)
    {
        return DataSource.OrdersList.Find(O => O.ID == id);
        throw new EntityNotFoundException("this id doesn't exist");
    }
    /// <summary>
    /// returns all the orders in the order list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> ReadAll(Func<Order, bool>? func = null)
    {
        try
        {
            List<Order> allOrders = new(DataSource.OrdersList);
            return func == null ? allOrders : allOrders.Where(func);
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
    public Order ReadSingle(Func<Order, bool> func)
    {
        return DataSource.OrdersList.Where(func).ToList()[0];
        throw new EntityNotFoundException("order not found");
    }
    /// <summary>
    /// update the order in the order list
    /// </summary>
    /// <param name="newOrder"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void Update(Order newOrder)
    {
        int idx = DataSource.OrdersList.FindIndex(O => O.ID == newOrder.ID);
        if (idx == -1)
            throw new EntityNotFoundException("this order doesn't exist");
        DataSource.OrdersList[idx] = newOrder;
        return;
    }
    /// <summary>
    /// gets id and delete the specific item of the order list
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void Delete(int id)
    {
        Order? o = DataSource.OrdersList.Find(O => O.ID == id);
        if (o == null)
            throw new EntityNotFoundException("This order does not exist");
        DataSource.OrdersList.Remove((Order)o);
        return;
    }

}
