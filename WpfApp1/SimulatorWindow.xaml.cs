using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
/// 
public partial class SimulatorWindow : Window
{
    private bool isTimerRun;
    BackgroundWorker background;
    private string clock;
    public SimulatorWindow()
    {
        InitializeComponent();
        background = new BackgroundWorker();
        background.DoWork += Background_DoWork;
        background.ProgressChanged += Background_ProgressChanged;
        background.RunWorkerCompleted += Background_RunWorkerCompleted;
        background.WorkerReportsProgress = true;
        clock = DateTime.Now.ToString();
        DataContext = clock;
        isTimerRun = true;
        background.RunWorkerAsync();
    }

    private void Background_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (isTimerRun)
        {
            isTimerRun = false;
        }
    }

    //private void stopTimerButton_Click(object sender, RoutedEventArgs e)
    //{
    //    if (isTimerRun)
    //    {
    //        stopWatch.Stop();
    //        isTimerRun = false;
    //    }
    //}
    private void Background_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.Run();
        while (isTimerRun)
        {
            background.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    private void Background_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        DataContext = DateTime.Now.ToString();
    }

    private void stop_Click(object sender, RoutedEventArgs e)
    {
        if (isTimerRun)
        {
            isTimerRun = false;
        }
    }

}