using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{   
    public void Execute();

    public bool Finished();

    public void Log();
}
