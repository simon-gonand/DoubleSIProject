using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public static LifeSystem instance;

    public float playerLife;
    public float playerMaxLife;
    public float enemylife;
    public float enemyMaxLife;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        playerLife = playerMaxLife;
        enemylife = enemyMaxLife;
    }

    private void PlayerDeath()
    {
        //go to Death Screen
    }


}
