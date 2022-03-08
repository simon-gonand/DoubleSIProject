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
    public TextMeshProUGUI txtPlayerTurn;
    public TextMeshProUGUI txtTimer;

    public RectTransform timeBarTransform;

    public RectTransform playerHealthBar;
    public RectTransform enemyHealthBar;

    public float timer = 30;
    private float maxTimer = 30;
    private float maxTimeWidth;
    private float maxPlayerHPWidth;
    private float maxEnemyHPWidth;

    private void Start()
    {
        maxTimeWidth = timeBarTransform.sizeDelta.x;
        maxPlayerHPWidth = playerHealthBar.sizeDelta.x;
        maxEnemyHPWidth = enemyHealthBar.sizeDelta.x;
        DisplayTime(timer);
    }

    /*private void Update()
    {
        txtPlayersPower.text = "Combined Power = " + Grid.instance.playerResult;
        txtEnemyPower.text = "Enemy Power = " + Grid.instance.enemyResult;

        playerHealthBar.sizeDelta = new Vector2(LifeSystem.instance.playerLife / LifeSystem.instance.playerMaxLife * maxPlayerHPWidth, playerHealthBar.sizeDelta.y);
        enemyHealthBar.sizeDelta = new Vector2(LifeSystem.instance.enemylife / LifeSystem.instance.enemyMaxLife * maxEnemyHPWidth, enemyHealthBar.sizeDelta.y);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            //DisplayTime(timer);
            float amount = timer / maxTimer;
            timeBarTransform.sizeDelta = new Vector2(amount * maxTimeWidth, timeBarTransform.sizeDelta.y);
        }

        if (GameManager.instance.player1Turn)
            txtPlayerTurn.text = "This is Player 1's Turn !";
        else
            txtPlayerTurn.text = "This is Player 2's Turn !";
    }*/

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        txtTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}