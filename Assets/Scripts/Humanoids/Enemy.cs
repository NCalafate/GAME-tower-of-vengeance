using System.Collections;
using System.Collections.Generic;
using Humanoids;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] ScriptableInteger _aliveEnemies;
    [SerializeField] CommandsScriptable _commandListEnemy;
    [SerializeField] ScriptableInteger _intermission;
    [SerializeField] ScriptableInteger _intermissionEnemy;
    [SerializeField] EnemyStatistics _baselineStatistics;

    public EnemyStatistics InstanceStatistics => _instanceStatistics;

    [SerializeField] HumanoidClass _class;

    private EnemyStatistics _instanceStatistics;

    private Animator _characterAnimator;

    private Vector3 _previousPosition;
    private NavMeshAgent _agent;
    private Command _command;
    private List<GameObject> _playerCharacters = new List<GameObject>();
    private bool _canAttack;
    private GameObject _aggro;
    private bool _dead;

    private void Start()
    {
        _aliveEnemies.Value += 1;

        _canAttack = true;

        _agent = GetComponent<NavMeshAgent>();
        _instanceStatistics = Instantiate(_baselineStatistics);
        _intermission.AddListener(StartMovement);
        _intermissionEnemy.AddListener(NewTurn);

        _previousPosition = transform.position;

        _characterAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();

        _playerCharacters.Add(GameObject.Find("Archer"));
        _playerCharacters.Add(GameObject.Find("Warrior"));
        _playerCharacters.Add(GameObject.Find("Mage"));
    }

    private void Update()
    {
        if(_agent.remainingDistance <= _instanceStatistics.AttackRange)
        {
            _agent.isStopped = true;
            _agent.ResetPath();
            Attack();
        }

        if (IsMoving())
        {
            if (!CanMove())
            {
                _agent.isStopped = true;
                _agent.ResetPath();
                Attack();
            }

            float movedDistance = Vector3.Distance(_previousPosition, transform.position);
            _instanceStatistics.MovementRange -= movedDistance;
            _previousPosition = transform.position;
        }
    }

    private bool Attack()
    {
        if (_aggro != null && _canAttack == true)
        {
            float distanceEnemy = Vector3.Distance(transform.position, _aggro.transform.position);

            if (distanceEnemy <= _instanceStatistics.AttackRange && _aggro != null)
            {
                _aggro.GetComponent<Humanoid>().Hurt(_instanceStatistics.Strength);
                AttackCommand();
                _characterAnimator.SetTrigger("attack");

                _canAttack = false;

                return true;
            }
        }

        return false;
    }

    private void StartMovement()
    {
        _aggro = null;
        float closestTargetDistance = float.MaxValue;
        NavMeshPath path;
        NavMeshPath shortestPath = null;

        foreach (GameObject character in _playerCharacters)
        {
            if (character != null && !character.GetComponent<Humanoid>().IsDead())
            {
                path = new NavMeshPath();

                if (NavMesh.CalculatePath(transform.position, character.transform.position, _agent.areaMask, path))
                {
                    float distance = Vector3.Distance(transform.position, path.corners[0]);

                    for (int i = 1; i < path.corners.Length; i++)
                    {
                        distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                    }

                    if (distance < closestTargetDistance)
                    {
                        closestTargetDistance = distance;
                        shortestPath = path;
                        _aggro = character;
                    }
                }
            }
        }

        _canAttack = true;

        if (shortestPath != null)
        {
            if (!Attack())
            {
                _agent.SetPath(shortestPath);
                MoveCommand();
                _characterAnimator.SetBool("isIdle", false);
                _characterAnimator.SetBool("isWalking", true);
            }
        }
    }

    public void Hurt(int damage)
    {
        if(IsDead() == false)
        {
            _instanceStatistics.Health -= damage;

            if (_instanceStatistics.Health <= 0)
            {
                _aliveEnemies.Value -= 1;

                _dead = true;

                _intermission.RemoveListener(StartMovement);

                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Creates a new Move Command for the enemy.
    /// </summary>
    public bool MoveCommand()
    {
        _command = new CommandMoveEnemy(this);

        _commandListEnemy.AddCommand(_command);

        return true;
    }

    public bool AttackCommand()
    {
        _command = new CommandAttackEnemy(this);

        // Attack commands don't need to be added to the list.
        _command.Finished();

        return true;
    }

    public bool CanMove()
    {
        if (_instanceStatistics.MovementRange <= 0)
        {
            return false;
        }

        return true;
    }

    public bool IsDead()
    {
        return _dead;
    }

    public bool IsMoving()
    {
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _instanceStatistics.AttackRange || _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude < 0.5f)
                {
                    _characterAnimator.SetBool("isWalking", false);
                    _characterAnimator.SetBool("isIdle", true);
                    Attack();
                    return false;
                }
            }
        }
        
        return true;
    }

    private void NewTurn()
    {
        _canAttack = false;
        _instanceStatistics.TurnRefresh();
    }
}
