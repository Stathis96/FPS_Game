using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;

    [SerializeField]
    private GameObject zombie_Prefab;

    public Transform[] zombie_SpawnPoints;

    [SerializeField]
    private int zombie_Enemy_Count;

    private int initial_Zombie_Count;

    public float wait_Before_Spawn_Enemies_Time = 10f;

    void Awake () {
        MakeInstance();
	}

    void Start() {
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

            if (index >= zombie_SpawnPoints.Length) {
                index = 0;
            }

            Instantiate(zombie_Prefab, zombie_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        zombie_Enemy_Count = 0;

    }

    IEnumerator CheckToSpawnEnemies() {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies"); //infinite coroutine

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

} // class