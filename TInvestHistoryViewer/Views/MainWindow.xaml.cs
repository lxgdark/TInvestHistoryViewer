using System;
using System.Windows;
using TInvestHistoryViewer.ViewModels;

namespace TInvestHistoryViewer.Views;

public partial class MainWindow : Window
{
    #region "Локальные"
    private readonly MainWindowViewModel _vm;
    #endregion

    public MainWindow(MainWindowViewModel vm)
    {
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        InitializeComponent();
        DataContext = _vm;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e) => _vm.OnLoaded();
}
