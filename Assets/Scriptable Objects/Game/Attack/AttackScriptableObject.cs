using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackScriptableObject", menuName = "AttackScriptableObject")]
public class AttackScriptableObject : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Texture _icon;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private Action _action;

    public string Name => _name;

    public Texture Icon => _icon;

    public float Damage => _damage;

    public float Range => _range;
}