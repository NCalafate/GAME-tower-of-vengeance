using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Integer", menuName = "Scriptable Integer")]
public class ScriptableInteger : ScriptableObject
{
    private int _value;
    [SerializeField] int _startingValue;

    public delegate void ValueChanged();
    private List<ValueChanged> _listeners = new List<ValueChanged>();

    private void Awake()
    {
        StartingInteger();
    }

    private void OnDestroy()
    {
        StartingInteger();
    }

    private void OnDisable()
    {
        StartingInteger();
    }

    private void OnEnable()
    {
        StartingInteger();
    }

    private void OnValidate()
    {
        StartingInteger();
    }

    public int Value
    {
        get => _value;

        set
        {
            _value = value;

            CallDelegates();
        }
    }

    public void RemoveListener(ValueChanged listener)
    {
        _listeners.Remove(listener);
    }

    public void AddListener(ValueChanged listener)
    {
        _listeners.Add(listener);
    }

    private void CallDelegates()
    {
        foreach (var listener in _listeners)
        {
            listener.Invoke();
        }
    }

    public void StartingInteger()
    {
        _value = _startingValue;
        _listeners.Clear();
    }
}
