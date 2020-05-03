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
        public OnActionCooldownUpdateEvent onActionCooldownUpdate;

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
            Debug.Log("Selected: " + actions[actionIndex].prefab.name);

            CancelAction();

            selectedAction = actionIndex;

            if (onActionSelect != null)
            {
                onActionSelect(actionIndex);
            }
        }

        public virtual void DoAction(Vector3 point)
        {
            Debug.Log("Do: " + actions[selectedAction].prefab.name);
            Instantiate(actions[selectedAction].prefab, point, Quaternion.identity);
            int actionIndex = selectedAction;
            selectedAction = -1;
            StartCoroutine(ProcessCoolDown(actionIndex));
        }

        IEnumerator ProcessCoolDown(int actionIndex)
        {
            Action action = actions[actionIndex];
            action.coolDown = action.waitTime;
            while (action.coolDown > 0)
            {
                if (onActionCooldownUpdate != null)
                {
                    onActionCooldownUpdate(actionIndex, action.coolDown, action.waitTime);
                }
                yield return null;
                action.coolDown -= Time.deltaTime;
            }
            action.coolDown = 0f;
            if (onActionCooldownUpdate != null)
            {
                onActionCooldownUpdate(actionIndex, action.coolDown, action.waitTime);
            }
        }

        public virtual void CancelAction()
        {
            if (selectedAction != -1) 
            {
                if (onActionCancel != null)
                {
                    onActionCancel(selectedAction);
                }
                selectedAction = -1;
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
