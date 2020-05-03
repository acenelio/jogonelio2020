using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NavGame.Managers;

public class UIManager : MonoBehaviour
{
    public GameObject[] actionCDPanels;
    
    public Text[] textActionCosts;

    Image[] actionCDImages;

    void Start()
    {
        InitializeUI();
        LevelManager.instance.onActionSelect += OnActionSelect;
        LevelManager.instance.onActionCancel += OnActionCancel;
        LevelManager.instance.onActionCooldownUpdate += OnActionCooldownUpdate;
    }

    void InitializeUI()
    {
        actionCDImages = new Image[actionCDPanels.Length];
        for (int i = 0; i < actionCDPanels.Length; i++)
        {
            actionCDImages[i] = actionCDPanels[i].GetComponent<Image>();
            actionCDImages[i].fillAmount = 0f;

            textActionCosts[i].text = "(" + LevelManager.instance.actions[i].cost + ")";
        }
    }

    void OnActionSelect(int actionIndex)
    {
        actionCDImages[actionIndex].fillAmount = 1f;
    }
    
    void OnActionCancel(int actionIndex)
    {
        actionCDImages[actionIndex].fillAmount = 0f;
    }

    void OnActionCooldownUpdate(int actionIndex, float time, float waitTime)
    {
        float fillPercent = time / waitTime;
        actionCDImages[actionIndex].fillAmount = fillPercent;
    }
}
