using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static ScriptableInteger;

[CreateAssetMenu(fileName = "New Command Controller", menuName = "Command Controller")]
public class CommandsScriptable : ScriptableObject
{
    private List<Command> _commandList = new List<Command>();

    public delegate void ListChanged();
    private List<ListChanged> _listeners = new List<ListChanged>();

    private void Awake()
    {
        StartingList();
    }

    private void OnDestroy()
    {
        StartingList();
    }

    private void OnDisable()
    {
        StartingList();
    }

    private void OnEnable()
    {
        StartingList();
    }

    private void OnValidate()
    {
        StartingList();
    }

    public List<Command> CommandList
    {
        get => _commandList;
    }

    public void AddCommand(Command command)
    {
        _commandList.Add(command);
        CallDelegates();
    }

    public void AddCommandSilent(Command command)
    {
        _commandList.Add(command);
    }

    public void RemoveCommand(Command command)
    {
        _commandList.Remove(command);
        CallDelegates();
    }

    public void RemoveCommandSilent(Command command)
    {
        _commandList.Remove(command);
    }

    public void RemoveListener(ListChanged listener)
    {
        _listeners.Remove(listener);
    }

    public void AddListener(ListChanged listener)
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

    public void StartingList()
    {
        _commandList = new List<Command>();
        _listeners.Clear();
    }
}
