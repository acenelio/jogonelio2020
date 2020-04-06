using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Core;

namespace NavGame.Effects
{
    [RequireComponent(typeof(DamageableGameObject))]
    public class DamageableEffects : SFXController
    {
        public string damageSound;
        public string dieSound;

        public GameObject damageEffects;
        public GameObject dieEffect;

        void Awake()
        {
            DamageableGameObject damageable = GetComponent<DamageableGameObject>();
            damageable.onDamageTaken += OnDamageTaken;
            damageable.onDied += OnDied;
        }

        protected virtual void OnDamageTaken(Vector3 strikePoint, int amount)
        {
            PlayEffects(strikePoint, damageSound, damageEffects, Quaternion.identity);
        }

        protected virtual void OnDied()
        {
            PlayEffects(transform.position, dieSound, dieEffect, Quaternion.identity);
        }
    }
}
