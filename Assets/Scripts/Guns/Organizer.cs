using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim {
    NONE,
    AIM
}

public enum WeaponFireType {
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType {
    BULLET,
    NONE
}

public class Organizer : MonoBehaviour
{
    public WeaponAim weapon_Aim; //τροπος στοχευσης οπλου--τσεκουρι μονο none. 
    public WeaponFireType fireType;//τροπος πυροβολισμου--1 σφαιρα ή πολλες 
    public WeaponBulletType bulletType;//τυπος σφαιρας
    public GameObject attack_Point;//για το τσεκουρι μονο , ανιχνευει την συγκρουση με αντιπαλο 

    private Animator animator;
    [SerializeField]
    private GameObject muzzleFlash; // για shotgun και revolver 
    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    void Awake() 
	{
        animator = GetComponent<Animator>();
    }

    public void ShootAnimation() {
        animator.SetTrigger("Shoot");
    }

    void Play_ShootSound() {
        shootSound.Play();
    }

    void Play_ReloadSound() {
        reload_Sound.Play();
    }

    void Turn_On_AttackPoint() {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint() {
        if(attack_Point.activeInHierarchy) {
            attack_Point.SetActive(false);
        }
    }

    void Turn_On_MuzzleFlash() {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash() {
        muzzleFlash.SetActive(false);
    }
} 
