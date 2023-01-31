using TInvestHistoryViewer.Services;
using Prism.Commands;
using System.Windows.Documents;
using System.Threading.Tasks;
using System.Threading;

namespace TInvestHistoryViewer.ViewModels;

public class MainWindowViewModel
{
    private readonly TinkoffTradingService _tradingService;
    public MainWindowViewModel(TinkoffTradingService tradingService)
    {
        _tradingService = tradingService;
        RefreshCommand = new DelegateCommand(async () => await RefreshAsync());
    }

    #region Команды"
    /// <summary>
    /// Команда выполняема при загрузке окна/страницы
    /// </summary>
    protected DelegateCommand RefreshCommand { get; init; }
    #endregion

    #region "Методы"
    /// <summary>
    /// Вызывается при загузке окна/страници
    /// </summary>
    public void OnLoaded() => RefreshCommand.Execute();

    private async Task RefreshAsync()
    {
       await _tradingService.Test();
    }
    #endregion
}
