using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Core;

namespace NavGame.Managers
{
    public abstract class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        public Action[] actions;

        public OnActionSelectEvent onActionSelect;
        public OnActionCancelEvent onActionCancel;


        protected int selectedAction = -1;

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

        public virtual void SelectAction(int actionIndex)
        {
            CancelAction();
            selectedAction = actionIndex;
            if (onActionSelect != null)
            {
                onActionSelect(actionIndex);
            }
        }

        public virtual void DoAction(Vector3 point)
        {
            Instantiate(actions[selectedAction].prefab, point, Quaternion.identity);
            int index = selectedAction;
            selectedAction = -1;
            if (onActionCancel != null)
            {
                onActionCancel(index);
            }
        }

        public virtual void CancelAction()
        {
            if (selectedAction != -1)
            {
                int index = selectedAction;
                selectedAction = -1;
                if (onActionCancel != null)
                {
                    onActionCancel(index);
                }
            }
        }

        public bool IsActionSelected()
        {
            return selectedAction != -1;
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
