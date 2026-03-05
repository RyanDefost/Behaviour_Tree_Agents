using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[Serializable]
public class Blackboard
{
    public Dictionary<string, object> values = new();

    public T GetValue<T>(string name)
    {
        return values.ContainsKey(name) ? (T)values[name] : default;
    }

    public void SetValue<T>(string name, T value)
    {
        if (values.ContainsKey(name))
        {
            values[name] = value;
        }
        else
        {
            values.Add(name, value);
        }
    }
}
