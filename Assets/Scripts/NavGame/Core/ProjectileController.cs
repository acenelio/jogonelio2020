using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class ProjectileController : MonoBehaviour
    {
        public float tolerance = 0.1f;
        public float speed = 8f;
        public OnAttackStrikeEvent onAttackStrike;

        DamageableGameObject target;
        int damage;
        bool isInit = false;


        void Start()
        {
            if (!isInit) 
            {
                Debug.LogError("ProjectileController Init was not called");
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.damageTransform.position, speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target.damageTransform.position);

            if (distance < tolerance) 
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
                if (onAttackStrike != null)
                {
                    onAttackStrike(target.damageTransform.position);
                }
            }
        }

        public void Init(DamageableGameObject target, int damage)
        {
            this.target = target;
            this.damage = damage;
            isInit = true;
        }
    }
}