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
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        XElement element = new ("product",
            new XElement("ID", Convert.ToInt32(root?.Descendants("config").Elements("productId")?.FirstOrDefault()?.Value + 1)),
            new XElement("Name", p.Name),
            new XElement("Category", p.Category),
            new XElement("Price", p.Price),
            new XElement("InStock", p.InStock));
        root?.Element("products")?.Add(element);
        root?.Save("..\\xml\\Product.xml");
        return p.ID;
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        root?.Elements("products").
            Where(p => Convert.ToInt32(p?.Attribute("ID")?.Value) == id).Remove();
        root?.Save(url);
    }

    public Product Read(int id)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        IEnumerable<XElement> xElements = root?.Descendants("products")?.Elements("product") ?? throw new Exception();
        List<Dal.DO.Product> ProductsList = new();
        foreach (XElement element in xElements)
        {
            ProductsList.Add(Casting(element));
        }
        return ProductsList.Where(p => p.ID == id).FirstOrDefault();
    }

    public IEnumerable<Product> ReadAll(Func<Product, bool>? func = null)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        IEnumerable<XElement> xElements = root?.Descendants("products")?.Elements("product") ?? throw new Exception();
        List<Dal.DO.Product> ProductsList = new();
        foreach (XElement element in xElements)
        {
            ProductsList.Add(Casting(element));
        }
        return func == null ? ProductsList : ProductsList.Where(func);
    }

    public Product ReadSingle(Func<Product, bool> func)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        IEnumerable<XElement> xElements = root?.Descendants("products")?.Elements("product") ?? throw new Exception();
        List<Dal.DO.Product> ProductsList = new();
        foreach (XElement element in xElements)
        {
            ProductsList.Add(Casting(element));
        }
        return ProductsList.Where(func).FirstOrDefault();
    }

    public void Update(Product p)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        XElement? e = root?.Elements("products")?.
                    Where(e => Convert.ToInt32(e.Attribute("ID")?.Value) == p.ID).FirstOrDefault();
        e?.Attribute("Name")?.SetValue(p.Name);
        e?.Attribute("Category")?.SetValue(p.Category);
        e?.Attribute("Price")?.SetValue(p.Price);
        e?.Attribute("InStock")?.SetValue(p.InStock);
        root?.Save(url);
    }

    public void UpdateAmount(int id, int amount)
    {

    }

}