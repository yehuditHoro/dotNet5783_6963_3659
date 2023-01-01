using DalApi;

namespace DalXml
{
    sealed internal class DalXml : IDal
    {
        public Iproduct product { get; } = new Dal.DalProduct();
        public Iorder order { get; } = new Dal.DalOrder();
        public IorderItem orderItem { get; } = new Dal.DalOrderItem();

    }
}