using System;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 15f;
    [SerializeField] ParticleSystem projectilePartical;

    Transform targetEnemy;

    // Update is called once per frame
    void Update ()
    {
        SetTargetEnemy();
        FireAtEnemy();
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private void FireAtEnemy()
    {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            RangeChecker();
        }
        else
        {
            SetTowerActive(false);
        }
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distanceToA = Vector3.Distance(transform.position, transformA.position);
        var distanceToB = Vector3.Distance(transform.position, transformB.position);

        if(distanceToA < distanceToB)
        {
            return transformA;
        }

        return transformB; 
    }

    private void RangeChecker()
    {
        float rangeToEnemy = Vector3.Distance(targetEnemy.position, transform.position);

        if (rangeToEnemy <= attackRange)
        {
            SetTowerActive(true);
        }
        else
        {
            SetTowerActive(false);
        }
    }

    private void SetTowerActive(bool isActive)
    {
            var emissionModule = projectilePartical.emission;
            emissionModule.enabled = isActive;
    }
}
