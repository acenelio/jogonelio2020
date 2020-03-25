using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavGame.Core;
using NavGame.Managers;

public class CreepController : AttackGameObject
{
    DamageableGameObject finalTarget;

    protected override void Awake()
    {
        base.Awake();
        GameObject obj = GameObject.FindWithTag("Finish");
        if (obj != null)
        {
            finalTarget = obj.GetComponent<DamageableGameObject>();
        }

        onAttackHit += PlayEffects;
    }

    protected override void Update()
    {
        base.Update();
        if (finalTarget != null)
        {
            agent.SetDestination(finalTarget.transform.position);
            if (IsInTouch(finalTarget))
            {
                AttackOnCooldown(finalTarget);
            }
        }
    }

    void PlayEffects(Vector3 position)
    {
        AudioManager.instance.Play("enemy-hit", position);
    }
}
