using System.Collections;
using System.Collections.Generic;
using Humanoids;
using Unity.VisualScripting;
using UnityEngine;

public class CommandMove : Command
{
    private Humanoid _associatedActor;

    private Vector3 _endPosition;

    public CommandMove(Humanoid actor, Vector3 destination)
    {
        _associatedActor = actor;
        _endPosition = destination;
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    public void Execute()
    {
        _associatedActor.MoveTo(_endPosition);
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
        actor.GenerateText(_associatedActor.name, "MOVEMENT " + _endPosition);
    }
}
