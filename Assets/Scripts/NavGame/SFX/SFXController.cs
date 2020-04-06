using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Managers;

namespace NavGame.Effects
{
    public abstract class SFXController : MonoBehaviour
    {
        protected void PlayEffects(Vector3 point, string sound, GameObject effects, Quaternion rotation)
        {
            AudioManager.instance.Play(sound, point);
            if (effects != null)
            {
                Instantiate(effects, point, rotation);
            }
        }
    }
}
