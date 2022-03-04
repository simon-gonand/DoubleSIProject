using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        playerLife = playerMaxLife;
        enemylife = enemyMaxLife;
    }

    private void Update()
    {
        if (playerLife <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("T'es mort Teuteu");
        SceneManager.LoadScene("Death Screen");
    }

}
