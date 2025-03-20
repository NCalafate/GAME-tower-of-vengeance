using UnityEngine;
using UnityEngine.AI;

namespace Humanoids
{
    public class Humanoid : MonoBehaviour
    {
        [SerializeField] CommandsScriptable _commandList;
        [SerializeField] ScriptableInteger _aliveCharacters;
        [SerializeField] ScriptableInteger _intermissionEnemy;
        [SerializeField] HumanoidStatistics _baselineStatistics;
        [SerializeField] HumanoidClass _class;
        
        private AudioSource _audio;
        private HumanoidStatistics _instanceStatistics;

        private Animator _characterAnimator;

        private Vector3 _previousPosition;
        private bool _canAttack;

        private Command _command;
        private NavMeshAgent _agent;

        private bool _killed = false;

        private void Start()
        {
            _aliveCharacters.Value += 1;

            _audio = GetComponent<AudioSource>();
            _agent = GetComponent<NavMeshAgent>();
            _instanceStatistics = Instantiate(_baselineStatistics);
            _intermissionEnemy.AddListener(NewTurn);

            _canAttack = true;
            _previousPosition = transform.position;

            _characterAnimator = gameObject.transform.GetChild(1).GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if(IsDead() == false)
            {
                if (IsMoving())
                {
                    if (!CanMove())
                    {
                        _agent.isStopped = true;
                        _agent.ResetPath();
                    }

                    float movedDistance = Vector3.Distance(_previousPosition, transform.position);
                    _instanceStatistics.MovementRange -= movedDistance;
                    _previousPosition = transform.position;
                }
            }
        }

        /// <summary>
        /// Checks if the Humanoid can move to a new position, if it can executes the command.
        /// </summary>
        public bool MoveCommand(Vector3 destination)
        {
            if(CanMove()) 
            {
                _command = new CommandMove(this, destination);
                _commandList.AddCommand(_command);
                _command.Execute();

                _canAttack = false;

                return true;
            }

            return false;
        }

        public bool IsDead()
        {
            return _killed;
        }

        /// <summary>
        /// Moves the Humanoid to a certain position using the NavMeshAgent.
        /// </summary>
        public void MoveTo(Vector3 newPosition)
        {
            _agent.SetDestination(newPosition);

            _characterAnimator.SetBool("isIdle", false);
            _characterAnimator.SetBool("isWalking", true);
        }

        /// <summary>
        /// Checks if the Humanoid has moves left.
        /// </summary>
        public bool CanMove()
        {
            if(_instanceStatistics.MovementRange <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Detects if the Humanoid is moving.
        /// </summary>
        public bool IsMoving()
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        _characterAnimator.SetBool("isWalking", false);
                        _characterAnimator.SetBool("isIdle", true);

                        return false;
                    }
                }
            }

            return true;
        }

        public void Hurt(int damage)
        {
            _audio.Play();
            _instanceStatistics.Health -= damage;

            if(!IsDead())
            {
                if (_instanceStatistics.Health <= 0)
                {
                    _killed = true;
                    _characterAnimator.SetTrigger("die");

                    _aliveCharacters.Value -= 1;
                }
            }
        }

        /// <summary>
        /// Resets actions for this Humanoid when there is a new turn.
        /// </summary>
        private void NewTurn()
        {
            _instanceStatistics.TurnRefresh();
            _canAttack = true;
        }

        /// <summary>
        /// Gets the class corresponding to this humanoid.
        /// </summary>
        public HumanoidClass GetClass()
        {
            return _class;
        }

        /// <summary>
        /// Gets the Scriptable Object associated with this humanoid.
        /// </summary>
        public HumanoidStatistics GetStatistics()
        {
            return _instanceStatistics;
        }
    }
}
