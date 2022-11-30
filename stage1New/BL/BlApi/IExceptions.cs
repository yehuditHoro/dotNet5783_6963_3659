using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// if the id from the user does not exist
/// </summary>
public class BlEntityNotFound : Exception {
    public BlEntityNotFound(DalApi.EntityNotFoundException? inner = null) : base("id not found", inner) { }
    public override string Message =>
                    "id not found";
}
/// <summary>
/// 
/// </summary>
public class BlEntityDuplicate : Exception
{
    public BlEntityDuplicate(DalApi.EntityDuplicateException? inner = null) : base("entity not found", inner) { }
    public override string Message =>
                    "entity not found";
}
/// <summary>
/// when the user's input is not valid
/// </summary>
public class BlInvalidInputException : Exception
{
    public BlInvalidInputException(string message) :
                                 base(message)
    { }
   
}
/// <summary>
/// 
/// </summary>
public class BlOutOfStockException : Exception
{
    public override string Message =>
                    "out of stock";
}
/// <summary>
/// 
/// </summary>
public class BlNullException : Exception
{
    public override string Message =>
                    "null value exception";
}
/// <summary>
/// 
/// </summary>
 public class BlFailedToAdd : Exception
{
    public override string Message =>
                    "failed to add this item";
}
/// <summary>
/// 
/// </summary>
 public class BlFailedToGet : Exception
{
    public override string Message =>
                    "failed to get this item";
}
/// <summary>
/// 
/// </summary>
public class BlFailedToUpdate : Exception
{
    public override string Message =>
                    "failed to update this item";
}
/// <summary>
/// 
/// </summary>
public class BlFailedToDelete : Exception
{
    public override string Message =>
                    "failed to delete this item";
}
/// <summary>
/// 
/// </summary>



