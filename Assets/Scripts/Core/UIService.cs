using System;
using System.Collections.Generic;

public class UIService
{
    public static UIService Instance { get; private set; } = new UIService();

    private Dictionary<Type, object> _uiList = new();

    // регистрация UI объекта
    public void Register<T>(T ui) where T : class
    {
        var type = typeof(T); // для чего?
        if (_uiList.ContainsKey(type))
            return;

        _uiList[type] = ui;
    }

    // получение UI объекта
    public T Get<T>() where T : class
    {
        var type = typeof(T);
        if (_uiList.TryGetValue(type, out var ui))
            return ui as T;

        return null;
    }

    public void Clear()
    {
        _uiList.Clear();
    }
}