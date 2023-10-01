using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TInvestHistoryViewer.Extensions;
/// <summary>
/// Расширения для <see cref="ObservableCollection{T}"/>
/// </summary>
public static class ObservableCollectionExtensions
{
    /// <summary>
    /// Заменяет целевую коллекцию переданной
    /// </summary>
    public static void Replace<T>(this ObservableCollection<T> collection, List<T> source)
    {
        collection.Clear();
        source?.ForEach(new Action<T>(collection.Add));
    }

    /// <summary>
    /// Заменяет целевую коллекцию переданной, выполняя указный Action для каждого элемента коллекции
    /// </summary>
    /// <param name="itemAction">Действие применяемое к элементу коллекции</param>
    public static void Replace<T>(this ObservableCollection<T> collection, List<T> source, Action<T> itemAction)
    {
        collection.Clear();
        source?.ForEach(f =>
        {
            itemAction.Invoke(f);
            collection.Add(f);
        });
    }

    /// <summary>
    /// Добавляет в целевую коллекцию переданную
    /// </summary>
    public static void AddRange<T>(this ObservableCollection<T> collection, List<T> source) =>
        source?.ForEach(new Action<T>(collection.Add));
}