using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animator;

	void Awake () 
	{
        animator = GetComponent<Animator>();	
	}
	
    public void Walk(bool walk) {
        animator.SetBool("Walk", walk);
    }

    public void Run(bool run) {
        animator.SetBool("Run", run);
    }

    public void Attack() {
        animator.SetTrigger("Attack");
    }

    public void Dead() {
        animator.SetTrigger("Die");
    }

} 
