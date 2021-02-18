using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;
	
    private CharacterController character_Controller;

    [HideInInspector]//we need them to be public but i dont want them to be visible in inspector pannel
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

	void Awake () {
        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>(); // getting the charcontroller from the top parent
	}
	
	void Update () {
        CheckToPlayFootstepSound();	
	}

    void CheckToPlayFootstepSound() {

        // if we are NOT on the ground
        if (!character_Controller.isGrounded) 
            return;
            

        if(character_Controller.velocity.sqrMagnitude > 0) { //if we re moving

            // accumulated distance is the value how far can we go 
            // e.g. make a step or sprint, or move while crouching
            // until we play the footstep sound
            accumulated_Distance += Time.deltaTime;

            if(accumulated_Distance > step_Distance) {

                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumulated_Distance = 0f;

            }

        } else {
            accumulated_Distance = 0f; //reseting the value , so next time the sound of sth isnt heard right away!!
        }

    }

} // class
