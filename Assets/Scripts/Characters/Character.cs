using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Character : MonoBehaviour
{
    Animator animator;
    CharacterController cc;
    [SerializeField]
    float attackRange = 1.5f;
    [SerializeField]
    float moveSpeed = 2;
    [SerializeField]
    Faction f = Faction.enemy;

    [SerializeField]
    float health = 5;

    [SerializeField]
    float dps = 1;
    [SerializeField]
    int currency = 1;

    [SerializeField]
    GameObject marker;

    [SerializeField]
    AudioSource getHit;

    bool isDead = false;
    public enum Faction { player, enemy }
    void Start()
    {
        animator = GetComponent<Animator>();
        cc= GetComponent<CharacterController>();
  
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        else
        {

            Character target = null;
            float closest = Mathf.Infinity;

            foreach (Character c in transform.parent.GetComponentsInChildren<Character>())
            {
                if (f != c.f)
                {
                    float dist = Vector3.Distance(c.transform.position, transform.position);
                    if (c != this && dist < closest)
                    {
                        closest = dist;
                        target = c;
                    }
                }
            }

            if (target != null)
            {
                animator.speed = 1f;
                Vector3 targetDir = target.transform.position - transform.position;
                if (targetDir.magnitude > attackRange)
                {
                    animator.SetBool("Attack", false);
                    transform.LookAt(target.transform.position);
                    
                    cc.SimpleMove(targetDir.normalized * moveSpeed);
                }
                else
                {
                    transform.LookAt(target.transform.position);
                    animator.SetBool("Attack", true);
                    target.Damage(dps*Time.deltaTime);
                }
            }
            else
            {

                cc.SimpleMove(transform.forward * 0.2f*moveSpeed);
                animator.speed = 0.2f;
                animator.SetBool("Attack", false);
            }
        }
    }

    public Faction GetFaction()
    {
        return f;
    }


    public void Damage(float damage)
    {
        
        health -= damage;
        if (health <= 0)
        {
            animator.speed = 1f;
            if(f != Faction.player)
            {
                PlayerInteraction.Instance.AddCurrency(currency);
            }
            animator.SetBool("Dead" , true);
            if(marker!= null)
            {
                Destroy(marker);
            }
            Destroy(cc);
            Destroy(this);
        }
        getHit.Play();
        Debug.Log("Ouch!");
    }
}
