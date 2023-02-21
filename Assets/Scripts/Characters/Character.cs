using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Character : MonoBehaviour
{
    Animator animator;
    Transform parent;
    CharacterController cc;
    [SerializeField]
    float attackRange = 1.5f;
    [SerializeField]
    float moveSpeed = 2;
    [SerializeField]
    faction f = faction.enemy;

    private enum faction { player, enemy }
    void Start()
    {
        animator = GetComponent<Animator>();
        cc= GetComponent<CharacterController>();
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        Character target = null;
        float closest = Mathf.Infinity;

        foreach(Character c in parent.GetComponentsInChildren<Character>()) 
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
       
        if(target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            if (targetDir.magnitude > attackRange)
            {
                animator.SetBool("Attack", false);
                cc.Move(targetDir.normalized * moveSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Attack", true);
            }
        }
    }

}
