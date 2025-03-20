using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Selected Object Controller", menuName = "Selection Controller")]
public class SelectedObjectScriptable : ScriptableObject
{
    private GameObject _currentSelectedObject = null;

    public delegate void SelectedChanged();
    private List<SelectedChanged> _listeners = new List<SelectedChanged>();

    private void Awake()
    {
        InitialSelection();
    }

    private void OnDestroy()
    {
        InitialSelection();
    }

    private void OnDisable()
    {
        InitialSelection();
    }

    private void OnEnable()
    {
        InitialSelection();
    }

    private void OnValidate()
    {
        InitialSelection();
    }

    public void InitialSelection()
    {
        _currentSelectedObject = null;
        _listeners.Clear();
    }

    public GameObject CurrentlySelected 
    { 
        get => _currentSelectedObject;
        set 
        {
            _currentSelectedObject = value;

            CallDelegates();
        } 
    }

    public void RemoveListener(SelectedChanged listener)
    {
        _listeners.Remove(listener);
    }

    public void AddListener(SelectedChanged listener)
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

    public void Deselect()
    {
        if (_currentSelectedObject != null)
        {
            _currentSelectedObject.transform.GetChild(0).gameObject.SetActive(false);

            _currentSelectedObject = null;

            CallDelegates();
        }
    }

    public void Select(GameObject clickedObject)
    {
        _currentSelectedObject = clickedObject;

        _currentSelectedObject.transform.GetChild(0).gameObject.SetActive(true);

        CallDelegates();
    }
}
