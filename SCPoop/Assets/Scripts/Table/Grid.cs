using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    public List<Transform> slots;

    private Card[][] _cards = new Card[3][];
    public Card[][] cards { get { return _cards; } }

    public float playerResult = 0;
    public float enemyResult = 0;

    public Card[] debugEnemyHand;

    public GameObject electroLinkVerticalEnemy;
    public GameObject electroLinkDiagonalEnemy;
    public GameObject electroLinkHorizontalEnemy;
    public float depthlink = 0.1f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; ++i)
        {
            cards[i] = new Card[3];
        }

        cards[0][0] = debugEnemyHand[0];
        cards[1][0] = debugEnemyHand[1];
        cards[2][0] = debugEnemyHand[2];

        CalculatePower();
        InitEnemyTrails();
    }

    public void InitEnemyTrails()
    {
        for (int i = 0; i < debugEnemyHand.Length; ++i)
        {


            for (int j = 0; j <= 7; ++j)
            {
                int mask = 1 << j;
                if ((mask & debugEnemyHand[i].stats.direction) == mask)
                {
                    switch (j)
                    {

                        case 1:
                            GameObject localLinkDown = Instantiate(electroLinkVerticalEnemy, debugEnemyHand[i].gameObject.transform, false);
                            localLinkDown.transform.localPosition = new Vector3(0, depthlink, -14);

                            break;
                        case 2:
                            if (i>=1)
                            {
                                GameObject localLinkLeft = Instantiate(electroLinkHorizontalEnemy, debugEnemyHand[i].gameObject.transform, false);
                                localLinkLeft.transform.localPosition = new Vector3(-11, depthlink, 0);
                            }
                            
                            break;
                        case 3:
                            if (i <= 1)
                            {
                                GameObject localLinkRight = Instantiate(electroLinkHorizontalEnemy, debugEnemyHand[i].gameObject.transform, false);
                                localLinkRight.transform.localPosition = new Vector3(11, depthlink, 0);

                            }

                            break;

                        case 6:
                            if (i <= 1)
                            {
                                GameObject localLinkDownRight = Instantiate(electroLinkHorizontalEnemy, debugEnemyHand[i].gameObject.transform, false);
                                localLinkDownRight.transform.localPosition = new Vector3(11, depthlink, -14);
                                localLinkDownRight.transform.localEulerAngles = new Vector3(0, -28, 0);
                            }

                            break;
                        case 7:
                            if (i >= 1)
                            {
                                GameObject localLinkDownLeft = Instantiate(electroLinkHorizontalEnemy, debugEnemyHand[i].gameObject.transform, false);
                                localLinkDownLeft.transform.localPosition = new Vector3(-11, depthlink, -14);
                                localLinkDownLeft.transform.localEulerAngles = new Vector3(0, -28, 0);
                                    }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    
        
    }

    public void BeginTurn()
    {
        for (int y = 1; y < 3; ++y)
        {
            for (int x = 0; x < 3; ++x)
            {
                if (cards[x][y] == null) continue;
                cards[x][y] = null;
            }
        }

        CalculatePower();
    }

    public bool IsSlotEmpty(Transform t)
    {
        for(int i = 0; i < slots.Count; ++i)
        {
            if (slots[i].position == t.position)
            {
                int y = i / 3;
                int x = i - y * 3;
                if (cards[x][y] == null) return true;
                else return false;
            }
        }
        return true;
    }

    public void AddCard(Card card, int x, int y)
    {
        cards[x][y] = card;
        card.meshSpawner.SpawnMesh();            

        if (CheckSamePowerOnLine(y))
        {
            Debug.Log("BUFF");
        }

        CalculatePower();
    }

    public void CalculatePower()
    {
        for (int x = 0; x < 3; ++x)
        {
            for (int y = 0; y < 3; ++y)
            {
                if (cards[x][y] == null) continue;
                cards[x][y].Init();
            }
        }

        for (int i = 0; i < 8; ++i)
        {
            for (int x = 0; x < 3; ++x)
            {
                for (int y = 0; y < 3; ++y)
                {
                    if (cards[x][y] == null) continue;
                    if (((int)cards[x][y].stats.effect) == i)
                    {
                        if (cards[x][y].stats.direction == 0)
                        {
                            cards[x][y].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                        }
                        else
                        {
                            for (int j = 0; j <= 7; ++j)
                            {
                                int mask = 1 << j;
                                if ((mask & cards[x][y].stats.direction) == mask)
                                {
                                    CheckDirection(x, y, j);
                                }
                            }
                        }
                    }

                }
            }
        }

        playerResult = 0;
        enemyResult = 0;
        float heal = 0;
        bool isFull = true;

        for (int x = 0; x < 3; ++x)
        {
            for (int y = 0; y < 3; ++y)
            {
                if (cards[x][y] == null)
                {
                    isFull = false;
                    continue;
                }
                if (y == 0)
                    enemyResult += cards[x][y].tempPower;
                else
                {
                    if (cards[x][y].tempIsHeal)
                        heal += cards[x][y].tempPower;
                    else
                        playerResult += cards[x][y].tempPower;
                }
            }
        }

        if (isFull)
        {
            Attack(playerResult, enemyResult);
            GameManager.instance.EndTurn();
        }

        /*Debug.Log("Enemy power : " + enemyResult);
        Debug.Log("Player power : " + playerResult);
        Debug.Log("Heal : " + heal);*/
    }

    private void CheckDirection(int x, int y, int j)
    {
        switch (j)
        {
            case 0:
                if (y == 0) return;
                if (cards[x][y - 1] == null) return;
                cards[x][y - 1].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                break;
            case 1:
                if (y == 2) return;
                if (cards[x][y + 1] == null) return;
                cards[x][y + 1].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                break;
            case 2:
                if (x == 0) return;
                if (cards[x - 1][y] == null) return;
                cards[x - 1][y].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                break;
            case 3:
                if (x == 2) return;
                if (cards[x + 1][y] == null) return;
                cards[x + 1][y].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                break;
            case 4:
                if (x == 2) return;
                if (y == 0) return;
                if (cards[x + 1][y - 1] == null) return;
                cards[x + 1][y - 1].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                break;
            case 5:
                if (x == 0) return;
                if (y == 0) return;
                if (cards[x - 1][y - 1] == null) return;
                cards[x - 1][y - 1].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                break;
            case 6:
                if (x == 2) return;
                if (y == 2) return;
                if (cards[x + 1][y + 1] == null) return;
                cards[x + 1][y + 1].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                break;
            case 7:
                if (x == 0) return;
                if (y == 2) return;
                if (cards[x - 1][y + 1] == null) return;
                cards[x - 1][y + 1].ApplyEffect(cards[x][y].stats.effect, cards[x][y].stats.power);
                break;
            default:
                break;
        }
    }

    private void Attack(float playerResult, float enemyResult)
    {
        LifeSystem.instance.enemylife = LifeSystem.instance.enemylife - playerResult;
        if(LifeSystem.instance.enemylife > 0)
        {
            StartCoroutine(FightCoroutine(0.7f, enemyResult));
        }
    }

    IEnumerator FightCoroutine(float time, float enemyResult)
    {
        yield return new WaitForSeconds(time);
        LifeSystem.instance.playerLife = LifeSystem.instance.playerLife - enemyResult;
        Debug.Log(LifeSystem.instance.playerLife);
        Debug.Log(LifeSystem.instance.enemylife);
    }

    public bool CheckSamePowerOnLine(int line)
    {
        if (cards[0][line] == null) return false;
        int power = cards[0][line].stats.power;
        for (int i = 1; i < 3; ++i)
        {
            if (cards[i][line] == null) return false;
            if (cards[i][line].stats.power != power) return false;
        }
        return true;
    }
}
