using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnablePrefab;

    public GameObject GetSpawnablePrefab()
    {
        if (true)
        {
            Debug.Log("Selected object!");
            return spawnablePrefab;
        }
        else
        {
            Debug.Log("Can not select object!");
            return null;
        }

    }
}
