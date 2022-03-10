using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    public TextMeshProUGUI txtPlayersPower;
    public TextMeshProUGUI txtEnemyPower;
    public TextMeshProUGUI txtHeal;
    public TextMeshProUGUI txtPlayerTurn;
    public TextMeshProUGUI txtTimer;

    public RectTransform timeBarTransform;

    public RectTransform playerHealthBar;
    public RectTransform enemyHealthBar;

    private float maxTimeWidth;
    private float maxPlayerHPWidth;
    private float maxEnemyHPWidth;

    private void Start()
    {
        maxTimeWidth = timeBarTransform.sizeDelta.x;
        maxPlayerHPWidth = playerHealthBar.sizeDelta.x;
        maxEnemyHPWidth = enemyHealthBar.sizeDelta.x;
        DisplayTime(Timer.instance.timer);
    }

    private void Update()
    {
        txtPlayersPower.text = "Player Power = " + Grid.instance.playerResult;
        txtHeal.text = "Heal = " + Grid.instance.heal;
        txtEnemyPower.text = "Enemy Power = " + Grid.instance.enemyResult;

        playerHealthBar.sizeDelta = new Vector2(LifeSystem.instance.playerLife / LifeSystem.instance.playerMaxLife * maxPlayerHPWidth, playerHealthBar.sizeDelta.y);
        enemyHealthBar.sizeDelta = new Vector2(LifeSystem.instance.enemylife / LifeSystem.instance.enemyMaxLife * maxEnemyHPWidth, enemyHealthBar.sizeDelta.y);

        DisplayTime(Timer.instance.timer);
        float amount = Timer.instance.timer / Timer.instance.maxTimer;
        timeBarTransform.sizeDelta = new Vector2(amount * maxTimeWidth, timeBarTransform.sizeDelta.y);

        if (GameManager.instance.player1Turn)
            txtPlayerTurn.text = "This is Player 1's Turn !";
        else
            txtPlayerTurn.text = "This is Player 2's Turn !";
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(Timer.instance.timer / 60);
        float seconds = Mathf.FloorToInt(Timer.instance.timer % 60);

        txtTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}