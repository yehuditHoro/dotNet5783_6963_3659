using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
public partial class SimulatorWindow : Window
{
    BackgroundWorker background;
    public SimulatorWindow()
    {
        InitializeComponent();
        background = new BackgroundWorker();
        background.DoWork += Background_DoWork;
        background.ProgressChanged += Background_ProgressChanged;
        background.WorkerReportsProgress = true;
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
