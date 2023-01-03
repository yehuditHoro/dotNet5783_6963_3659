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

    //private Order Casting(Object element)
    //{
    //    Product p = new();
    //    p.ID = Convert.ToInt32(element?.Attribute("ID")?.Value);
    //    p.Name = element?.Attribute("Name")?.Value;
    //    p.Category = Enum.Parse<eCategory>(element?.Attribute("Category")?.Value);
    //    p.Price = Convert.ToInt32(element?.Attribute("Price")?.Value);
    //    p.InStock = Convert.ToInt32(element?.Attribute("InStock")?.Value);
    //    return p;
    //}

    public int Add(Order item)
    {
        // לבדוק אם צריך לקרוא את כל הרשימה מהדף ואז להוסיף פה לרשימה ולכתוב את הרשימה מחדש
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        lst.Add(item);
        StreamWriter sw = new("Order.xml");
        XmlSerializer ser1 = new(typeof(Order));
        ser1.Serialize(sw, item);
        sw.Close();
        return item.ID;
    }
    public Order Read(int id)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        return lst.Where(x => x.ID == id).FirstOrDefault();
    }

    public IEnumerable<Order> ReadAll(Func<Order, bool>? func = null)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        return func == null ? lst : lst.Where(func);

    }

    public Order ReadSingle(Func<Order, bool> func)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        return lst.Where(func).FirstOrDefault();// need to check if func ==null?
    }

    public void Update(Order item)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == item.ID);
        lst.RemoveAt(idx);
        lst.Add(item);  
        StreamWriter sw = new("Order.xml");
        XmlSerializer ser1 = new(typeof(List<Order>));
        ser1.Serialize(sw, lst);
        sw.Close();
    }

    public void Delete(int id)
    {
        StreamReader rw = new("Order.xml");
        XmlSerializer ser = new(typeof(List<Order>));
        List<Order> lst = (List<Order>)ser.Deserialize(rw);// what is Deserialize do?
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == id);
        lst.RemoveAt(idx);
        StreamWriter sw = new("Order.xml");
        XmlSerializer ser1 = new(typeof(List<Order>));
        ser1.Serialize(sw, lst);
        sw.Close();
    }
}

