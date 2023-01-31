using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace TInvestHistoryViewer.Services;

public class TinkoffTradingService
{
    #region "Локальные"
    readonly Context context;
    #endregion
    public TinkoffTradingService(IConfiguration configuration)
    {
        IConfigurationSection section = configuration.GetSection("TinkoffApiToken");
        string token = section?.Value ?? throw new ArgumentNullException("TinkoffApiToken");
        Connection connection = ConnectionFactory.GetConnection(token);
        context = connection.Context;
    }

    public async Task Test ()
    {
        var x = await context.MarketStocksAsync();
    }
}
