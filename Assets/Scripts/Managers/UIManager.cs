using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NavGame.Managers;

public class UIManager : MonoBehaviour
{
    public GameObject errorPanel;
    public Text errorText;
    public float errorTime = 1.5f;
    public string errorSound;
    public GameObject[] actionCDPanels;
    
    public Text[] textActionCosts;

    Image[] actionCDImages;

    void Start()
    {
        errorPanel.SetActive(false);
        InitializeUI();
        LevelManager.instance.onActionSelect += OnActionSelect;
        LevelManager.instance.onActionCancel += OnActionCancel;
        LevelManager.instance.onActionCooldownUpdate += OnActionCooldownUpdate;
        LevelManager.instance.onError += OnError;
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

    void OnError(string message)
    {
        StartCoroutine(ShowError(message));
    }

    IEnumerator ShowError(string message)
    {
        float wait = errorTime;
        errorText.text = message;
        errorPanel.SetActive(true);
        AudioManager.instance.Play(errorSound, PlayerManager.instance.GetPlayer().transform.position);
        while (wait > 0) 
        {
            wait -= Time.deltaTime;
            yield return null;
        }
        errorPanel.SetActive(false);
    }
}
