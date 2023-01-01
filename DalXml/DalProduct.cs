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
    XElement? root = XDocument.Load("..\\..\\Product.xml").Root;
    private Product Casting(XElement element)
    {
        Product p = new();
        return p;
    }

    public int Add(Product p)
    {
        XElement element = new XElement("product",
            new XAttribute("ID", p.ID),
            new XAttribute("Name", p.Name),
            new XAttribute("Category", p.Category),
            new XAttribute("Price", p.Price),
            new XAttribute("InStock", p.InStock));
        root?.Element("products")?.Add(element);
        root?.Save("..\\..\\Product.xml");
        return p.ID;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Product Read(int id)
    {
        IEnumerable<XElement> xElements = root?.Descendants("products")?.Elements("student") ?? throw new Exception();
        List<Dal.DO.Product> ProductsList = new();
        foreach (XElement element in xElements)
        {
            ProductsList.Add(Casting(element));
        }
        return ProductsList.Where(p => p.ID == id).FirstOrDefault();  //?? throw new Exception();
    }

public IEnumerable<Product> ReadAll(Func<Product, bool>? func = null)
{
    throw new NotImplementedException();
}

public Product ReadSingle(Func<Product, bool> func)
{
    throw new NotImplementedException();
}

public void Update(Product item)
{
    throw new NotImplementedException();
}

public void UpdateAmount(int id, int amount)
{
    throw new NotImplementedException();
}

    
}

