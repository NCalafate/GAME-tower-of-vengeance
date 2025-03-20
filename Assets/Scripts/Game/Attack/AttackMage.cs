using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackMage : Attack, AttackInterface
{
    public void AttackBasic(IReadOnlyList<RaycastHit> hits, float accuracy)
    {
        UseAttackToken();
        if (!isTargetSuccess(accuracy)) return;

        if (!isTarget(hits[^1])) return;
        var target = hits[^1].collider.gameObject.GetComponent<Enemy>();
        target.Hurt((int)AttackSet.AttackA.Damage);

        FileActor actor = gameObject.AddComponent<FileActor>();
        actor.GenerateText("Mage", "BASIC ATTACK WITH DAMAGE: " + AttackSet.AttackA.Damage);
    }

    public void AttackClass(IReadOnlyList<RaycastHit> hits, float accuracy)
    {
        UseAttackToken();
        if (!isTargetSuccess(accuracy)) return;

        if (!isTarget(hits[^1])) return;
        var target = hits[^1].collider.gameObject.GetComponent<Enemy>();
        target.Hurt((int)AttackSet.AttackB.Damage);

        FileActor actor = gameObject.AddComponent<FileActor>();
        actor.GenerateText("Mage", "CLASS ATTACK WITH DAMAGE: " + AttackSet.AttackB.Damage);
    }

    public void AttackSpecial(IReadOnlyList<RaycastHit> hits, float accuracy)
    {
        UseAttackToken();
        if (!isTargetSuccess(accuracy)) return;

        if (!isTarget(hits[^1])) return;
        var target = hits[^1].collider.gameObject;
        var targetStatus = target.GetComponent<Enemy>();
        var x = new Ray(target.transform.position, transform.forward);
        var hitsCircle = Physics.SphereCastAll(x, 10.0f, 0.0f, _layerTarget.value);
        foreach (var hitCircle in hitsCircle)
            if (isTarget(hitCircle))
            {
                if (hitCircle.collider.gameObject != target)
                {
                    targetStatus.Hurt((int)AttackSet.AttackC.Damage);
                    FileActor actor = target.AddComponent<FileActor>();
                    actor.GenerateText("Mage", "SPECIAL ATTACK WITH DAMAGE: " + AttackSet.AttackC.Damage);
                }
                else 
                { 
                    targetStatus.Hurt((int)(AttackSet.AttackC.Damage / 2));
                    FileActor actor = target.AddComponent<FileActor>();
                    actor.GenerateText("Mage", "SPECIAL ATTACK WITH DAMAGE: " + AttackSet.AttackC.Damage / 2);
                }
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