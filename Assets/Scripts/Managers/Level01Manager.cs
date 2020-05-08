using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Managers;

public class Level01Manager : LevelManager
{
    public Transform[] badSpawn;
    public GameObject badPrefab;
    public int badWaves = 3;
    public int monstersPerWave = 2;
    public float waitTimeFirstWave = 2f;
    public float waitTimeBetweenWaves = 4f;

    protected override void Start()
    {
        base.Start();
        if (onWaveUpdate != null)
        {
            onWaveUpdate(badWaves, 0);
        }
    }

    protected override IEnumerator SpawnBad()
    {
        yield return new WaitForSeconds(waitTimeFirstWave);
        for (int i = 0; i < badWaves; i++)
        {
            for (int j = 0; j < badSpawn.Length; j++)
            {
                for (int k = 0; k < monstersPerWave; k++)
                {
                    Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), 0f, Random.Range(-0.1f, 0.1f));
                    Instantiate(badPrefab, badSpawn[j].position + offset, Quaternion.identity);
                }
            }
            if (onWaveUpdate != null)
            {
                onWaveUpdate(badWaves, i + 1);
            }
            yield return new WaitForSeconds(waitTimeBetweenWaves);
        }
    }
}
