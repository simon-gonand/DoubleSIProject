using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    public TextMeshProUGUI txtPlayersHP;
    public TextMeshProUGUI txtEnemyHP;
    public TextMeshProUGUI txtPlayersPower;
    public TextMeshProUGUI txtEnemyPower;

    public LifeSystem lifeSystem;

    private float currentPlayersHP;
    private float currentEnemyHP;
    private float playersPower;
    private float enemyPower;

    private void Start()
    {
        currentPlayersHP = LifeSystem.instance.playerLife;
        currentEnemyHP = LifeSystem.instance.enemylife;
        playersPower = Grid.instance.playerResult;
        enemyPower = Grid.instance.enemyResult;

        txtPlayersHP.text = "Life = " + currentPlayersHP;
        txtEnemyHP.text = "Enemy Life = " + currentEnemyHP;
        txtPlayersPower.text = "Combined Power = " + playersPower;
        txtEnemyPower.text = "Enemy Power = " + enemyPower;
    }

    private void Update()
    {
        currentPlayersHP = LifeSystem.instance.playerLife;
        currentEnemyHP = LifeSystem.instance.enemylife;
        playersPower = Grid.instance.playerResult;
        enemyPower = Grid.instance.enemyResult;
    }
}
