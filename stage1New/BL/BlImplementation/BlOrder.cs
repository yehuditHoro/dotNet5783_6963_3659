using BlApi;
using DalApi;
using dalList;
namespace BlImplementation;

internal class BlOrder : BlApi.Iorder
{
    IDal Dal = new DalList();
    List<Dal.DO.OrderItem> allItems = dalList.DataSource.OrderItemsList;

    public IEnumerable<BO.OrderForList> GetOrdersList()
    {
        try
        {
            IEnumerable<Dal.DO.Order> getOrders = Dal.order.ReadAll();
            if (getOrders.Count() <= 0)
            {
                throw new BlFailedToGet();
            }
            List<BO.OrderForList> boOrders = new();
            foreach (Dal.DO.Order o in getOrders)
            {
                BO.OrderForList order = new();
                order.ID = o.ID;
                order.CustomerName = o.CustomerName;
                order.Status = CheckStatus(o);
                double sum = 0;
                int itemsAmount = 0;
                foreach (Dal.DO.OrderItem item in allItems)
                {
                    if (item.OrderId == o.ID)
                    {
                        sum += (double)(item.Amount * item.Price);
                        itemsAmount++;
                    }
                }
                order.TotalPrice = sum;
                order.AmountOfItems = itemsAmount;
                boOrders.Add(order);
            }
            return boOrders;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    public BO.Order GetOrder(int id)
    {
        try
        {
            if (id > 0)
            {
                Dal.DO.Order currOrder = Dal.order.Read(id);
                BO.Order newO = new();
                newO.ID = currOrder.ID;
                newO.CustomerName = currOrder.CustomerName;
                newO.CustomerAddress = currOrder.CustomerAddress;
                newO.DeliveryDate = currOrder.DeliveryDate;
                newO.OrderDate = currOrder.OrderDate;
                newO.ShipDate = currOrder.ShipDate;
                newO.CustomerEmail = currOrder.CustomerEmail;
                newO.Status = CheckStatus(currOrder);
                (newO.Items, newO.TotalPrice) = convertDToB(allItems, id);
                return newO;
            }
            throw new BlInvalidInputException("invalid negative input");
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    public BO.Order ShipedOrder(int id)
    {
        try
        {
            Dal.DO.Order currOrder = Dal.order.Read(id);
            if (currOrder.ShipDate < DateTime.Now)
                throw new BlFailedToUpdate();
            currOrder.ShipDate = DateTime.Now;
            Dal.order.Update(currOrder);
            BO.Order order = new BO.Order();
            order.ID = currOrder.ID;
            order.OrderDate = currOrder.OrderDate;
            order.ShipDate = DateTime.Now;
            order.DeliveryDate = currOrder.DeliveryDate;
            order.Status = BO.Enums.eOrderStatus.shiped;
            order.CustomerName = currOrder.CustomerName;
            order.CustomerAddress = currOrder.CustomerAddress;
            order.CustomerEmail = currOrder.CustomerEmail;
            (order.Items, order.TotalPrice) = convertDToB(allItems, currOrder.ID);
            return order;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    public BO.Order DeliveredOrder(int id)
    {
        try
        {
            Dal.DO.Order currOrder = Dal.order.Read(id);
            if (currOrder.DeliveryDate > DateTime.Now && currOrder.ShipDate < DateTime.Now)
            {
                currOrder.DeliveryDate = DateTime.Now;
                Dal.order.Update(currOrder);
                BO.Order order = new BO.Order();
                order.ID = currOrder.ID;
                order.OrderDate = currOrder.OrderDate;
                order.ShipDate = currOrder.ShipDate;
                order.DeliveryDate = DateTime.Now;
                order.Status = BO.Enums.eOrderStatus.delivered;
                order.CustomerName = currOrder.CustomerName;
                order.CustomerAddress = currOrder.CustomerAddress;
                order.CustomerEmail = currOrder.CustomerEmail;
                (order.Items, order.TotalPrice) = convertDToB(allItems, currOrder.ID);
                return order;
            }
            throw new BlFailedToUpdate();
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    //public BO.OrderTracking OrderTrack(int id)
    //{
    //    Dal.DO.Order currOrder = Dal.order.Read(id);
    //    BO.OrderTracking TOrder = new BO.OrderTracking();
    //    TOrder.ID = currOrder.ID;   
    //    TOrder.Status=
    //}

    private (List<BO.OrderItem>, double) convertDToB(List<Dal.DO.OrderItem> DItems, int orderId)
    {
        double OrderTotalPrice = 0;
        List<BO.OrderItem> items = new List<BO.OrderItem>();
        foreach (Dal.DO.OrderItem item in DItems)
        {
            if (item.OrderId == orderId)
            {
                BO.OrderItem oi = new();
                oi.ID = item.ID;
                oi.Name = Dal.product.Read(item.ProductId).Name;
                oi.ProductID = item.ProductId;
                oi.Amount = item.Amount;
                oi.Price = item.Price;
                oi.TotalPrice = oi.Amount * oi.Price;
                items.Add(oi);
                OrderTotalPrice += oi.TotalPrice;
            }
        }
        return (items, OrderTotalPrice);
    }

    private BO.Enums.eOrderStatus CheckStatus(Dal.DO.Order o)
    {
        BO.Enums.eOrderStatus orderStatus = o.ShipDate > DateTime.Now ? BO.Enums.eOrderStatus.confirmed
                : (o.DeliveryDate > DateTime.Now ? BO.Enums.eOrderStatus.shiped
                : BO.Enums.eOrderStatus.delivered);
        return orderStatus;
    }
}

