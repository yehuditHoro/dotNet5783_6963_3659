
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

    public static Order Read(int id)
    {
        for (int i = 0; i < DataSource.config.indexOrder; i++)
        {
            if (DataSource.OrdersList[i].ID == id)
            {
                return DataSource.OrdersList[i];
            }
        }
        throw new Exception("this id doesn't exist");
    }
    public static Order[] ReadAll()
    {
        Order[] allOrders = new Order[DataSource.config.indexOrder];
        for (int i = 0; i < DataSource.OrdersList.Length; i++)
        {
            allOrders[i] = DataSource.OrdersList[i];
        }
        return allOrders;
    }

    public static void Update(Order newOrder)
    {
        int id = newOrder.ID;
        for (int i = 0; i < DataSource.config.indexOrder; i++)
        {
            if (DataSource.OrdersList[i].ID == id)
            {
                DataSource.OrdersList[i] = newOrder;
                return;
            }
        }
        throw new Exception("this Order doesn't exist");
    }

    public static void Delete(int id)
    {
        for (int i = 0; i < DataSource.config.indexOrder; i++)
        {
            if (DataSource.OrdersList[i].ID == id)
            {
                DataSource.OrdersList[i] = DataSource.OrdersList[DataSource.config.indexOrder];
                DataSource.config.indexOrder--;
                return;
            }
        }
        throw new Exception("this Order doesn't exist");
    }
}
