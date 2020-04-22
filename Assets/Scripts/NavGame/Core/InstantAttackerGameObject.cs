﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class InstantAttackerGameObject : AttackGameObject
    {
        // Start is called before the first frame update
        protected override void Attack(DamageableGameObject target)
        {
            target.TakeDamage(ofenseStats.damage);

            if (onAttackStrike != null)
            {
                onAttackStrike(target.damageTransform.position);
            }
        }
    }
}
