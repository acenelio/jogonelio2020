using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class ThrowerGameObject : AttackGameObject
    {
        public GameObject projectilePrefab;

        protected override void Attack(DamageableGameObject target)
        {
            GameObject projectile = Instantiate(projectilePrefab, castTransform.position, Quaternion.identity) as GameObject;
            ProjectileController controller = projectile.GetComponent<ProjectileController>();
            controller.Init(target, ofenseStats.damage);
            controller.onAttackStrike += OnAttackStrike;
        }

        void OnAttackStrike(Vector3 strikePoint)
        {
            if (onAttackStrike != null)
            {
                onAttackStrike(strikePoint);
            }
        }
    }
}
