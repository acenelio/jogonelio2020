using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavGame.Managers;

namespace NavGame.Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class AttackGameObject : TouchableGameObject
    {
        public OfenseStats ofenseStats;
        public float attackRange = 4f;
        public float attackDelay = 0.5f;
        public Transform castTransform;
        public string[] enemyLayers;
        public bool isInCombat { get; private set; }

        [SerializeField]
        protected List<DamageableGameObject> enemiesToAttack = new List<DamageableGameObject>();

        protected NavMeshAgent agent;
        float cooldown = 0f;
        LayerMask enemyMask;

        public OnAttackStartEvent onAttackStart;
        public OnAttackCastEvent onAttackCast;
        public OnAttackStrikeEvent onAttackStrike;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyMask = LayerMask.GetMask(enemyLayers);

            if (castTransform == null)
            {
                castTransform = transform;
            }
        }

        protected virtual void Update()
        {
            DecreaseAttackCooldown();
            UpdateAttack();
        }

        protected virtual void UpdateAttack()
        {
            if (enemiesToAttack.Count > 0)
            {
                agent.SetDestination(enemiesToAttack[0].gameObject.transform.position);
                if (IsInRange(enemiesToAttack[0].gameObject.transform.position))
                {
                    agent.ResetPath();
                    FaceObjectFrame(enemiesToAttack[0].gameObject.transform);
                    AttackOnCooldown(enemiesToAttack[0]);
                    isInCombat = true;
                    return;
                }
            }
            isInCombat = false;
        }

        public void AttackOnCooldown(DamageableGameObject target)
        {
            if (cooldown <= 0f)
            {
                cooldown = 1f / ofenseStats.attackSpeed;
                if (onAttackStart != null) 
                {
                    onAttackStart();
                }
                StartCoroutine(AttackAfterDelay(target, attackDelay));
            }
        }

        IEnumerator AttackAfterDelay(DamageableGameObject target, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (target != null) 
            {
                if (onAttackCast != null)
                {
                    onAttackCast(castTransform.position);
                }
                
                Attack(target);
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
                DamageableGameObject obj = other.transform.parent.GetComponent<DamageableGameObject>();
                if (!enemiesToAttack.Contains(obj))
                {
                    enemiesToAttack.Add(obj);
                    obj.onDied += () => { enemiesToAttack.Remove(obj); }; 
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (enemyMask.Contains(other.gameObject.layer))
            {
                DamageableGameObject obj = other.transform.parent.GetComponent<DamageableGameObject>();
                enemiesToAttack.Remove(obj);
            }
        }


        public bool IsInRange(Vector3 point)
        {
            float distance = Vector3.Distance(transform.position, point);
            return distance <= attackRange;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        protected abstract void Attack(DamageableGameObject target);
    }
}
