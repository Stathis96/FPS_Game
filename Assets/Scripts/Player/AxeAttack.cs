using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttack : MonoBehaviour
{
    private Director director;

    void Awake()
    {
        director = GetComponent<Director>();  	
    }
    // Update is called once per frame
    void Update()
    {
        AxeShoot();
    }
	void AxeShoot(){
		if(Input.GetMouseButtonDown(0)) {

			// διαχειριση τσεκουριου
			if(director.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.NONE) {
				director.GetCurrentSelectedWeapon().ShootAnimation();
			}
	}
}}
