using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float attackRange = 15f;
    [SerializeField] ParticleSystem projectilePartical;


    // Update is called once per frame
    void Update ()
    {
        objectToPan.LookAt(targetEnemy);
        FireAtEnemy();
    }

    private void FireAtEnemy()
    {
        if (targetEnemy)
        {
            //print("Target found!");
            RangeChecker();
        }
        else
        {
           // print("Target not found!");
            SetTowerActive(false);
        }
    }

    private void RangeChecker()
    {
        float rangeToEnemy = Vector3.Distance(targetEnemy.position, transform.position);

        if (rangeToEnemy <= attackRange)
        {
           // print("Target in range of tower: " + rangeToEnemy);
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
