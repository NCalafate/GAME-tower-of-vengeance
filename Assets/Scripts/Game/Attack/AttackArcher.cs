using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackArcher : Attack, AttackInterface
{
    public void AttackBasic(IReadOnlyList<RaycastHit> hits, float accuracy)
    {
        UseAttackToken();
        if (!isTargetSuccess(accuracy)) return;

        if (!isTarget(hits[^1])) return;
        var target = hits[^1].collider.gameObject.GetComponent<Enemy>();

        target.Hurt((int)AttackSet.AttackA.Damage);

        FileActor actor = gameObject.AddComponent<FileActor>();
        actor.GenerateText("Archer", "BASIC ATTACK WITH DAMAGE: " + AttackSet.AttackA.Damage);
    }

    public void AttackClass(IReadOnlyList<RaycastHit> hits, float accuracy)
    {
        UseAttackToken();
        if (!isTargetSuccess(accuracy)) return;

        if (!isTarget(hits[^1])) return;
        var target = hits[^1].collider.gameObject.GetComponent<Enemy>();

        target.Hurt((int)AttackSet.AttackB.Damage);

        FileActor actor = gameObject.AddComponent<FileActor>();
        actor.GenerateText("Archer", "CLASS ATTACK WITH DAMAGE: " + AttackSet.AttackB.Damage);
    }

    public void AttackSpecial(IReadOnlyList<RaycastHit> hits, float accuracy)
    {
        UseAttackToken();
        if (!isTargetSuccess(accuracy)) return;

        foreach (var hit in hits)
            if (isTarget(hit))
            {
                var target = hit.collider.gameObject.GetComponent<Enemy>();

                target.Hurt((int)AttackSet.AttackC.Damage);

                FileActor actor = gameObject.AddComponent<FileActor>();
                actor.GenerateText("Archer", "SPECIAL ATTACK WITH DAMAGE: " + AttackSet.AttackC.Damage);
            }
    }

    public Action<RaycastHit[], float>[] Actions()
    {
        return new Action<RaycastHit[], float>[] { AttackBasic, AttackClass, AttackSpecial };
    }

    AttackSetScriptableObject AttackInterface.AttackSet()
    {
        return AttackSet;
    }
}