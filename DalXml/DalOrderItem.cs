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

internal class DalOrderItem : IorderItem
{
    public int Add(OrderItem oi)
    {
        //config???????
        XElement? config = XDocument.Load("..\\xml\\Config.xml").Root;
        oi.ID = Convert.ToInt32(config?.Elements("orderItemId")?.FirstOrDefault()?.Value);
        config?.Element("orderItemId")?.SetValue(Convert.ToInt32(config?.Elements("orderItemId")?.FirstOrDefault()?.Value) + 1);
        config?.Save("..\\xml\\Config.xml");
        List<OrderItem> lst = new();
        if (oi.ID != 1000)
        {
            StreamReader sr = new("..\\xml\\OrderItem.xml");
            XmlSerializer ser = new(typeof(List<OrderItem>));
            lst = (List<OrderItem>)ser.Deserialize(sr);
            sr.Close();
        }
        lst?.Insert(0, oi);
        StreamWriter sw = new("..\\xml\\OrderItem.xml");
        XmlSerializer s = new(typeof(List<OrderItem>));
        s.Serialize(sw, lst);
        sw.Close();
        return oi.ID;
    }

    public OrderItem Read(int id)
    {
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);
        rw.Close();
        return lst.Where(x => x.ID == id).FirstOrDefault();
    }

    public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func = null)
    {
        //IEnumerable when there us func
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        if (func == null)
            return lst;
        return lst.Where(func);
    }

    public OrderItem ReadSingle(Func<OrderItem, bool> func)
    {
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);
        rw.Close();
        return lst.Where(func).FirstOrDefault();// need to check if func ==null?
    }

    public void Update(OrderItem oi)
    {
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = new();
        lst = (List<OrderItem>)ser.Deserialize(rw);//?? throw new Exception();
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == oi.ID);
        lst.RemoveAt(idx);
        lst.Insert(0, oi);
        StreamWriter sw = new("..\\xml\\OrderItem.xml");
        XmlSerializer s = new(typeof(List<OrderItem>));
        s.Serialize(sw, lst);
        sw.Close();
    }

    public void Delete(int id)
    {
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = new();
        lst = (List<OrderItem>)ser.Deserialize(rw); //?? throw new Exception();
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == id);
        lst.RemoveAt(idx);
        StreamWriter sw = new("..\\xml\\OrderItem.xml");
        XmlSerializer s = new(typeof(List<OrderItem>));
        s.Serialize(sw, lst);
        sw.Close();
    }
}

