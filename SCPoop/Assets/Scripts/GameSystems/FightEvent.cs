using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEvent : MonoBehaviour
{
    public static FightEvent instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public  void PlayersAttack()
    {

    }
}
