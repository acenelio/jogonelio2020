using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NavGame.Managers;

public class UIManager : MonoBehaviour
{
    public GameObject[] actionCDPanels;

    Image[] actionCDImages;

    void Start()
    {
        InitializeUI();
        LevelManager.instance.onActionSelect += OnActionSelect;
        LevelManager.instance.onActionReset += OnActionReset;
    }

    void InitializeUI()
    {
        actionCDImages = new Image[actionCDPanels.Length];
        for (int i = 0; i < actionCDPanels.Length; i++)
        {
            actionCDImages[i] = actionCDPanels[i].GetComponent<Image>();
            actionCDImages[i].fillAmount = 0f;
        }
    }

    void OnActionSelect(int code)
    {
        actionCDImages[code - 1].fillAmount = 1f;
    }
    
    void OnActionReset()
    {
        foreach (Image img in actionCDImages)
        {
            img.fillAmount = 0f;
        }
    }
}
