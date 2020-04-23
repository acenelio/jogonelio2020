using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class ProjectileController : MonoBehaviour
    {
        public float speed = 8f;

        DamageableGameObject target;

        void Start()
        {

        }

        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.damageTransform.position, speed * Time.deltaTime);
        }

        public void Init(DamageableGameObject target)
        {
            this.target = target;
        }
    }
}