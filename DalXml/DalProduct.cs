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
    
    public int Add(Product item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Product Read(int id)
    {
        throw new NotImplementedException();
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

