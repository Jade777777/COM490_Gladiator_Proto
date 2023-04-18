using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField]
    GameObject spawnable;
    [SerializeField]
    GameObject spawnable2;
    [SerializeField]
    GameObject spawnable3;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    float spawnRange = 5;
    [SerializeField]
    float spawnTime = 4f;

    int wave=0;
    bool spawning = false;


    void Update()
    {
        UpdateWave();
    }
    
    private void UpdateWave()
    {
        if(spawning)
            return; 

        Character[] characters = GetComponentsInChildren<Character>();
        bool waveEnd = true;
        bool playerDead = true;
        foreach (Character c in characters)
        {
            if (c.GetFaction() == Character.Faction.enemy)
            {
                waveEnd = false;
                
            }
            else if (c.GetFaction() == Character.Faction.player)
            {
                playerDead = false;
                reseting = false;
            }
        }

        if (waveEnd && characters.Length > 0)
        {
            StartCoroutine(NewWave());
        }
        if (playerDead && !reseting)
        {
            StartCoroutine(ResetWave());
            Debug.Log("Game Over");
        }
    }










    bool reseting = false;

    //after a delay resets the game to the start state
    IEnumerator ResetWave()
    {
        reseting = true;
        yield return new WaitForSeconds(spawnTime);



        if (reseting)
        {
            wave = 0;

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        reseting = false;
    }

    //after a delay spawns new enemies within a specified range of the spawn point
    IEnumerator NewWave()
    {
       
        spawning = true;
        yield return new WaitForSeconds(spawnTime);
        for (int i = 0; i < wave; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-spawnRange,spawnRange),0, Random.Range(-spawnRange, spawnRange));
            if(wave >5 && Random.value > 0.8f)
            {
                Instantiate(spawnable2, spawnPoint.position + offset, Quaternion.identity, transform);
            }
            else if(wave>10 && Random.value > 0.8f)
            {
                Instantiate(spawnable3, spawnPoint.position + offset, Quaternion.identity, transform);
            }
            else
            {
                Instantiate(spawnable, spawnPoint.position + offset, Quaternion.identity, transform);
            }
            
        }
        wave++;
        spawning = false;
    }
}
