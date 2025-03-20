using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandAttackEnemy : Command
{
    private Enemy _associatedActor;

    public CommandAttackEnemy(Enemy actor)
    {
        _associatedActor = actor;
    }

    public void Execute()
    {
        // Enemies don't need the execute.
    }

    public bool Finished()
    {
        // Enemy attacks are instant.

        Log();
        return true;

    }

    /// <summary>
    /// Logs the command.
    /// </summary>
    public void Log()
    {
        FileActor actor = new FileActor();
        actor.GenerateText(_associatedActor.name, "BASIC ATTACK");
    }
}