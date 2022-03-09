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

    public void CheckDeath()
    {
        if (playerLife <= 0)
        {
            PlayerDeath();
        }
        else if (enemylife <= 0)
        {
            EnemyDeath();
        }
    }

    private void PlayerDeath()
    {
        SceneManager.LoadScene("Death Screen");
    }

    private void EnemyDeath()
    {
        WaveClearDisplay.instance.Display();
    }
}
