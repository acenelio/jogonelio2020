using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Managers;

public class Level01Manager : LevelManager
{
    public Transform[] badSpawn;
    public GameObject badPrefab;
    public int badWaves = 3;
    public float waitTimeFirstWave = 2f;
    public float waitTimeBetweenWaves = 4f;

    protected override IEnumerator SpawnBad()
    {
        yield return new WaitForSeconds(waitTimeFirstWave);
        for (int i = 0; i < badWaves; i++)
        {
            for (int j = 0; j < badSpawn.Length; j++)
            {
                Instantiate(badPrefab, badSpawn[j].position, Quaternion.identity);
            }
            yield return new WaitForSeconds(waitTimeBetweenWaves);
        }
    }
}
