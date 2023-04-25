using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    Transform rightHand;

    [SerializeField]
    GameObject spawnablePrefab;

    [SerializeField] 
    GameObject spawnableContainer;

    [SerializeField]
    TMP_Text currencyValue;

    [SerializeField]
    AudioSource addCurrency;

    [SerializeField]
    int currency = 7;

    [HideInInspector]
    public static PlayerInteraction Instance;

    public void Awake()
    {
        Instance = this;
        currencyValue.text = currency.ToString();
    }

    public void AddCurrency(int amount)
    {
        addCurrency.Play();
        currency += amount;
        currencyValue.text = currency.ToString();
        Debug.Log("Added " + amount + " to currency");
   
    }
    public int GetCurrency()
    {
        return currency;
    }




    public void OnFire(InputValue value)
    {

        if (spawnablePrefab != null &&
            Physics.Raycast(rightHand.position, rightHand.forward, out RaycastHit hitInfo, float.PositiveInfinity, 1<<LayerMask.NameToLayer("Spawn")) )
        {
            GameObject newGO = Instantiate(spawnablePrefab,hitInfo.point,Quaternion.identity,spawnableContainer.transform);
            //newGO.transform.position = hitInfo.point;
            Debug.Log("Succesfuly Placed Item!");
            spawnablePrefab = null;
        }
        else
        {
            Debug.Log("Failed to place item!");
        }
    }
    public void OnGrab(InputValue value)
    {
        if( Physics.Raycast(rightHand.position, rightHand.forward, out RaycastHit hitInfo, float.PositiveInfinity, 1 << LayerMask.NameToLayer("UI"))
            && hitInfo.transform.TryGetComponent(out Interactable selection) )
        {
            spawnablePrefab = selection.GetSpawnablePrefab();
        }
        Debug.Log("Test");
        Debug.Log(rightHand.position);
    }

}
