
namespace BO;

public class Enums
{
    public enum eCategory {
    men,
    women,
    boys,
    girls,
    babies
    }

    public enum eOrderStatus {
    confirmed,
    shiped,
    delivered
    }

    public enum eMenuOptions { 
    EXIT, 
    PRODUCT, 
    ORDER, 
    CART 
    };

    public enum eProductOptions { 
    GetProducts, 
    GetCatalog, 
    GetItemsManager, 
    GetItemsCustomer,
    Add,
    Update,
    Remove 
    };

    public enum eOrderOptions {
    GetOrders,
    GetOrder,
    ShipedOrder,
    DeliveredOrder
    };

    public enum eCartOptions {
    Add,
    UpdateQuantity,
    MakeOrder
    };
}

