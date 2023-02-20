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
    private Stopwatch stopWatch;
    private bool isTimerRun;
    BackgroundWorker timerworker;
    BackgroundWorker background;
    public SimulatorWindow()
    {
        InitializeComponent();
        background = new BackgroundWorker();
        background.DoWork += Background_DoWork;
        background.ProgressChanged += Background_ProgressChanged;
        background.WorkerReportsProgress = true;
        stopWatch = new Stopwatch();
        timerworker = new BackgroundWorker();
        timerworker.DoWork += Worker_DoWork;
        timerworker.ProgressChanged += Worker_ProgressChanged;
        timerworker.WorkerReportsProgress = true;
        stopWatch.Restart();
        isTimerRun = true;
        timerworker.RunWorkerAsync();
    }
 
    private void stopTimerButton_Click(object sender, RoutedEventArgs e)
    {
        if (isTimerRun)
        {
            stopWatch.Stop();
            isTimerRun = false;
        }
    }


    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        this.timerTextBlock.Text = timerText;
    }
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        while (isTimerRun)
        {
            timerworker.ReportProgress(1);
            Thread.Sleep(1000);
        }

       
    }
    private void Background_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Background_DoWork(object? sender, DoWorkEventArgs e)
    {
        throw new NotImplementedException();
    }
}
