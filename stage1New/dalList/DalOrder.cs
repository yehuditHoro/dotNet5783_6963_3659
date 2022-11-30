using DalApi;
using Dal.DO;

namespace dalList;
internal class DalOrder : Iorder
{ 
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
    public Order Read(int id)
    {
        for (int i = 0; i < DataSource.OrdersList.Count(); i++)
        {
            if (DataSource.OrdersList[i].ID == id)
            {
                return DataSource.OrdersList[i];
            }
        }
        throw new EntityDuplicateException("this id doesn't exist");
    }
    public IEnumerable<Order> ReadAll()
    {
        List<Order> allOrders = new List<Order>();
     
        for (int i = 0; i < DataSource.OrdersList.Count(); i++)
        {
            allOrders.Add(DataSource.OrdersList[i]);
        }
        return allOrders;
    }
    
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
        throw new EntityDuplicateException("this order doesn't exist");
    }

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
        throw new EntityDuplicateException("this order doesn't exist");
    }

}
