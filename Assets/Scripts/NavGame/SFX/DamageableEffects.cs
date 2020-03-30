using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Core;
using NavGame.Managers;

namespace NavGame.Effects
{
    [RequireComponent(typeof(DamageableGameObject))]
    public class DamageableEffects : SFXController
    {
        public string damageSound;
        public string dieSound;

        public GameObject damageEffects;
        public GameObject dieEffects;

        protected virtual void Awake()
        {
            DamageableGameObject damageable = GetComponent<DamageableGameObject>();
            damageable.onDamageTaken += OnDamageTaken;
            damageable.onDied += OnDied;
        }

        protected virtual void OnDamageTaken(Vector3 point, int amount)
        {
            PlayEffects(point, damageSound, damageEffects);
        }

        protected virtual void OnDied()
        {
            PlayEffects(transform.position, dieSound, dieEffects);
        }
    }
}
