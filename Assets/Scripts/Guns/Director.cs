using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
	[SerializeField] //editing size at inspector 
	private Organizer[] weapons;

	private int current_Weapon;

    void Start()
    {
        current_Weapon = 0;
		weapons[current_Weapon].gameObject.SetActive(true);
    }

	void Update () 
	{			
							//no1 στο πληκτρολογιο
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            TurnOnSelectedWeapon(0);
        }
							//no2 στο πληκτρολογιο
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            TurnOnSelectedWeapon(1);
        }
							//no3 στο πληκτρολογιο
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            TurnOnSelectedWeapon(2);
        }
							//no4 στο πληκτρολογιο
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            TurnOnSelectedWeapon(3);
        }

    } 

    void TurnOnSelectedWeapon(int weaponIndex) {

		//αποτρέπει να ΕΠΑΝΑφορτωθει το ιδιο οπλο 
        if (current_Weapon == weaponIndex) 
            return;

        // απενεργοποιηση τρεχοντος οπλου
        weapons[current_Weapon].gameObject.SetActive(false);

        // ενεργοποιηση επιλεγμενου οπλου 
        weapons[weaponIndex].gameObject.SetActive(true);

        // αποθηκευση τρεχουσας τιμης 
        current_Weapon = weaponIndex;

    }

    public Organizer GetCurrentSelectedWeapon() {
        return weapons[current_Weapon];
    }

} 
