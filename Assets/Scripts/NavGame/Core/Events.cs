using UnityEngine;

namespace NavGame.Core
{
    // CHARACTER EVENTS
    public delegate void OnAttackCastEvent(Vector3 castPoint);
    public delegate void OnAttackStrikeEvent(Vector3 strikePoint);
    public delegate void OnAttackStartEvent();
    public delegate void OnHealthChangedEvent(int maxHealth, int currentHealth);
    public delegate void OnDiedEvent();
    public delegate void OnDamageTakenEvent(Vector3 strikePoint, int amount);
}
