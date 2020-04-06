using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Misc
{
    public class DestroyWithDelay : MonoBehaviour
    {
        public float delay = 2f;

        void Start()
        {
            Destroy(gameObject, delay);
        }
    }
}
