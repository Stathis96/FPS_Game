using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
	public GameObject gameOverMenuUI;
	public GameObject crosshair;

    private Animations animations;
    private Audio audio;
    private Controller controller;
    private NavMeshAgent navMeshAgent;
	private Stats stats;

    public float health = 100f;
    public bool is_Player, is_Enemy;

    private bool is_Dead;

	void Awake () 
	{    
        if(is_Enemy) {
            animations = GetComponent<Animations>();
            controller = GetComponent<Controller>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            audio = GetComponentInChildren<Audio>();
        }

        if(is_Player) {
            stats = GetComponent<Stats>();
        }

	}

    public void ApplyDamage(float damage) {

        if (is_Dead)
            return;

        health -= damage;

        if(is_Player) {
            // εμφάνιση των Stats του Player
            stats.Display_HealthStats(health);
        }

        if(is_Enemy) {
            if(controller.Enemy_State == EnemyState.PATROL) { 
                controller.chase_Distance = 50f; 
            }
        }

        if(health <= 0f) {

            SomeoneDied();

            is_Dead = true;
        }

    } // apply damage

    void SomeoneDied() {

        if(is_Enemy) {

			ScoreManager.instance.AddPoint();
			GameOverMenu.instance.AddPoint2();
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
            controller.enabled = false;

            animations.Dead();

            StartCoroutine(DeadSound()); // για να καθυστερήσει ελαχιστα η εκτέλεση 

            // αναγέννησε κι αλλα ζομπι
            EnemyManager.instance.EnemyDied();
        }

        if(is_Player) {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<Controller>().enabled = false; 
            }

            // σταματημα αναγεννησης 
            EnemyManager.instance.StopSpawning();

            GetComponent<Movement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<Director>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }

        if(tag == "Player") {
			
            Invoke("ShowGameOverMenu", 3f);

        } else {

            Invoke("TurnOffGameObject", 3f);
        }
    } // SomeoneDied

    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }
    IEnumerator DeadSound() {
        yield return new WaitForSeconds(0.3f);
        audio.Play_DeadSound();
    }

    void ShowGameOverMenu() {
		gameOverMenuUI.SetActive(true);
		crosshair.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true ;
    }

}
