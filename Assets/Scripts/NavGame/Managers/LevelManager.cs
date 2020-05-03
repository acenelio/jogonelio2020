using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Managers
{
    public abstract class LevelManager : MonoBehaviour
    {
        public Action[] actions;

        void Start()
        {
            StartCoroutine(SpawnBad());
        }

        protected abstract IEnumerator SpawnBad();

        [Serializable]
        public class Action
        {
            public int cost;
            public GameObject prefab;
            public float waitTime = 1f;
            public float coolDown;
        }
    }
}
