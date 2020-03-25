using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class BasicGameObject : MonoBehaviour
    {
        public float angularSpeed = 6f;

        public void FaceObjectFrame(Transform destination)
        {
            Vector3 direction = (destination.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angularSpeed);
        }

        public void FaceObject(Transform destination)
        {
            Vector3 direction = (destination.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        }
    }
}
