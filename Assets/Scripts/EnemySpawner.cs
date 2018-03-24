using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Range(0.1f,10f)]
    [SerializeField] float secondsBetweenSpawns = 5f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParrentTransform;

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnEnemies());
	}

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnemyMovement enemy =Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = enemyParrentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }

    }
}
