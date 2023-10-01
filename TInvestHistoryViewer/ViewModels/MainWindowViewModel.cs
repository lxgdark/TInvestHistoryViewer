using TInvestHistoryViewer.Services;
using Prism.Commands;
using System.Threading.Tasks;
using Tinkoff.InvestApi.V1;
using System.Collections.ObjectModel;
using TInvestHistoryViewer.Extensions;
using TInvestHistoryViewer.Base;
using System;
using TInvestHistoryViewer.Base.Enums;
using Google.Protobuf.WellKnownTypes;
using System.Windows;

namespace TInvestHistoryViewer.ViewModels;

public class MainWindowViewModel : ObservableModel
{
    #region Локальные
    private readonly TinkoffTradingService _tradingService;
    private Account _selectedAccount;
    #endregion
    //
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

    #region Свойства
    /// <summary>
    /// Список брокерских счетов
    /// </summary>
    public ObservableCollection<Account> Accounts { get; } = new();

    /// <summary>
    /// Выделенный брокерский счет
    /// </summary>
    public Account SelectedAccount
    {
        get => _selectedAccount;
        set
        {
            SetProperty(ref _selectedAccount, value);
            UpdateHistory();
        }
    }
    #endregion

    #region "Методы"
    /// <summary>
    /// Вызывается при загрузке окна/страницы
    /// </summary>
    public void OnLoaded() => RefreshAsync();

    /// <summary>
    /// Обновление данных
    /// </summary>
    /// <returns></returns>
    private Task RefreshAsync() =>
        HandleAsync(async () => Accounts.Replace(await _tradingService.GetInvestAccountsAsync()));

    private Task UpdateHistory() => HandleAsync(async () => await _tradingService.GetHistoryAsync(SelectedAccount.Id));
    #endregion

    #region Системыне
    private LoadingStatus _pageLoadStatus;
    /// <summary>
    /// Статус загрузки страницы
    /// </summary>
    public LoadingStatus PageLoadStatus { get => _pageLoadStatus; set => SetProperty(ref _pageLoadStatus, value); }

    /// <summary>
    /// Обработчик выполнения запросов
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private async Task HandleAsync(Func<Task> action)
    {
        PageLoadStatus = LoadingStatus.Loading;
        try
        {
            await action();
            PageLoadStatus = LoadingStatus.Load;
        }
        catch (Exception ex)
        {
            PageLoadStatus = LoadingStatus.Error;
            MessageBox.Show(ex.Message);
        }
    }
    #endregion
}