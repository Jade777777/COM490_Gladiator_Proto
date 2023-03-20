using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField]
    GameObject spawnable;
    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float spawnRange = 5;

    
    int wave=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Character[] characters = GetComponentsInChildren<Character>();
        bool waveEnd = true;
        bool playerDead = true;
        foreach(Character c in characters)
        {
            if(c.GetFaction() == Character.Faction.enemy)
            {
                waveEnd = false;
                
            }
            else if(c.GetFaction() == Character.Faction.player)
            {
                playerDead = false;
            }
        }
        if(waveEnd && characters.Length > 0)
        {
            NewWave();

        }
        if (playerDead)
        {
            Debug.Log("Game Over");
        }
        
    }
    
    private void NewWave()
    {
        for (int i = 0; i < wave; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-spawnRange,spawnRange),0, Random.Range(-spawnRange, spawnRange));
            Instantiate(spawnable, spawnPoint.position + offset, Quaternion.identity, transform);
        }
        wave++;
    }
}
