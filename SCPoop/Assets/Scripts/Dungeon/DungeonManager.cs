using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public List<WavePreset> waves;

    public static DungeonManager instance;

    private int actualWave = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            actualWave = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NextWave();
    }

    public void NextWave()
    {
        if (actualWave >= waves.Count)
        {
            Debug.Log("Victory !");
            // Display Victory Screen
            return;
        }

        Carousel.instance.ChangeBoard(waves[actualWave].cards);
        LifeSystem.instance.enemylife = waves[actualWave].healthPoints;
        LifeSystem.instance.enemyMaxLife = waves[actualWave].healthPoints;
        ++actualWave;

        Timer.instance.LaunchTimer();
    }
}
