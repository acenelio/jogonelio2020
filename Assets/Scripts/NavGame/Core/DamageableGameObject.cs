using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class DamageableGameObject : TouchableGameObject
    {
        public DefenseStats defenseStats;
        public int currentHealth;

        public OnHealthChangedEvent onHealthChanged;
        public OnDiedEvent onDied;

        protected virtual void Awake()
        {
            currentHealth = defenseStats.maxHealth;
        }

        public void TakeDamage(int amount)
        {
            amount -= defenseStats.armor;
            amount = Mathf.Clamp(amount, 1, defenseStats.maxHealth);

            currentHealth -= amount;
            if (onHealthChanged != null)
            {
                onHealthChanged(defenseStats.maxHealth, currentHealth);
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
