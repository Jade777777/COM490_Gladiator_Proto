using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnablePrefab;
    [SerializeField]
    private int cost;
    [SerializeField]
    private AudioSource purchase;
    [SerializeField]
    private AudioSource purchaseFailure;
    public GameObject GetSpawnablePrefab()
    {
        if (PlayerInteraction.Instance.GetCurrency()>= cost)
        {
            purchase.Play();
            PlayerInteraction.Instance.AddCurrency(-cost);
            Debug.Log("Selected object!");
            return spawnablePrefab;
        }
        else
        {
            purchaseFailure.Play();
            Debug.Log("Can not select object!");
            return null;
        }

    }
}
