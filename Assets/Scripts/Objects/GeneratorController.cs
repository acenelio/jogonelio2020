using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Managers;

public class GeneratorController : MonoBehaviour
{
    public GameObject prefab;

    public float firstDelay = 5f;
    public float delay = 10f;
    public string generateSound;

    void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        yield return new WaitForSeconds(firstDelay);
        while (true)
        {
            float offsetx = Random.Range(-0.2f, 0.2f);
            float offsetz = Random.Range(-0.2f, 0.2f);
            Vector3 pos = new Vector3(transform.position.x + offsetx, transform.position.y, transform.position.z + offsetz);
            Instantiate(prefab, pos, Quaternion.identity);
            AudioManager.instance.Play(generateSound, transform.position);
            yield return new WaitForSeconds(delay);
        }
    }
}
