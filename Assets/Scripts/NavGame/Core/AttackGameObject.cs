using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavGame.Managers;

namespace NavGame.Core
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class AttackGameObject : TouchableGameObject
    {
        public OfenseStats ofenseStats;
        public string[] enemyLayers;

        [SerializeField]
        protected List<DamageableGameObject> enemiesToAttack = new List<DamageableGameObject>();

        public OnAttackHitEvent onAttackHit;

        protected NavMeshAgent agent;
        float cooldown = 0f;
        LayerMask enemyMask;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyMask = LayerMask.GetMask(enemyLayers);
        }

        protected virtual void Update()
        {
            DecreaseAttackCooldown();
        }

        public void AttackOnCooldown(DamageableGameObject target)
        {
            if (cooldown <= 0f)
            {
                cooldown = 1f / ofenseStats.attackSpeed;
                target.TakeDamage(ofenseStats.damage);
                if (onAttackHit != null)
                {
                    onAttackHit(target.transform.position);
                }
            }
        }

        void DecreaseAttackCooldown()
        {
            if (cooldown == 0f)
            {
                return;
            }
            cooldown -= Time.deltaTime;
            if (cooldown < 0f)
            {
                cooldown = 0f;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (enemyMask.Contains(other.gameObject.layer))
            {
                DamageableGameObject obj = other.transform.parent.gameObject.GetComponent<DamageableGameObject>();
                if (!enemiesToAttack.Contains(obj))
                {
                    enemiesToAttack.Add(obj);
                }
            }
        }
    }
}
