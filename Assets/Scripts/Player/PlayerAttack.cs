using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	
    private Director weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    void Awake()
    {
        weapon_Manager = GetComponent<Director>();  
        zoomCameraAnim = GameObject.FindWithTag("FP Camera").GetComponent<Animator>();

        crosshair = GameObject.FindWithTag("Crosshair");

        mainCam = Camera.main;		

    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
		ZoomInAndOut();
    }

    void WeaponShoot() {

        // if we have assault riffle
        if(weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE) {

            // if we press and hold left mouse click AND
            // if Time is greater than the nextTimeToFire
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire) {

                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();

            }

            // if we have a regular weapon that shoots once
        } else {

            if(Input.GetMouseButtonDown(0)) {

                // handle axe
                if(weapon_Manager.GetCurrentSelectedWeapon().tag == "Axe") {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                // handle shoot
                if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET) {

                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();

                }
            } // if input get mouse button 0

        } // else

    } // weapon shoot

    void ZoomInAndOut() {

        // we are going to aim with our camera on the weapon
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {

            // if we press and hold right mouse button
            if(Input.GetMouseButtonDown(1)) {

                zoomCameraAnim.Play("ZoomIn");

                crosshair.SetActive(false);
            }

            // when we release the right mouse button click
            if (Input.GetMouseButtonUp(1)) {

                zoomCameraAnim.Play("ZoomOut");

                crosshair.SetActive(true);	
            }

        } // if we need to zoom the weapon
    } // zoom in and out

    void BulletFired() {

        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {

            if(hit.transform.tag == "Enemy") {
                hit.transform.GetComponent<Health>().ApplyDamage(damage);
            }
			print("we hit"+ hit.transform.gameObject.name);
        }

    } // bullet fired

}
