using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {


    [SerializeField] int playerHealth = 10;
    [SerializeField] int enemyHitDamage = 2;


    private void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(playerHealth <= 0)
        {
            print("You Died!!!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseDamaged();
    }

    private void BaseDamaged()
    {
            playerHealth = playerHealth - enemyHitDamage;   
    }
}
