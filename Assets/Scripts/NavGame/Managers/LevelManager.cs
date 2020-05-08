using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Core;
using NavGame.Models;

namespace NavGame.Managers
{
    public abstract class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public Action[] actions;
        public string errorSound;

        public OnActionSelectEvent onActionSelect;
        public OnActionCancelEvent onActionCancel;
        public OnActionCooldownUpdateEvent onActionCooldownUpdate;
        public OnResourceUpdateEvent onResourceUpdate;
        public OnReportableErrorEvent onReportableError;
        public OnWaveUpdateEvent onWaveUpdate;
        public OnWaveCountdownEvent onWaveCountdown;

        protected int selectedAction = -1;
        protected LevelData levelData = new LevelData();

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

        protected virtual void Start()
        {
            StartCoroutine(SpawnBad());
        }

        public virtual void AddResource(int amount)
        {
            levelData.AddCoins(amount);
            if (onResourceUpdate != null)
            {
                onResourceUpdate(levelData.CoinCount);
            }
        }

        public virtual void SelectAction(int actionIndex)
        {
            try
            {
                levelData.ValidateCoinAmount(actions[actionIndex].cost);
                if (actions[actionIndex].coolDown > 0)
                {
                    AudioManager.instance.Play(errorSound, PlayerManager.instance.GetPlayer().transform.position);
                    return;
                }
                CancelAction();
                selectedAction = actionIndex;
                if (onActionSelect != null)
                {
                    onActionSelect(actionIndex);
                }
            }
            catch (InvalidOperationException e)
            {
                AudioManager.instance.Play(errorSound, PlayerManager.instance.GetPlayer().transform.position);
                if (onReportableError != null)
                {
                    onReportableError(e.Message);
                }
            }
        }

        public virtual void DoAction(Vector3 point)
        {
            try
            {
                levelData.ConsumeCoins(actions[selectedAction].cost);
                Instantiate(actions[selectedAction].prefab, point, Quaternion.identity);
                if (onResourceUpdate != null)
                {
                    onResourceUpdate(levelData.CoinCount);
                }
                int index = selectedAction;
                selectedAction = -1;
                StartCoroutine(ProcessCooldown(index));
            }
            catch (InvalidOperationException e)
            {
                AudioManager.instance.Play(errorSound, PlayerManager.instance.GetPlayer().transform.position);
                if (onReportableError != null)
                {
                    onReportableError(e.Message);
                }
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

        IEnumerator ProcessCooldown(int actionIndex)
        {
            Action action = actions[actionIndex];
            action.coolDown = action.waitTime;
            while (action.coolDown > 0f)
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
