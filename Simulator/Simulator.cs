using System.ComponentModel;
using BlApi;
namespace Simulator;

public static class Simulator
{
    public static BO.Order? currentOrder;
    public static event EventHandler progreesChange;
    public static event EventHandler stopSimulation;
    private static IBl? bl = BlApi.Factory.Get();
    private static bool IsFinished = true;
    public static Random? rand;
    public static void Run()
    {
        Thread thread = new Thread(NextOrderToChange);
        thread.Start();
    }

    public static void NextOrderToChange()
    {
        try
        {
            while (IsFinished)
            {
                int? orderId = bl?.order.ChooseOrder();
                if (orderId == null)
                {
                    StopSimulation();
                }
                currentOrder = bl?.order.GetOrder((int)orderId);
                rand = new Random();
                int workTime = (int)rand.Next(3, 10);
                OrderStatus orderStatus;
                if (currentOrder?.Status == BO.eOrderStatus.confirmed)
                {
                    bl?.order.ShipedOrder(currentOrder.ID);
                    orderStatus = new OrderStatus((int)orderId, BO.eOrderStatus.confirmed, BO.eOrderStatus.shiped, workTime);
                }
                else
                {
                    bl?.order.DeliveredOrder(currentOrder.ID);
                    orderStatus = new OrderStatus((int)orderId, BO.eOrderStatus.shiped, BO.eOrderStatus.delivered, workTime);
                }
                if (progreesChange != null)
                    progreesChange(null, orderStatus);
                Thread.Sleep(workTime * 1000);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void StopSimulation()
    {
        IsFinished = false;
        if (stopSimulation != null)
            stopSimulation(null, EventArgs.Empty);
    }
}


public class OrderStatus : EventArgs
{
    public int orderId;
    public BO.eOrderStatus oldStatus;
    public BO.eOrderStatus newStatus;
    public int time;
    public OrderStatus(int oId, BO.eOrderStatus olds, BO.eOrderStatus news, int t)
    {
        this.orderId = oId;
        this.oldStatus = olds;
        this.newStatus = news;
        this.time = t;
    }
}
