using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public Clip[] clips;



        [Serializable]
        public class Clip
        {
            public string name;
            public AudioClip audio;
        }
    }
}
