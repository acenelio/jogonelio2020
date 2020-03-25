using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavGame.Core;
using NavGame.Managers;

[RequireComponent(typeof(NavMeshAgent))]
public class CreepController : AttackGameObject
{
    NavMeshAgent agent;
    DamageableGameObject finalTarget;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
        if (finalTarget == null)
        {
            return;
        }
        if (IsInTouch(finalTarget))
        {
            AttackOnCooldown(finalTarget);
        }
    }

    void Start()
    {
        if (finalTarget != null)
        {
            agent.SetDestination(finalTarget.transform.position);
        }
    }

    void PlayEffects(Vector3 position)
    {
        AudioManager.instance.Play("enemy-hit", position);
    }
}
