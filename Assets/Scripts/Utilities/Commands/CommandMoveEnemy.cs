using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandMoveEnemy : Command
{
    private Enemy _associatedActor;

    public CommandMoveEnemy(Enemy actor)
    {
        _associatedActor = actor;
    }

    public void Execute()
    {
        // Enemies don't need the execute.
    }

    /// <summary>
    /// Checks if the command has finished.
    /// </summary>
    public bool Finished()
    {
        if (_associatedActor.IsMoving()) return false;

        Log();
        return true;

    }

    /// <summary>
    /// Logs the command.
    /// </summary>
    public void Log()
    {
        FileActor actor = new FileActor();
        actor.GenerateText(_associatedActor.name, "MOVEMENT");
    }
}
