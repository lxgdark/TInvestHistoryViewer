using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Windows;
using TInvestHistoryViewer.ViewModels;
using TInvestHistoryViewer.Views;

namespace TInvestHistoryViewer;

public partial class App : Application
{
    private static Mutex _appMutex;

    /// <summary>
    /// Инициализация
    /// </summary>
    public App() { }

    #region "Свойства"
    /// <summary>
    /// Провайдер сервисов для сервисной архитектуры
    /// </summary>
    private static ServiceProvider ServiceProvider { get; set; }
    #endregion

    #region "Методы"
    /// <summary>
    /// Получает сервис для указанного T
    /// </summary>
    /// <returns>Возвращает экземпляр класса связанного с этим сервисом</returns>
    public static T GetService<T>() => ServiceProvider.GetRequiredService<T>();

    /// <summary>
    /// Запуск приложения
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        //Проверяет, что запущен только один Экземпляр приложения
        CheckSingleInstance("TInvestHystory");

        //Получение файла конфигурации
        string appsettingsPath = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development" ? "appsettings.Development.json" : "appsettings.json";

        //Построение DI контейнера
        ServiceProvider = ConfigureServices(new ServiceCollection(), appsettingsPath).BuildServiceProvider();

        //Запуск главного окна
        Current.MainWindow = GetService<MainWindow>();
        Current.MainWindow.Show();
    }

    /// <summary>
    /// Конфигурирует сервисы
    /// </summary>
    private static IServiceCollection ConfigureServices(IServiceCollection services, string appsettingsPath)
    {
        //Реализация работы с конфигурациями
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(appsettingsPath, false, false).Build();
        services.AddSingleton(s => configuration);

        //Добавление экземпляров классов и сервисов
        services
            .AddSingleton<MainWindow>()
            .AddSingleton<MainWindowViewModel>();

        return services;
    }

    /// <summary>
    /// Проверяет, что запущен только один экземпляр приложения
    /// </summary>
    private static void CheckSingleInstance(string name)
    {
        if (_appMutex is null)
        {
            Mutex mutex = new(true, name, out bool createdNew);
            if (!createdNew)
            {
                mutex.Dispose();
                throw new InvalidOperationException("Может быть открыт только один экземпляр приложения на одну учетную запись");
            }
            _appMutex = mutex;
        }
    }
    #endregion
}
