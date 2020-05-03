using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NavGame.Managers;

public class UIManager : MonoBehaviour
{
    public GameObject[] cooldownObjects;

    Image[] cooldownImages;

    void Start()
    {
        InitializeUI();
        LevelManager.instance.onActionSelect += OnActionSelect;
        LevelManager.instance.onActionCancel += OnActionCancel;
    }

    void InitializeUI()
    {
        cooldownImages = new Image[cooldownObjects.Length];
        for (int i = 0; i < cooldownObjects.Length; i++)
        {
            cooldownImages[i] = cooldownObjects[i].GetComponent<Image>();
            cooldownImages[i].fillAmount = 0f;
        }
    }

    void OnActionSelect(int actionIndex)
    {
        cooldownImages[actionIndex].fillAmount = 1f;
    }

    void OnActionCancel(int actionIndex)
    {
        cooldownImages[actionIndex].fillAmount = 0f;
    }
}
