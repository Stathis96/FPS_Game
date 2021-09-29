using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;
    public Transform[] zombie_SpawnPoints;
    public float wait_Before_Spawn_Enemies_Time = 10f;

    [SerializeField]
    private GameObject zombie_Prefab;
    [SerializeField]
    private int zombie_Enemy_Count;
    private int initial_Zombie_Count;

    void Awake () 
	{
        MakeInstance();
	}

    void Start() 
	{
        initial_Zombie_Count = zombie_Enemy_Count;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    void SpawnEnemies() {

        int index = 0;
        for (int i = 0; i < zombie_Enemy_Count; i++) {

            Instantiate(zombie_Prefab, zombie_SpawnPoints[index].position, Quaternion.identity);
            index++;
        }

        zombie_Enemy_Count = 0;

    }

    IEnumerator CheckToSpawnEnemies() {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies"); //αδιάκοπη coroutine

    }

    public void EnemyDied() {

            zombie_Enemy_Count++;

            if(zombie_Enemy_Count > initial_Zombie_Count) {
                zombie_Enemy_Count = initial_Zombie_Count;
            }   
    }

    public void StopSpawning() {
        StopCoroutine("CheckToSpawnEnemies");
    }

} 