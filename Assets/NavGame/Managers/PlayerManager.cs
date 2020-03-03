using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        void Awake()
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

        public GameObject GetPlayer()
        {
            return GameObject.FindGameObjectWithTag("Player");
        }
    }
}