﻿using System;
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

internal class DalOrderItem : IorderItem
{
    /// <summary>
    /// the function add a order item to the list in OrderItem.xml file
    /// </summary>
    /// <param name="oi"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem oi)
    {
        XElement? config = XDocument.Load("..\\xml\\Config.xml").Root;
        oi.ID = Convert.ToInt32(config?.Elements("orderItemId")?.FirstOrDefault()?.Value);
        config?.Element("orderItemId")?.SetValue(Convert.ToInt32(config?.Elements("orderItemId")?.FirstOrDefault()?.Value) + 1);
        config?.Save("..\\xml\\Config.xml");
        List<OrderItem> lst = new();
        if (oi.ID != 1000)
        {
            StreamReader sr = new("..\\xml\\OrderItem.xml");
            XmlSerializer ser = new(typeof(List<OrderItem>));
            lst = (List<OrderItem>)ser.Deserialize(sr) ?? throw new Exception();
            sr.Close();
        }
        lst?.Insert(0, oi);
        StreamWriter sw = new("..\\xml\\OrderItem.xml");
        XmlSerializer s = new(typeof(List<OrderItem>));
        s.Serialize(sw, lst);
        sw.Close();
        return oi.ID;
    }

    /// <summary>
    /// if the function doesn't get any argument the function returns all the order items,
    /// otherwise the function returns order item according to the lambada
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func = null)
    {
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem>? lst = (List<OrderItem>)ser.Deserialize(rw) ?? throw new Exception();
        rw.Close();
        if (func == null)
            return lst;
        return lst.Where(func);
    }

    /// <summary>
    /// The function returns the first order item from the OrderItem.xml file
    /// that meets the condition lambada 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem ReadSingle(Func<OrderItem, bool> func)
    {
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem>? lst = (List<OrderItem>)ser.Deserialize(rw) ?? throw new Exception(); ;
        rw.Close();
        return lst.Where(func).FirstOrDefault();
    }

    /// <summary>
    /// the function gets the order with changes and update this order item
    /// according to the order item id in the OrderItem.xml file
    /// </summary>
    /// <param name="oi"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem oi)
    {
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem>? lst = (List<OrderItem>)ser.Deserialize(rw) ?? throw new Exception();
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == oi.ID);
        lst.RemoveAt(idx);
        lst.Insert(0, oi);
        StreamWriter sw = new("..\\xml\\OrderItem.xml");
        XmlSerializer s = new(typeof(List<OrderItem>));
        s.Serialize(sw, lst);
        sw.Close();
    }

    /// <summary>
    /// the function gets the id of the order item that we want to remove and according to
    /// the order item id the function delete this order item in the OrderItem.xml file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        StreamReader rw = new("..\\xml\\OrderItem.xml");
        XmlSerializer ser = new(typeof(List<OrderItem>));
        List<OrderItem> lst = new();
        lst = (List<OrderItem>)ser.Deserialize(rw) ?? throw new Exception();
        rw.Close();
        int idx = lst.FindIndex(x => x.ID == id);
        lst.RemoveAt(idx);
        StreamWriter sw = new("..\\xml\\OrderItem.xml");
        XmlSerializer s = new(typeof(List<OrderItem>));
        s.Serialize(sw, lst);
        sw.Close();
    }
}

