using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Dal.DO;
using DalApi;
namespace Dal;

internal class DalOrderItem : IorderItem
{
    public int Add(OrderItem item)
    {
        // לבדוק אם צריך לקרוא את כל הרשימה מהדף ואז להוסיף פה לרשימה ולכתוב את הרשימה מחדש
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        lst.Add(item);
        StreamWriter sw = new("Order.xml");
        XmlSerializer ser1 = new(typeof(OrderItem));
        ser1.Serialize(sw, item);
        sw.Close();
        return item.ID;
    }
    public OrderItem Read(int id)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        return lst.Where(x => x.ID == id).FirstOrDefault();
    }

    public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func = null)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        return func == null ? lst : lst.Where(func);

    }

    public OrderItem ReadSingle(Func<OrderItem, bool> func)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        return lst.Where(func).FirstOrDefault();// need to check if func ==null?
    }

    public void Update(OrderItem item)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == item.ID);
        lst.RemoveAt(idx);
        lst.Add(item);
        StreamWriter sw = new("Order.xml");
        XmlSerializer ser1 = new(typeof(List<OrderItem>));
        ser1.Serialize(sw, lst);
        sw.Close();
    }

    public void Delete(int id)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = (List<OrderItem>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == id);
        lst.RemoveAt(idx);
        StreamWriter sw = new("Order.xml");
        XmlSerializer ser1 = new(typeof(List<OrderItem>));
        ser1.Serialize(sw, lst);
        sw.Close();
    }
}

