﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Simulator;

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
    public Tuple<int, BO.eOrderStatus, BO.eOrderStatus, int> tuple;
    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    public SimulatorWindow()
    {
        InitializeComponent();
        background = new BackgroundWorker();
        background.DoWork += Background_DoWork;
        background.ProgressChanged += Background_ProgressChanged;
        background.RunWorkerCompleted += Background_RunWorkerCompleted;
        background.WorkerReportsProgress = true;
        background.WorkerSupportsCancellation = true;
        clock = DateTime.Now.ToString();
        DataContext = clock;
        isTimerRun = true;
        background.RunWorkerAsync();
    }

    private void onLoad(object sender, RoutedEventArgs e)
    {
        var hwnd = new WindowInteropHelper(this).Handle;
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    private void Background_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (isTimerRun)
        {
            isTimerRun = false;
            Simulator.Simulator.StopSimulation();
        }
    }

    private void Background_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.Run();
        Simulator.Simulator.progreesChange += updateView;
        Simulator.Simulator.stopSimulation += stopSimulator;
        while (isTimerRun)
        {
            background.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    private void updateView(object? sender, EventArgs e)
    {
        if (!(e is OrderStatus))
            return;
        OrderStatus os = e as OrderStatus;
        if (os == null) return;
        tuple = new Tuple<int, BO.eOrderStatus, BO.eOrderStatus, int>(os.orderId, os.oldStatus, os.newStatus, os.time);
        if (!CheckAccess())
        {
            Dispatcher.BeginInvoke(updateView, sender, e);
        }
        else
        {
            DataContext = tuple;
        }
    }

    private void Background_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        timerTextBlock.DataContext = DateTime.Now.ToString();
    }

    private void stopSimulator(object? sender, EventArgs e)
    {
        MessageBox.Show("complete updating");
        this.Close();
    }

    private void stop_Click(object sender, RoutedEventArgs e)
    {
        if (isTimerRun)
        {
            isTimerRun = false;
            Simulator.Simulator.StopSimulation();
        }
    }
}