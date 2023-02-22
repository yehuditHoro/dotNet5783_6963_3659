using System.ComponentModel;
using BlApi;
namespace Simulator;

public static class Simulator
{
    public static BO.Order? currentOrder;
    private static IBl? bl;
    private static bool IsFinish = true;
    public static void Run()
    {
        Thread thread = new Thread(new ThreadStart(ChooseOrder));
        thread.Start();
    }

    public static void ChooseOrder()
    {
        int? orderId = bl?.order.ChooseOrder();
        if (orderId != null)
            currentOrder = bl?.order.GetOrder((int)orderId);
        Random rand = new Random();
        int workTime = (int)rand.Next(500, 1000);
        Thread.Sleep(workTime);
        if (currentOrder?.Status == BO.eOrderStatus.confirmed)
            bl?.order.ShipedOrder(currentOrder.ID);
        else bl?.order.DeliveredOrder(currentOrder.ID);
    }

    public static void StopSimulation()
    {

    }
}
