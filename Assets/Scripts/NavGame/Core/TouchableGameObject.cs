using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class TouchableGameObject : BasicGameObject
    {
        public float contactRadius = 0.5f;

        public bool IsInTouch(TouchableGameObject other)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            return distance < contactRadius + other.contactRadius;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, contactRadius);
        }
    }
}
