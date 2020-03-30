using UnityEngine;

namespace NavGame.Core
{
    // CHARACTER EVENTS
    public delegate void OnAttackHitEvent(Vector3 position);
    public delegate void OnHealthChangedEvent(int maxHealth, int currentHealth);
    public delegate void OnDiedEvent();
    public delegate void OnDamageTakenEvent(Vector3 strikePoint, int amount);
}
