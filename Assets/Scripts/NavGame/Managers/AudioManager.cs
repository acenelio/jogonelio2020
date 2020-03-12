using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public Clip[] clips;

        Dictionary<string, AudioClip> dict = new Dictionary<string, AudioClip>();

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

        void Start()
        {
            foreach (Clip clip in clips)
            {
                dict.Add(clip.name, clip.audio);
            }
        }

        public void Play(string clipName, Vector3 position)
        {
            if (dict.ContainsKey(clipName))
            {
                AudioSource.PlayClipAtPoint(dict[clipName], position);
            }
        }

        [Serializable]
        public class Clip
        {
            public string name;
            public AudioClip audio;
        }
    }
}
