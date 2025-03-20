using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Statistics", menuName = "Enemy")]
public class EnemyStatistics : ScriptableObject
{
    private int _health;
    private int _maxHealth;
    private int _strength;
    private int _attackRange;
    private float _movementRange;

    public delegate void HealthChanged();
    private List<HealthChanged> _listenersHealthChanged = new List<HealthChanged>();

    public delegate void MaxHealthChanged();
    private List<MaxHealthChanged> _listenersMaxHealthChanged = new List<MaxHealthChanged>();

    public delegate void StrengthChanged();
    private List<StrengthChanged> _listenersStrengthChanged = new List<StrengthChanged>();

    public delegate void AttackRangeChanged();
    private List<AttackRangeChanged> _listenersAttackRangeChanged = new List<AttackRangeChanged>();

    public delegate void MovementRangeChanged();
    private List<MovementRangeChanged> _listenersMovementRangeChanged = new List<MovementRangeChanged>();

    [SerializeField] private int _startingExperience;
    [SerializeField] private int _baseHealth;
    [SerializeField] private int _baseStrength;
    [SerializeField] private int _baseAttackRange;
    [SerializeField] private float _baseMovementRange;

    private void Awake()
    {
        BaselineEnemy();
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

    public int AttackRange
    {
        get => _attackRange;
        set
        {
            _attackRange = value;
            CallAttackRangeDelegates();
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

    private void BaselineEnemy()
    {
        _maxHealth = _baseHealth;
        _health = _maxHealth;
        _strength = _baseStrength;
        _attackRange = _baseAttackRange;
        _movementRange = _baseMovementRange;
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

    public void TurnRefresh()
    {
        _movementRange = _baseMovementRange;
        CallMovementRangeDelegates();
    }
}

