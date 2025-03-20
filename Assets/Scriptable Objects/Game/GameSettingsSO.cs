using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "GameSettingsSO")]
public class GameSettingsSO : ScriptableObject
{
    public delegate void ChangedValue();

    [Range(0.0f, 1.0f)] [SerializeField] private float _music = 0.4f;
    [Range(0.0f, 1.0f)] [SerializeField] private float _sfx = 0.5f;

    private readonly List<ChangedValue> _listenersMusic = new();
    private readonly List<ChangedValue> _listenersSfx = new();

    public float Music
    {
        get => _music;
        set
        {
            _music = value;
            CallDelegatesMusic();
        }
    }

    public float Sfx
    {
        get => _sfx;
        set
        {
            _sfx = value;
            CallDelegatesSfx();
        }
    }


    public void RemoveListenerMusic(ChangedValue listener)
    {
        _listenersMusic.Remove(listener);
    }

    public void RemoveListenerSfx(ChangedValue listener)
    {
        _listenersSfx.Remove(listener);
    }

    public void AddListenerMusic(ChangedValue listener)
    {
        _listenersMusic.Add(listener);
    }

    public void AddListenerSfx(ChangedValue listener)
    {
        _listenersSfx.Add(listener);
    }


    private void CallDelegatesMusic()
    {
        foreach (var listener in _listenersMusic) listener.Invoke();
    }

    private void CallDelegatesSfx()
    {
        foreach (var listener in _listenersSfx) listener.Invoke();
    }
}