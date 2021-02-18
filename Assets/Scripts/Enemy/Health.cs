using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{

    private Animations enemy_Anim;
    private NavMeshAgent navAgent;
    private Controller enemy_Controller;

    public float health = 100f;

    public bool is_Player, is_Enemy;

    private bool is_Dead;
    private Audio enemyAudio;

	private Stats player_Stats;

	void Awake () {
	    
        if(is_Enemy) {
            enemy_Anim = GetComponent<Animations>();
            enemy_Controller = GetComponent<Controller>();
            navAgent = GetComponent<NavMeshAgent>();

            // get enemy audio
            enemyAudio = GetComponentInChildren<Audio>();
        }

        if(is_Player) {
            player_Stats = GetComponent<Stats>();
        }

	}

    public void ApplyDamage(float damage) {

        // if we died don't execute the rest of the code
        if (is_Dead)
            return;

        health -= damage;

        if(is_Player) {
            // show the stats(display the health UI value)
            player_Stats.Display_HealthStats(health);
        }

        if(is_Enemy) {
            if(enemy_Controller.Enemy_State == EnemyState.PATROL) {
                enemy_Controller.chase_Distance = 50f;
            }
        }

        if(health <= 0f) {

            PlayerDied();

            is_Dead = true;
        }

    } // apply damage

    void PlayerDied() {

        if(is_Enemy) {

            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();

            StartCoroutine(DeadSound()); // in order to delay the execution of the function!

            // EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied();
        }

        if(is_Player) {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<Controller>().enabled = false; // shutting off all of the enemies Controller script
            }

            // call enemy manager to stop spawning enemis
            EnemyManager.instance.StopSpawning();

            GetComponent<Movement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<Director>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }

        if(tag == "Player") {

            Invoke("RestartGame", 3f);

        } else {

            Invoke("TurnOffGameObject", 3f);

        }

    } // player died

    void RestartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Example");
    }

    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }
    IEnumerator DeadSound() {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }

}
