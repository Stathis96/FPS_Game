using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 2f;
    public float range = 1f;
    public LayerMask layerMask;
	
	void Update () 
	{
        Collider[] strikes = Physics.OverlapSphere(transform.position, range, layerMask);
			if(strikes.Length > 0) {
				strikes[0].gameObject.GetComponent<Health>().ApplyDamage(damage); 
			    gameObject.SetActive(false); // ανιχνευση ζημιας μονο μια φορα και απενεργοποιηση του attackPoint.
			}
	}
} 
