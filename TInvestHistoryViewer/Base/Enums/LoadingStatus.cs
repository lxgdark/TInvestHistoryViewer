namespace TInvestHistoryViewer.Base.Enums;
/// <summary>
/// Статус загрузки страницы
/// </summary>
public enum LoadingStatus
{
    /// <summary>
    /// Не загружалось
    /// </summary>
    NotLoad,
    /// <summary>
    /// В процессе загрузки
    /// </summary>
    Loading,
    /// <summary>
    /// Загружено
    /// </summary>
    Load,
    /// <summary>
    /// Ошибка загрузки
    /// </summary>
    Error,
    /// <summary>
    /// Отмена загрузки
    /// </summary>
    Cancel
}
