using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NavGame.Core
{
    [RequireComponent(typeof(DamageableGameObject))]
    public class HealthUIController : MonoBehaviour
    {
        public GameObject healthUIPrefab;
        public Transform healthPosition;

        GameObject healthUI;
        Image healthSlider;
        Transform cam;

        DamageableGameObject damageable;

        void Awake() // referencias e inscricoes em eventos
        {
            Canvas canvas = FindWorldSpaceCanvas();
            if (canvas == null)
            {
                throw new Exception("A WorldSpace Canvas is needed to use HealthUIController");
            }
            cam = Camera.main.transform;
            healthUI = Instantiate(healthUIPrefab, canvas.transform);
            healthSlider = healthUI.transform.GetChild(0).GetComponent<Image>();

            damageable = GetComponent<DamageableGameObject>();
            damageable.onHealthChanged += UpdateHealthUI;
            damageable.onDied += DestroyUI;
        }

        void LateUpdate()
        {
            if (healthUI != null)
            {
                healthUI.transform.position = healthPosition.position;
                healthUI.transform.forward = -cam.forward;
            }
        }

        void UpdateHealthUI(int maxHealth, int currentHealth)
        {
            if (healthUI != null)
            {
                float healthPercent = (float)damageable.currentHealth / maxHealth;
                healthSlider.fillAmount = healthPercent;
            }
        }

        void DestroyUI()
        {
            if (healthUI != null) 
            {
                Destroy(healthUI);
            }
        }

        Canvas FindWorldSpaceCanvas()
        {
            foreach (Canvas c in FindObjectsOfType<Canvas>())
            {
                if (c.renderMode == RenderMode.WorldSpace)
                {
                    return c;
                }
            }
            return null;
        }
    }
}
