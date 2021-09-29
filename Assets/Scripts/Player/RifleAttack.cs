using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAttack : PlayerAttack
{
	protected float closeRangeDamage = 35f;
	protected float mediumRangeDamage = 25f;
	protected float longRangeDamage = 20f;
	protected float fireRate = 4f;
	protected float nextTimeToFire;

	public override void WeaponShoot(){

	        if(director.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE) {

				// εαν κραταω πατημενο το δεξι κλικ ΚΑΙ εαν Time > nextTimeToFire
				if(Input.GetMouseButton(0) && Time.time > nextTimeToFire) {

					nextTimeToFire = Time.time + 1f / fireRate;

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
