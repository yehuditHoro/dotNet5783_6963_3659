using BlImplementation;
using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public class Factory
{
    //public static IBl bl { get; } = new BlImplementation.Bl();
    public static IBl Get()
    {
        IBl Ibl = new Bl();
        return Ibl;
    }
}

