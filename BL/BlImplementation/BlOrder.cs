using BlApi;
using DalApi;
using Dal;
using System.Net.NetworkInformation;

namespace BlImplementation;

internal class BlOrder : BlApi.Iorder
{
    private IDal? dal;
    List<Dal.DO.OrderItem> allItems;
    public BlOrder()
    {
        dal = DalApi.Factory.Get();
        allItems = dal.orderItem.ReadAll().ToList();
    }

    /// <summary>
    /// the function return all the orders from the datasource
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BlFailedToGet"></exception>
    /// <exception cref="BlIdNotFound"></exception>
    public IEnumerable<BO.OrderForList> GetOrdersList()
    {
        try
        {
            IEnumerable<Dal.DO.Order>? getOrders = dal?.order.ReadAll();
            if (getOrders?.Count() <= 0)
            {
                throw new BlFailedToGet();
            }
            List<BO.OrderForList> boOrders = new();
            double sum = 0;
            int itemsAmount = 0;
            getOrders?.Select(o =>
            {
                BO.OrderForList boOrder = new();
                boOrder.ID = o.ID;
                boOrder.CustomerName = o.CustomerName;
                boOrder.Status = CheckStatus(o);
                allItems.Where(oi => oi.OrderId == o.ID).Select(oi =>
                {
                    sum = sum + (double)(oi.Amount * oi.Price);
                    itemsAmount = itemsAmount + 1;
                    return oi;
                }).ToList();
                boOrder.TotalPrice = sum;
                boOrder.AmountOfItems = itemsAmount;
                boOrders.Add(boOrder);
                return boOrder;
            }).ToList();
            return boOrders;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
        catch (Exception e)
        {
            throw new Exception( e.Message);
        }
    }

    /// <summary>
    /// the function gets id and return its 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BlInvalidInputException"></exception>
    /// <exception cref="BlIdNotFound"></exception>
    public BO.Order GetOrder(int id)
    {
        try
        {
            if (id > 0)
            {
                Dal.DO.Order currOrder = dal.order.ReadSingle(x => x.ID == id);
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
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// the function gets id of an order and change the shiping date
    /// </summary>
    /// <param name="id"></param>
    /// returns></returns>
    /// <exception cref="BlFailedToUpdate"></exception>
    /// <exception cref="BlIdNotFound"></exception>
    public BO.Order ShipedOrder(int id)
    {
        try
        {
            Dal.DO.Order currOrder = dal.order.ReadSingle(x => x.ID == id);
            if (currOrder.ShipDate < DateTime.Now && currOrder.ShipDate!=DateTime.MinValue)
                throw new BlFailedToUpdate();
            currOrder.ShipDate = DateTime.Now;
            dal.order.Update(currOrder);
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
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// the function gets id of an order and change the delivery date
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BlFailedToUpdate"></exception>
    /// <exception cref="BlIdNotFound"></exception>
    public BO.Order DeliveredOrder(int id)
    {
        try
        {
            Dal.DO.Order currOrder = dal.order.ReadSingle(x => x.ID == id);
            if ((currOrder.DeliveryDate > DateTime.Now && currOrder.ShipDate < DateTime.Now) || currOrder.DeliveryDate==DateTime.MinValue)
            {
                currOrder.DeliveryDate = DateTime.Now;
                dal.order.Update(currOrder);
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
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BlIdNotFound"></exception>
    public BO.OrderTracking OrderTrack(int id)
    {
        try
        {
            Dal.DO.Order currOrder = dal.order.ReadSingle(x=>x.ID==id);
            BO.OrderTracking TOrder = new BO.OrderTracking();
            TOrder.ID = currOrder.ID;
            TOrder.packageStatus?.Add(new Tuple<DateTime, BO.Enums.eOrderStatus>(currOrder.OrderDate, (BO.Enums.eOrderStatus)0));
               // TOrder.Status = (BO.Enums.eOrderStatus)0;
            if (currOrder.ShipDate < DateTime.Now && currOrder.ShipDate != DateTime.MinValue)
            {
                TOrder.packageStatus?.Add(new Tuple<DateTime, BO.Enums.eOrderStatus>(currOrder.ShipDate, (BO.Enums.eOrderStatus)1));
                //TOrder.Status = (BO.Enums.eOrderStatus)1;
            }
            if (currOrder.DeliveryDate < DateTime.Now && currOrder.DeliveryDate != DateTime.MinValue)
            {
                TOrder.packageStatus?.Add(new Tuple<DateTime, BO.Enums.eOrderStatus>(currOrder.DeliveryDate, (BO.Enums.eOrderStatus)2));
                //TOrder.Status = (BO.Enums.eOrderStatus)2;
            }
            TOrder.Status = CheckStatus(currOrder);
            return TOrder;
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new BlIdNotFound();
        }
    }

    /// <summary>
    /// the function convert item of type Dal.do to item of type BO
    /// </summary>
    /// <param name="DItems"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    private (List<BO.OrderItem>, double) convertDToB(List<Dal.DO.OrderItem> DItems, int orderId)
    {
        List<BO.OrderItem> items = new List<BO.OrderItem>();
        double OrderTotalPrice = 0;
        DItems.Where(item => item.OrderId == orderId).Select(item =>
        {
            BO.OrderItem oi = new();
            oi.ID = item.ID;
            oi.Name = dal.product.ReadSingle(x => x.ID == item.ProductId).Name;
            oi.ProductID = item.ProductId;
            oi.Amount = item.Amount;
            oi.Price = item.Price;
            oi.TotalPrice = oi.Amount * oi.Price;
            items.Add(oi);
            OrderTotalPrice += oi.TotalPrice;
            return oi;
        }).ToList();
        return (items, OrderTotalPrice);
    }

    /// <summary>
    /// globak function that checks the status of the order
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    private BO.Enums.eOrderStatus CheckStatus(Dal.DO.Order o)
    {
        BO.Enums.eOrderStatus orderStatus = o.ShipDate > DateTime.Now ? BO.Enums.eOrderStatus.confirmed
                : (o.DeliveryDate > DateTime.Now ? BO.Enums.eOrderStatus.shiped
                : BO.Enums.eOrderStatus.delivered);
        return orderStatus;
    }
}

