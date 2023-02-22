using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Dal.DO;
using DalApi;
namespace Dal;

internal class DalOrder : Iorder
{
    /// <summary>
    /// the function add a order to the list in Order.xml file
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order order)
    {
        XElement? config = XDocument.Load("..\\xml\\Config.xml").Root;
        order.ID = Convert.ToInt32(config?.Elements("orderId")?.FirstOrDefault()?.Value);
        config?.Element("orderId")?.SetValue(Convert.ToInt32(config?.Elements("orderId")?.FirstOrDefault()?.Value) + 1);
        config?.Save("..\\xml\\Config.xml");
        List<Order> lst = new();
        if (order.ID != 100)
        {
            StreamReader sr = new("..\\xml\\Order.xml");
            XmlSerializer ser = new(typeof(List<Order>));
            lst = (List<Order>)ser.Deserialize(sr) ?? throw new Exception();
            sr.Close();
        }
        lst?.Insert(0, order);
        StreamWriter sw = new("..\\xml\\Order.xml");
        XmlSerializer s = new(typeof(List<Order>));
        s.Serialize(sw, lst);
        sw.Close();
        return order.ID;
    }

    /// <summary>
    /// if the function doesn't get any argument the function returns all the orders,
    /// otherwise the function returns order according to the lambada
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> ReadAll(Func<Order, bool>? func = null)
    {
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order>? lst = (List<Order>)ser.Deserialize(rw) ?? throw new Exception();
        rw.Close();
        if (func == null)
            return lst;
        return lst.Where(func);
    }

    /// <summary>
    /// The function returns the first order from the Order.xml file
    /// that meets the condition lambada 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order ReadSingle(Func<Order, bool> func)
    {
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order>? lst = (List<Order>)ser.Deserialize(rw) ?? throw new Exception();
        rw.Close();
        return lst.Where(func).FirstOrDefault();
    }

    /// <summary>
    /// the function gets the order with changes and update this order
    /// according to the order id in the Order.xml file
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order order)
    {
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order>? lst = (List<Order>)ser.Deserialize(rw) ?? throw new Exception();
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == order.ID);
        lst.RemoveAt(idx);
        lst.Insert(0, order);
        StreamWriter sw = new("..\\xml\\Order.xml");
        XmlSerializer s = new(typeof(List<Order>));
        s.Serialize(sw, lst);
        sw.Close();
    }

    /// <summary>
    /// the function gets the id of the order that we want to remove and according to
    /// the order id the function delete this order in the Order.xml file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order>? lst = (List<Order>)ser.Deserialize(rw) ?? throw new Exception();
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == id);
        lst.RemoveAt(idx);
        StreamWriter sw = new("..\\xml\\Order.xml");
        XmlSerializer s = new(typeof(List<Order>));
        s.Serialize(sw, lst);
        sw.Close();
    }
}

