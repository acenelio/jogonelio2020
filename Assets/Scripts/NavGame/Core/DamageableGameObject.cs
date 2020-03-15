﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class DamageableGameObject : TouchableGameObject
    {
        public int currentHealth;
        public Stats stats;

        public OnHealthChangedEvent onHealthChanged;
        public OnDiedEvent onDied;

        protected virtual void Awake()
        {
            currentHealth = stats.maxHealth;
        }

        public void TakeDamage(int amount)
        {
            amount -= stats.armor;
            amount = Mathf.Clamp(amount, 1, stats.maxHealth);

            currentHealth -= amount;
            if (onHealthChanged != null)
            {
                onHealthChanged(stats.maxHealth, currentHealth);
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            Destroy(gameObject);
            if (onDied != null)
            {
                onDied();
            }
        }
    }
}
