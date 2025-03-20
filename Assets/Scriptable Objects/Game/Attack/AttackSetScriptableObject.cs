using UnityEngine;

[CreateAssetMenu(fileName = "AttackSetScriptableObject", menuName = "AttackSetScriptableObject")]
public class AttackSetScriptableObject : ScriptableObject
{
    [SerializeField] private float _attacks = 1.0f;
    
    [SerializeField] private AttackScriptableObject _attackA;
    [SerializeField] private AttackScriptableObject _attackB;
    [SerializeField] private AttackScriptableObject _attackC;
    public float Attacks => _attacks;

    public AttackScriptableObject AttackA => _attackA;

    public AttackScriptableObject AttackB => _attackB;

    public AttackScriptableObject AttackC => _attackC;
}