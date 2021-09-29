using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverAttack : PlayerAttack
{
	protected float closeRangeDamage = 40f;
	protected float mediumRangeDamage = 30f;
	protected float longRangeDamage = 15f;
	
	public override void WeaponShoot(){

	if(Input.GetMouseButtonDown(0)) {
		
        if(director.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET && 
			director.GetCurrentSelectedWeapon().tag == "Revolver") {

            director.GetCurrentSelectedWeapon().ShootAnimation();

            BulletFired();
        }	
	}	
	}
	public override void ZoomInAndOut(){
		// we are going to aim with our camera on the weapon
			if(director.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {

				// if we press and hold right mouse button
				if(Input.GetMouseButtonDown(1)) {

					zoomCam.Play("ZoomIn");
					crosshair.SetActive(false);
				}

				// when we release the right mouse button click
				if (Input.GetMouseButtonUp(1)) {

					zoomCam.Play("ZoomOut");
					crosshair.SetActive(true);	
				}

			}
	}
	public override void BulletFired(){
		RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {

            if(hit.transform.tag == "Enemy") {

					if(Vector3.Distance(hit.transform.position, transform.position) <= 5f){
					 
						hit.transform.GetComponent<Health>().ApplyDamage(closeRangeDamage); 
					}

					else if (Vector3.Distance(hit.transform.position, transform.position) <= 10f){

						hit.transform.GetComponent<Health>().ApplyDamage(mediumRangeDamage);
					}

					else { //long range

						hit.transform.GetComponent<Health>().ApplyDamage(longRangeDamage);				
					}	
            }
        }
	}
}
