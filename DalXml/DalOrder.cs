using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Dal.DO;
using DalApi;
namespace Dal;

internal class DalOrder : Iorder
{
    public int Add(Order order)
    {
        //config???????
        XElement? config = XDocument.Load("..\\xml\\Config.xml").Root;
        order.ID = Convert.ToInt32(config?.Elements("orderId")?.FirstOrDefault()?.Value);
        config?.Element("orderId")?.SetValue(Convert.ToInt32(config?.Elements("orderId")?.FirstOrDefault()?.Value) + 1);
        config?.Save("..\\xml\\Config.xml");
        List<Order> lst = new();
        if (order.ID != 100) {
            StreamReader sr = new("..\\xml\\Order.xml");
            XmlSerializer ser = new(typeof(List<Order>));
            lst = (List<Order>)ser.Deserialize(sr);
            sr.Close();
        }
        lst?.Insert(0, order);
        StreamWriter sw = new("..\\xml\\Order.xml");
        XmlSerializer s = new(typeof(List<Order>));
        s.Serialize(sw, lst);
        sw.Close();
        return order.ID;
    }

    public Order Read(int id)
    {
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);
        rw.Close();
        return lst.Where(x => x.ID == id).FirstOrDefault();
    }

    public IEnumerable<Order> ReadAll(Func<Order, bool>? func = null)
    {
        //IEnumerable when there us func
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        if (func == null)
            return lst;
        return lst.Where(func);
    }

    public Order ReadSingle(Func<Order, bool> func)
    {
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);
        rw.Close();
        return lst.Where(func).FirstOrDefault();// need to check if func ==null?
    }

    public void Update(Order order)
    {
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = new();
        lst = (List<Order>)ser.Deserialize(rw);//?? throw new Exception();
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == order.ID);
        lst.RemoveAt(idx);
        lst.Insert(0, order);
        StreamWriter sw = new("..\\xml\\Order.xml");
        XmlSerializer s = new(typeof(List<Order>));
        s.Serialize(sw, lst);
        sw.Close();
    }

    public void Delete(int id)
    {
        StreamReader rw = new("..\\xml\\Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = new();
        lst = (List<Order>)ser.Deserialize(rw); //?? throw new Exception();
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == id);
        lst.RemoveAt(idx);
        StreamWriter sw = new("..\\xml\\Order.xml");
        XmlSerializer s = new(typeof(List<Order>));
        s.Serialize(sw, lst);
        sw.Close();
    }
}

