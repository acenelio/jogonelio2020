using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Managers;

namespace NavGame.Core
{
    public class ProjectileController : BasicGameObject
    {
        public float tolerance = 0.1f;
        public float speed = 8f;
        public OnAttackStrikeEvent onAttackStrike;

        Transform strikeTransform;
        DamageableGameObject target;
        int damage;
        bool isInit = false;

        void Start()
        {
            if (!isInit)
            {
                Debug.LogError("ProjectileController Init was not called. Destroying GameObject");
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (target == null) // target may have died for another cause
            {
                Destroy(gameObject);
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, strikeTransform.position, speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, strikeTransform.position);
            if (distance < tolerance)
            {
                target.TakeDamage(damage);
                Hit();
                if (onAttackStrike != null)
                {
                    onAttackStrike(strikeTransform.position);
                }
            }
        }

        protected virtual void Hit()
        {
            Destroy(gameObject);
        }

        public void Init(DamageableGameObject target, int damage)
        {
            this.damage = damage;
            this.target = target;
            strikeTransform = target.damageTransform;
            isInit = true;
        }
    }
}
