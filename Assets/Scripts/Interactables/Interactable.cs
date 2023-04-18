using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnablePrefab;
    [SerializeField]
    private int cost;
    public GameObject GetSpawnablePrefab()
    {
        if (PlayerInteraction.Instance.GetCurrency()>= cost)
        {
            PlayerInteraction.Instance.AddCurrency(-cost);
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
