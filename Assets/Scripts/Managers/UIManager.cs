using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] cooldownObjects;

    Image[] cooldownImages;

    void Start()
    {
        InitializeUI();
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
}
