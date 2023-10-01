using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TInvestHistoryViewer.Base;
/// <summary>
/// Расширение класса ObservableObject удобными методами
/// </summary>
public class ObservableModel : ObservableObject
{
    /// <summary>
    /// Установка свойства с вызовом переданного Action
    /// </summary>
    protected bool SetProperty<T>([NotNullIfNotNull(nameof(newValue))] ref T field, T newValue, Action action, [CallerMemberName] string propertyName = null)
    {
        bool result = SetProperty(ref field, newValue, propertyName);
        if (result)
        {
            action?.Invoke();
        }
        return result;
    }
}
