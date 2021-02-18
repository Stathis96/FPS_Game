using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
	[SerializeField] //editing size at inspector 
	private Organizer[] weapons;

	private int current_Weapon_Index;


    // Start is called before the first frame update
    void Start()
    {
        current_Weapon_Index = 0;
		weapons[current_Weapon_Index].gameObject.SetActive(true);
    }

	void Update () {

							//no1 on keyboard
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            TurnOnSelectedWeapon(0);
        }
							//no2 on keyboard
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            TurnOnSelectedWeapon(1);
        }
							//no3 on keyboard
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            TurnOnSelectedWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            TurnOnSelectedWeapon(3);
        }

    } // update

    void TurnOnSelectedWeapon(int weaponIndex) {

		//preventing to REreload the same weapon twice !
        if (current_Weapon_Index == weaponIndex) 
            return;

        // turn off the current weapon
        weapons[current_Weapon_Index].gameObject.SetActive(false);

        // turn on the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);

        // store the current selected weapon index
        current_Weapon_Index = weaponIndex;

    }

    public Organizer GetCurrentSelectedWeapon() {
        return weapons[current_Weapon_Index];
    }

} // class
