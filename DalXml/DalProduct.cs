using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dal.DO;
using DalApi;

namespace Dal;

internal class DalProduct : Iproduct
{
    /// <summary>
    /// the function gets list of xml elements and casting all of
    /// them to a new list of DO.Product elements
    /// </summary>
    /// <param name="allProduct"></param>
    /// <returns></returns>
    private IEnumerable<DO.Product> Casting(IEnumerable<XElement>? allProduct)
    {
        return from product in allProduct
               select new DO.Product
               {
                   ID = Convert.ToInt32(product?.Attribute("ID")?.Value),
                   Name = product?.Attribute("Name")?.Value,
                   Category = Enum.Parse<eCategory>(product?.Attribute("Category")?.Value),
                   Price = Convert.ToInt32(product?.Attribute("Price")?.Value),
                   InStock = Convert.ToInt32(product?.Attribute("InStock")?.Value)
               };
    }

    /// <summary>
    /// the function add a product to the list in Product.xml file
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product p)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        XElement? config = XDocument.Load("..\\xml\\Config.xml").Root;
        p.ID = Convert.ToInt32(config?.Elements("productId")?.FirstOrDefault()?.Value);
        XElement element = new XElement("product",
            new XAttribute("ID", p.ID),
            new XAttribute("Name", p.Name),
            new XAttribute("Category", p.Category),
            new XAttribute("Price", p.Price),
            new XAttribute("InStock", p.InStock));
        root?.Add(element);
        root?.Save("..\\xml\\Product.xml");
        config?.Element("productId")?.SetValue(Convert.ToInt32(config?.Elements("productId")?.FirstOrDefault()?.Value) + 1);
        config?.Save("..\\xml\\Config.xml");
        return p.ID;
    }

    /// <summary>
    /// the function gets the id of the product that we want to remove and according to
    /// the product id the function delete this product in the Product.xml file
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        root?.Elements("product")?.Where(p => Convert.ToInt32(p?.Attribute("ID")?.Value) == id).Remove();
        root?.Save("..\\xml\\Product.xml");
    }

    /// <summary>
    /// if the function doesn't get any argument the function returns all the products,
    /// otherwise the function returns product according to the lambada
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product> ReadAll(Func<Product, bool>? func = null)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        IEnumerable<XElement> ProductsList = root?.Elements("product") ?? throw new Exception();
        List<DO.Product> allProduct = Casting(ProductsList).ToList();
        return func == null ? allProduct : allProduct.Where(func);
    }

    /// <summary>
    /// The function returns the first product from the Product.xml file
    /// that meets the condition lambada 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product ReadSingle(Func<Product, bool> func)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        IEnumerable<XElement> ProductsList = root?.Elements("product") ?? throw new Exception();
        List<DO.Product> allProduct = Casting(ProductsList).ToList();
        return allProduct.Where(func).FirstOrDefault();
    }

    /// <summary>
    /// the function gets the product with changes and update this product
    /// according to the product id in the Product.xml file
    /// </summary>
    /// <param name="p"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product p)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        XElement? e = root?.Elements("product")?.
                    Where(e => Convert.ToInt32(e.Attribute("ID")?.Value) == p.ID).FirstOrDefault();
        e?.Attribute("Name")?.SetValue(p.Name);
        e?.Attribute("Category")?.SetValue(p.Category);
        e?.Attribute("Price")?.SetValue(p.Price);
        e?.Attribute("InStock")?.SetValue(p.InStock);
        root?.Save("..\\xml\\Product.xml");
    }

    /// <summary>
    /// when we make a new order we need to apdate the amount in stock,
    /// so this function gets product id and amount and the func update
    /// the amount in the Product.xml file
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UpdateAmount(int id, int amount)
    {
        XElement? root = XDocument.Load("..\\xml\\Product.xml").Root;
        XElement? e = root?.Elements("product")?.
                    Where(e => Convert.ToInt32(e.Attribute("ID")?.Value) == id).FirstOrDefault();
        e?.Attribute("InStock")?.SetValue(Convert.ToInt32(e.Attribute("InStock")?.Value) - amount);
        root?.Save("..\\xml\\Product.xml");
    }
}