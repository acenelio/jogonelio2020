using UnityEngine;

namespace NavGame.Core
{
    // CHARACTER EVENTS
    public delegate void OnAttackHitEvent(Vector3 position);
    public delegate void OnDamageTakenEvent(Vector3 strikePoint, int amount);
    public delegate void OnHealthChangedEvent(int maxHealth, int currentHealth);
    public delegate void OnDiedEvent();
}
