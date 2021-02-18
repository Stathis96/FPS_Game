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
    private Animator anim;

    public WeaponAim weapon_Aim; //determine whether the weapon can aim on its own or not

    [SerializeField]
    private GameObject muzzleFlash; // for shotgun and revolver 

    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attack_Point;//for axe determine whether we have collided with enemy

    void Awake() {
        anim = GetComponent<Animator>();
    }

    public void ShootAnimation() {
        anim.SetTrigger("Shoot");
    }

    public void Aim(bool canAim) {
        anim.SetBool("Aim", canAim);
    }

    void Turn_On_MuzzleFlash() {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash() {
        muzzleFlash.SetActive(false);
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

} // class
