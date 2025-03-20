using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Humanoid Statistics", menuName = "Humanoid")]
public class HumanoidStatistics : ScriptableObject
{
    private string _name;
    private int _kills;
    private int _health;
    private int _maxHealth;
    private int _strength;
    private int _defense;
    private float _dexterity;
    private float _movementRange;
    private float _attackRange;

    public delegate void KillsChanged();
    private List<KillsChanged> _listenersKillsChanged = new List<KillsChanged>();

    public delegate void HealthChanged();
    private List<HealthChanged> _listenersHealthChanged = new List<HealthChanged>();

    public delegate void MaxHealthChanged();
    private List<MaxHealthChanged> _listenersMaxHealthChanged = new List<MaxHealthChanged>();

    public delegate void StrengthChanged();
    private List<StrengthChanged> _listenersStrengthChanged = new List<StrengthChanged>();

    public delegate void DefenseChanged();
    private List<DefenseChanged> _listenersDefenseChanged = new List<DefenseChanged>();

    public delegate void DexterityChanged();
    private List<DexterityChanged> _listenersDexterityChanged = new List<DexterityChanged>();

    public delegate void MovementRangeChanged();
    private List<MovementRangeChanged> _listenersMovementRangeChanged = new List<MovementRangeChanged>();

    public delegate void AttackRangeChanged();
    private List<AttackRangeChanged> _listenersAttackRangeChanged = new List<AttackRangeChanged>();

    [SerializeField] private string _assignedName;
    [SerializeField] private int _startingKills;
    [SerializeField] private int _baseHealth;
    [SerializeField] private int _baseStrength;
    [SerializeField] private int _baseDefense;
    [SerializeField] private float _baseDexterity;
    [SerializeField] private float _baseMovementRange;
    [SerializeField] private float _baseAttackRange;

    private void Awake()
    {
        BaselineHumanoid();
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public int Kills
    {
        get => _kills;
        set { _kills = value; CallKillsDelegates(); }
    }

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            CallHealthDelegates();
        }
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            CallMaxHealthDelegates();
        }
    }

    public int Strength
    {
        get => _strength;
        set
        {
            _strength = value;
            CallStrengthDelegates();
        }
    }

    public int Defense
    {
        get => _defense;
        set
        {
            _defense = value;
            CallDefenseDelegates();
        }
    }

    public float Dexterity
    {
        get => _dexterity;
        set
        {
            _dexterity = value;
            CallDexterityDelegates();
        }
    }

    public float MovementRange
    {
        get => _movementRange;
        set
        {
            _movementRange = value;
            CallMovementRangeDelegates();
        }
    }

    public float AttackRange
    {
        get => _attackRange;
        set
        {
            _attackRange = value;
            CallAttackRangeDelegates();
        }
    }

    private void BaselineHumanoid()
    {
        _name = _assignedName;
        _kills = _startingKills;
        _maxHealth = _baseHealth;
        _health = _maxHealth;
        _strength = _baseStrength;
        _defense = _baseDefense;
        _dexterity = _baseDexterity;
        _movementRange = _baseMovementRange;
        _attackRange = _baseAttackRange;
    }

    public void AddKillsListener(KillsChanged listener)
    {
        _listenersKillsChanged.Add(listener);
    }

    public void RemoveKillsListener(KillsChanged listener)
    {
        _listenersKillsChanged.Remove(listener);
    }

    private void CallKillsDelegates()
    {
        foreach (KillsChanged listener in _listenersKillsChanged)
        {
            listener.Invoke();
        }
    }

    public void AddHealthListener(HealthChanged listener)
    {
        _listenersHealthChanged.Add(listener);
    }

    public void RemoveHealthListener(HealthChanged listener)
    {
        _listenersHealthChanged.Remove(listener);
    }

    private void CallHealthDelegates()
    {
        foreach (HealthChanged listener in _listenersHealthChanged)
        {
            listener.Invoke();
        }
    }

    public void AddMaxHealthListener(MaxHealthChanged listener)
    {
        _listenersMaxHealthChanged.Add(listener);
    }

    public void RemoveMaxHealthListener(MaxHealthChanged listener)
    {
        _listenersMaxHealthChanged.Remove(listener);
    }

    private void CallMaxHealthDelegates()
    {
        foreach (MaxHealthChanged listener in _listenersMaxHealthChanged)
        {
            listener.Invoke();
        }
    }

    public void AddStrengthListener(StrengthChanged listener)
    {
        _listenersStrengthChanged.Add(listener);
    }

    public void RemoveStrengthListener(StrengthChanged listener)
    {
        _listenersStrengthChanged.Remove(listener);
    }

    private void CallStrengthDelegates()
    {
        foreach (StrengthChanged listener in _listenersStrengthChanged)
        {
            listener.Invoke();
        }
    }

    public void AddDefenseListener(DefenseChanged listener)
    {
        _listenersDefenseChanged.Add(listener);
    }

    public void RemoveDefenseListener(DefenseChanged listener)
    {
        _listenersDefenseChanged.Remove(listener);
    }

    private void CallDefenseDelegates()
    {
        foreach (DefenseChanged listener in _listenersDefenseChanged)
        {
            listener.Invoke();
        }
    }

    public void AddDexterityListener(DexterityChanged listener)
    {
        _listenersDexterityChanged.Add(listener);
    }

    public void RemoveDexterityListener(DexterityChanged listener)
    {
        _listenersDexterityChanged.Remove(listener);
    }

    private void CallDexterityDelegates()
    {
        foreach (DexterityChanged listener in _listenersDexterityChanged)
        {
            listener.Invoke();
        }
    }

    public void AddMovementRangeListener(MovementRangeChanged listener)
    {
        _listenersMovementRangeChanged.Add(listener);
    }

    public void RemoveMovementRangeListener(MovementRangeChanged listener)
    {
        _listenersMovementRangeChanged.Remove(listener);
    }

    private void CallMovementRangeDelegates()
    {
        foreach (MovementRangeChanged listener in _listenersMovementRangeChanged)
        {
            listener.Invoke();
        }
    }

    public void AddAttackRangeListener(AttackRangeChanged listener)
    {
        _listenersAttackRangeChanged.Add(listener);
    }

    public void RemoveAttackRangeListener(AttackRangeChanged listener)
    {
        _listenersAttackRangeChanged.Remove(listener);
    }

    private void CallAttackRangeDelegates()
    {
        foreach (AttackRangeChanged listener in _listenersAttackRangeChanged)
        {
            listener.Invoke();
        }
    }

    public void TurnRefresh()
    {
        _movementRange = _baseMovementRange;
        CallMovementRangeDelegates();
    }
}



