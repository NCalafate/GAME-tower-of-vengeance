using System;
using System.Collections.Generic;
using UnityEngine;

public interface AttackInterface
{
    public void AttackBasic(IReadOnlyList<RaycastHit> hits, float accuracy);

    public void AttackClass(IReadOnlyList<RaycastHit> hits, float accuracy);

    public void AttackSpecial(IReadOnlyList<RaycastHit> hits, float accuracy);

    public Action<RaycastHit[], float>[] Actions();
    public AttackSetScriptableObject AttackSet();
}