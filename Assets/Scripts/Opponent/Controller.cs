using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    PATROL,
    CHASE,
    ATTACK
}

public class Controller : MonoBehaviour
{

    private Animations animations;
    private NavMeshAgent navMeshAgent;
    private EnemyState enemy_State;
    private Transform player;
    private Audio audio;
    public GameObject attack_Point;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    public float chase_Distance = 20f;
    public float attack_Distance = 2f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    public float wait_Before_Attack = 2f;

    private float current_Chase_Distance;
    private float patrol_Timer;
    private float attack_Timer;

    void Awake() 
	{
        animations = GetComponent<Animations>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        audio = GetComponentInChildren<Audio>();
    }

    void Start () 
	{
        enemy_State = EnemyState.PATROL;
        patrol_Timer = patrol_For_This_Time;

        // when the enemy first gets to the player,attack right away
        attack_Timer = wait_Before_Attack;

        // memorize the value of chase distance,so that we can put it back
        current_Chase_Distance = chase_Distance;

	}

	void Update () 
	{
		
        if(enemy_State == EnemyState.PATROL) {
            Patrol();
        }

        if(enemy_State == EnemyState.CHASE) {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK) {
            Attack();
        }

    }

    public EnemyState Enemy_State {
        get; set;
    }

    void Turn_On_AttackPoint() {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint() {
        if (attack_Point.activeInHierarchy) {
            attack_Point.SetActive(false);
        }
    }

	void Patrol() {

        // enable the agent to move again
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walk_Speed;

        // add to the patrol timer
        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_For_This_Time) {

            SetNewRandomDestination();

            patrol_Timer = 0f; //resetin to be able to use again 

        }

        if(navMeshAgent.velocity.sqrMagnitude > 0) { // if moving 
        
            animations.Walk(true);
        
        } else {

            animations.Walk(false);

        }

        // test the distance between the player and the enemy
        if(Vector3.Distance(transform.position, player.position) <= chase_Distance) {

            animations.Walk(false);

            enemy_State = EnemyState.CHASE;

            // play spotted audio
            audio.Play_ScreamSound();

        }


    } // patrol

    void Chase() {

        // enable the agent to move again
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = run_Speed;

        // set the player's position as the destination,because we are chasing(running towards) the player
        navMeshAgent.SetDestination(player.position);

        if (navMeshAgent.velocity.sqrMagnitude > 0) {

            animations.Run(true);

        } else {

            animations.Run(false);

        }

        // if the distance between enemy and player is less than the attack distance
        if(Vector3.Distance(transform.position, player.position) <= attack_Distance) {

            // stop the animations
            animations.Run(false);
            animations.Walk(false);
            enemy_State = EnemyState.ATTACK;

            // reset the chase distance to previous
            if(chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            }

        } else if(Vector3.Distance(transform.position, player.position) > chase_Distance) {
            // player run away from enemy

            // stop running
            animations.Run(false);

            enemy_State = EnemyState.PATROL;

            // reset the patrol timer so that the function,can calculate the new patrol destination right away
            patrol_Timer = patrol_For_This_Time;

            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            }
        } 

    } // chase

    void Attack() {
		
		//σταμάτημα της κίνησης του Ζομπι 
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;

        attack_Timer += Time.deltaTime;
		//ελεγχος χρονου επιθεσης 
        if(attack_Timer > wait_Before_Attack) {

            animations.Attack();

            attack_Timer = 0f;

            audio.Play_AttackSound();

        }
		//εαν ο παικτης τρεξει 
        if(Vector3.Distance(transform.position, player.position) >
           attack_Distance + chase_After_Attack_Distance) {

            enemy_State = EnemyState.CHASE;

        }

    } // attack

    void SetNewRandomDestination() {

        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1); // ελεγχει την τιμη του randDir για να δει αν ειναι μεσα στην navigationable περιοχη, 
																	// αν ειναι εκτος, βρισκει νεα τιμη απο το rand_Radius και θα το αποθηκευσει στην τιμη navHit,το -1 = ολα τα layers.
        navMeshAgent.SetDestination(navHit.position);

    }
	
} 
