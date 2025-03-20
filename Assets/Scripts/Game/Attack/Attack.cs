using UnityEngine;
using Random = UnityEngine.Random;

public class Attack : MonoBehaviour
{
    [SerializeField] protected LayerMask _layerTarget;
    [SerializeField] private AttackSetScriptableObject _attackSet;
    [SerializeField] private ScriptableInteger _turn;
    [SerializeField] private ScriptableInteger _intermission;

    private float _remainingAttacks;


    protected AttackSetScriptableObject AttackSet => _attackSet;

    public void Start()
    {
        HandleTurn();
        _turn.AddListener(HandleTurn);
        _intermission.AddListener(HandleTurn);

    }

    protected bool isTargetSuccess(float accuracy)
    {
        var accuracyCheck = Random.Range(0, 100);
        var acc = (int)accuracy;
        return accuracyCheck < acc;
    }

    protected bool isTarget(RaycastHit hit)
    {
        var target = hit.collider.gameObject;
        LayerMask hitLayer = 1 << target.layer;
        return hitLayer == _layerTarget.value;
    }

    protected void HandleTurn()
    {
        _remainingAttacks = _attackSet.Attacks;
    }

    protected void UseAttackToken()
    {
        _remainingAttacks -= 1;
    }
}