using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] ScriptableInteger _turn;
    [SerializeField] ScriptableInteger _intermission;
    [SerializeField] ScriptableInteger _intermissionEnemy;
    [SerializeField] CommandsScriptable _commandList;
    [SerializeField] CommandsScriptable _commandListEnemy;

    private void Start()
    {
        _turn.AddListener(NextTurn);
        _intermission.AddListener(NextTurnEnemy);
    }

    /// <summary>
    /// Runs the players turn.
    /// </summary>
    public void NextTurn()
    {
        if(_turn.Value != 1)
        {
            StartCoroutine(ProcessTurn());
        }
    }

    /// <summary>
    /// Processes the current turn for the player.
    /// </summary>
    private IEnumerator ProcessTurn()
    {
        foreach (Command command in _commandList.CommandList)
        {
            while (!command.Finished())
            {
                yield return new WaitForSeconds(0.01f);
            }
        }

        yield return new WaitForSeconds(1f);

        _intermission.Value++;
        _commandList.CommandList.Clear();
    }

    /// <summary>
    /// Runs the enemies turn.
    /// </summary>
    public void NextTurnEnemy()
    {
        if(_intermission.Value != 1)
        {
            StartCoroutine(ProcessTurnEnemy());
        }
    }

    /// <summary>
    /// Processes the current turn for the enemies.
    /// </summary>
    private IEnumerator ProcessTurnEnemy()
    {
        List<Command> copy = new List<Command>(_commandListEnemy.CommandList);

        foreach (Command command in copy)
        {
            while (!command.Finished())
            {
                yield return new WaitForSeconds(0.01f);
            }
        }

        yield return new WaitForSeconds(3.5f);

        _intermissionEnemy.Value++;
        _commandListEnemy.CommandList.Clear();
    }
}
