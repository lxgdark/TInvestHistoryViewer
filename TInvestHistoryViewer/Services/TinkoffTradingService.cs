using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace TInvestHistoryViewer.Services;

public class TinkoffTradingService
{
    #region "Локальные"
    readonly InvestApiClient _investApi;
    #endregion

    public TinkoffTradingService(InvestApiClient investApi) =>
        _investApi = investApi;

    /// <summary>
    /// Получение списка биржевых счетов
    /// </summary>
    public async Task<List<Account>> GetInvestAccountsAsync()
    {
        GetAccountsResponse response = await _investApi.Users.GetAccountsAsync();
        return response.Accounts.Where(w => w.Type is AccountType.Tinkoff or AccountType.TinkoffIis).ToList();
    }

    public async Task<decimal> GetHistoryAsync(string accountId)
    {
        List<OperationItem> result = new();
        GetOperationsByCursorRequest request = new()
        {
            AccountId = accountId,
            Limit = 1000
        };

        bool hasNext = true;
        while (hasNext)
        {
            GetOperationsByCursorResponse response = await _investApi.Operations.GetOperationsByCursorAsync(request);
            result.AddRange(response.Items);
            request.Cursor = response.NextCursor;
            hasNext = response.HasNext;

        }

        decimal x = result.Where(w => w.Payment.Currency == "rub").Sum(s => s.Payment.Units + s.Commission.Units);
        return x;
    }
}
