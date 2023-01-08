using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dal.DO;
using DalApi;

namespace Dal;

internal class DalProduct : Iproduct
{
    private const string url = "..\\xml\\Product.xml";

    private Product Casting(XElement element)
    {
        Product p = new();
        p.ID = Convert.ToInt32(element?.Attribute("ID")?.Value);
        p.Name = element?.Attribute("Name")?.Value;
        p.Category = Enum.Parse<eCategory>(element?.Attribute("Category")?.Value);
        p.Price = Convert.ToInt32(element?.Attribute("Price")?.Value);
        p.InStock = Convert.ToInt32(element?.Attribute("InStock")?.Value);
        return p;
    }

    public int Add(Product p)
    {
        XElement? root = XDocument.Load(url).Root;
        XElement? config = XDocument.Load("..\\xml\\Config.xml").Root;
        XElement element = new XElement("product",
            new XAttribute("ID", Convert.ToInt32(config?.Elements("productId")?.FirstOrDefault()?.Value)),
            new XAttribute("Name", p.Name),
            new XAttribute("Category", p.Category),
            new XAttribute("Price", p.Price),
            new XAttribute("InStock", p.InStock));
        root?.Add(element);
        root?.Save(url);
        config?.Element("productId")?.SetValue(Convert.ToInt32(config?.Elements("productId")?.FirstOrDefault()?.Value)+1);
        config?.Save("..\\xml\\Config.xml");
        return p.ID;
    }

    public void Delete(int id)
    {
        IEnumerable<OrderItem> orderItems = DalXml.Instance.orderItem.ReadAll();
        foreach (OrderItem item in orderItems) //is it possible to use foreach?
        {
            if (item.ProductId == id)
                throw new Exception("the product couldn't be deleted because the product is already ordered");
        }
        XElement? root = XDocument.Load(url).Root;
        root?.Elements("product")?.Where(p => Convert.ToInt32(p?.Attribute("ID")?.Value) == id).Remove();
        root?.Save(url);
    }

    public Product Read(int id)
    {
        XElement? root = XDocument.Load(url).Root;
        IEnumerable<XElement> xElements = root?.Elements("product") ?? throw new Exception();
        List<Dal.DO.Product> ProductsList = new();
        foreach (XElement element in xElements)
        {
            ProductsList.Add(Casting(element));
        }
        return ProductsList.Where(p => p.ID == id).FirstOrDefault();
    }

    public IEnumerable<Product> ReadAll(Func<Product, bool>? func = null)
    {
        XElement? root = XDocument.Load(url).Root;
        IEnumerable<XElement> xElements = root?.Elements("product") ?? throw new Exception();
        List<Dal.DO.Product> ProductsList = new();
        foreach (XElement element in xElements)
        {
            ProductsList.Add(Casting(element));
        }
        return func == null ? ProductsList : ProductsList.Where(func);
    }

    public Product ReadSingle(Func<Product, bool> func)
    {
        XElement? root = XDocument.Load(url).Root;
        IEnumerable<XElement> xElements = root?.Elements("product") ?? throw new Exception();
        List<Dal.DO.Product> ProductsList = new();
        foreach (XElement element in xElements)
        {
            ProductsList.Add(Casting(element));
        }
        return ProductsList.Where(func).First();
    }

    public void Update(Product p)
    {
        XElement? root = XDocument.Load(url).Root;
        XElement? e = root?.Elements("product")?.
                    Where(e => Convert.ToInt32(e.Attribute("ID")?.Value) == p.ID).FirstOrDefault();
        e?.Attribute("Name")?.SetValue(p.Name);
        e?.Attribute("Category")?.SetValue(p.Category);
        e?.Attribute("Price")?.SetValue(p.Price);
        e?.Attribute("InStock")?.SetValue(p.InStock);
        root?.Save(url);
    }

    public void UpdateAmount(int id, int amount)
    {
        XElement? root = XDocument.Load(url).Root;
        XElement? e = root?.Elements("product")?.
                    Where(e => Convert.ToInt32(e.Attribute("ID")?.Value) == id).First(); 
        //firstordefault???
        e?.Attribute("InStock")?.SetValue(Convert.ToInt32(e.Attribute("InStock")?.Value) - amount);
        root?.Save(url);
    }
}