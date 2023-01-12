using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// if the id from the user does not exist
/// </summary>
public class BlIdNotFound : Exception {
    public BlIdNotFound(DalApi.EntityNotFoundException? inner = null) : base("id not found", inner) { }
    public override string Message =>
                    "id not found";
}
/// <summary>
/// if the user try to add item with an existing id
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
/// when the user's input is more than the amount in stock in the datasource
/// </summary>
public class BlOutOfStockException : Exception
{
    public override string Message =>
                    "out of stock";
}
/// <summary>
/// when the user's input is null
/// </summary>
public class BlNullException : Exception
{
    public override string Message =>
                    "null value exception";
}
/// <summary>
/// when it failed to add an item
/// </summary>
 public class BlFailedToAdd : Exception
{
    public override string Message =>
                    "failed to add this item";
}
/// <summary>
/// when it failed to get an item
/// </summary>
public class BlFailedToGet : Exception
{
    public override string Message =>
                    "failed to get this item";
}
/// <summary>
/// when it failed to update an item
/// </summary>
public class BlFailedToUpdate : Exception
{
    public override string Message =>
                    "failed to update this item";
}
/// <summary>
/// when it failed to delete an item
/// </summary>
public class BlFailedToDelete : Exception
{
    public override string Message =>
                    "failed to delete this item";
}




