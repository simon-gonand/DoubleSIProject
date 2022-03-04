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

    private void Update()
    {
        txtPlayersHP.text = "Life = " + LifeSystem.instance.playerLife;
        txtEnemyHP.text = "Enemy Life = " + LifeSystem.instance.enemylife;
        txtPlayersPower.text = "Combined Power = " + Grid.instance.playerResult;
        txtEnemyPower.text = "Enemy Power = " + Grid.instance.enemyResult;

        if (GameManager.instance.player1Turn)
            txtPlayerTurn.text = "This is Player 1's Turn !";
        else
            txtPlayerTurn.text = "This is Player 2's Turn !";
    }
}
