using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] actionCDPanels;

    Image[] actionCDImages;

    void Start()
    {
        InitializeUI();        
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
}
