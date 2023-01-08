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

    private IEnumerable<DO.Product> Casting(IEnumerable<XElement>? allProduct)
    {
        return from product in allProduct
               select new DO.Product
               {
                   ID = Convert.ToInt32(product.Attribute("ID").Value),
                   Name = product.Attribute("Name").Value,
                   Category = Enum.Parse<eCategory>(product.Attribute("Category").Value),
                   Price = Convert.ToInt32(product.Attribute("Price").Value),
                   InStock = Convert.ToInt32(product.Attribute("InStock").Value)
               };
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
        config?.Element("productId")?.SetValue(Convert.ToInt32(config?.Elements("productId")?.FirstOrDefault()?.Value) + 1);
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

    public IEnumerable<Product> ReadAll(Func<Product, bool>? func = null)
    {
        XElement? root = XDocument.Load(url).Root;
        IEnumerable<XElement> ProductsList = root?.Elements("product") ?? throw new Exception();
        List<DO.Product> allProduct = Casting(ProductsList).ToList();
        return func == null ? allProduct : allProduct.Where(func);
    }

    public Product ReadSingle(Func<Product, bool> func)
    {
        XElement? root = XDocument.Load(url).Root;
        IEnumerable<XElement> ProductsList = root?.Elements("product") ?? throw new Exception();
        List<DO.Product> allProduct = Casting(ProductsList).ToList();
        return allProduct.Where(func).FirstOrDefault();
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
                    Where(e => Convert.ToInt32(e.Attribute("ID")?.Value) == id).FirstOrDefault();
        e?.Attribute("InStock")?.SetValue(Convert.ToInt32(e.Attribute("InStock")?.Value) - amount);
        root?.Save(url);
    }
}