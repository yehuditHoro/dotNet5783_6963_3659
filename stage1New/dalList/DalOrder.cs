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
        for (int i = 0; i < DataSource.OrdersList.Count(); i++)
        {
            if (DataSource.OrdersList[i].ID == newOrder.ID)
            {
                throw new EntityDuplicateException("this order already exist");
            }
        }
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
        for (int i = 0; i < DataSource.OrdersList.Count(); i++)
        {
            if (DataSource.OrdersList[i].ID == id)
            {
                return DataSource.OrdersList[i];
            }
        }
        throw new EntityNotFoundException("this id doesn't exist");
    }
    /// <summary>
    /// returns all the orders in the order list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> ReadAll()
    {
        List<Order> allOrders = new List<Order>();
     
        for (int i = 0; i < DataSource.OrdersList.Count(); i++)
        {
            allOrders.Add(DataSource.OrdersList[i]);
        }
        return allOrders;
    }
    /// <summary>
    /// update the order in the order list
    /// </summary>
    /// <param name="newOrder"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void Update(Order newOrder)
    {
        int id = newOrder.ID;
        for (int i = 0; i < DataSource.OrdersList.Count(); i++)
        {
            if (DataSource.OrdersList[i].ID == id)
            {
                DataSource.OrdersList[i] = newOrder;
                return;
            }
        }
        throw new EntityNotFoundException("this order doesn't exist");
    }
    /// <summary>
    /// gets id and delete the specific item of the order list
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.OrdersList.Count(); i++)
        {
            if (DataSource.OrdersList[i].ID == id)
            {
                DataSource.OrdersList.Remove(DataSource.OrdersList[i]);
                return;
            }
        }
        throw new EntityNotFoundException("this order doesn't exist");
    }

}
