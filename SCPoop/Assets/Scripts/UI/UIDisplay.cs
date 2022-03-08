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

    public float timer = 30;
    private float maxTimer = 30;
    private float maxWidth;

    private void Start()
    {
        //maxWidth = timeBarTransform.sizeDelta.x;
        //DisplayTime(timer);
    }

    /*private void Update()
    {
        txtPlayersHP.text = "Life = " + LifeSystem.instance.playerLife;
        txtEnemyHP.text = "Enemy Life = " + LifeSystem.instance.enemylife;
        txtPlayersPower.text = "Combined Power = " + Grid.instance.playerResult;
        txtEnemyPower.text = "Enemy Power = " + Grid.instance.enemyResult;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            //DisplayTime(timer);
            float amount = timer / maxTimer;
            timeBarTransform.sizeDelta = new Vector2(amount * maxWidth, timeBarTransform.sizeDelta.y);
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