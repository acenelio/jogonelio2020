using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Managers
{
    public abstract class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        public Action[] actions;

        protected Action selectedAction = null;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            StartCoroutine(SpawnBad());
        }

        public virtual void SelectAction(int code)
        {
            Action action = actions[code - 1];
            Debug.Log("Selected: " + action.prefab.name);
            ResetAction();
            selectedAction = action;
        }

        public virtual void DoAction(Vector3 point)
        {
            Debug.Log("Do: " + selectedAction.prefab.name);
            Instantiate(selectedAction.prefab, point, Quaternion.identity);
        }

        public virtual void ResetAction()
        {
            selectedAction = null;
        }

        public bool IsActionSelected()
        {
            return selectedAction != null;
        }
        
        protected abstract IEnumerator SpawnBad();

        [Serializable]
        public class Action
        {
            public int cost;
            public GameObject prefab;
            public float coolDown = 1f;
        }
    }
}
