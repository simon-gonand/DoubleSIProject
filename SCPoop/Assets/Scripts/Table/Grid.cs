using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    public List<Transform> slots;

    private Card[][] _cards = new Card[3][];
    public Card[][] cards { get { return _cards; } }

    public float playerResult = 0;
    public float enemyResult = 0;
    public float heal = 0;

    [Header("LinkEnnemies")] 
    public GameObject electroLinkVerticalEnemy;
    public GameObject electroLinkDiagonalEnemy;
    public GameObject electroLinkHorizontalEnemy;
    public float depthlinkEnemy = 0.1f;
    private List  <GameObject> enemyTrail = new List<GameObject>();
    [Header("LinkPlayer")]
    private List<GameObject> playerTrail = new List<GameObject>();
    public GameObject electroLinkDiagonal;
    public GameObject electroLinkHorizontal;
    public GameObject electroLinkVertical;
    public float depthlink = 0.05f;

    [Header("FXAttack")]
    public GameObject playerDamage;
    public float increaseDuration = 1;
    public TextMeshPro playerdamageText;

    [Header("FXWound")]
    public GameObject enemyDamage;
    public TextMeshPro enemydamageText;

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

        CalculatePower();
    }

    public void ChangeEnemyHand(List<Card> card, Transform[] s)
    {
        Destroy(cards[0][0]?.gameObject);
        Destroy(cards[1][0]?.gameObject);
        Destroy(cards[2][0]?.gameObject);

        cards[0][0] = card[0];
        cards[1][0] = card[1];
        cards[2][0] = card[2];

        slots[0] = s[1];
        slots[1] = s[2];
        slots[2] = s[3];

        InitEnemyTrails();
    }

    public void InitEnemyTrails()
    {
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j <= 7; ++j)
            {
                int mask = 1 << j;
                if ((mask & cards[i][0].stats.direction) == mask)
                {
                    switch (j)
                    {
                        case 1:
                            GameObject localLinkDown = Instantiate(electroLinkVerticalEnemy, slots[i].gameObject.transform, false);
                            localLinkDown.transform.localPosition = new Vector3(0, depthlinkEnemy, -0.45f);
                            enemyTrail.Add(localLinkDown);
                            break;
                        case 2:
                            if (i>=1)
                            {
                                GameObject localLinkLeft = Instantiate(electroLinkHorizontalEnemy, slots[i].gameObject.transform, false);
                                localLinkLeft.transform.localPosition = new Vector3(-0.4f, depthlinkEnemy, 0);
                                enemyTrail.Add(localLinkLeft);
                            }
                            
                            break;
                        case 3:
                            if (i <= 1)
                            {
                                GameObject localLinkRight = Instantiate(electroLinkHorizontalEnemy, slots[i].gameObject.transform, false);
                                localLinkRight.transform.localPosition = new Vector3(0.4f, depthlinkEnemy, 0);
                                enemyTrail.Add(localLinkRight);
                            }

                            break;

                        case 6:
                            if (i <= 1)
                            {
                                GameObject localLinkDownRight = Instantiate(electroLinkDiagonalEnemy, slots[i].gameObject.transform, false);
                                localLinkDownRight.transform.localPosition = new Vector3(0.4f, depthlinkEnemy, -0.45f);
                                localLinkDownRight.transform.localEulerAngles = new Vector3(0, -28, 0);
                                enemyTrail.Add(localLinkDownRight);
                            }

                            break;
                        case 7:
                            if (i >= 1)
                            {
                                GameObject localLinkDownLeft = Instantiate(electroLinkDiagonalEnemy, slots[i].gameObject.transform, false);
                                localLinkDownLeft.transform.localPosition = new Vector3(-0.4f, depthlinkEnemy,-0.45f);
                                localLinkDownLeft.transform.localEulerAngles = new Vector3(0, 28, 0);
                                enemyTrail.Add(localLinkDownLeft);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    
        
    }
    public void InitPlayerTrails()
    {
        for (int i = 3; i < 9; ++i)
        {
            Card currentCard;
            int y = i / 3;
            
            if (cards[i - y * 3][y] != null)
            {
                currentCard = cards[i - y * 3][y];

                for (int j = 0; j <= 7; ++j)
                {
                    int mask = 1 << j;
                    if ((mask & currentCard.stats.direction) == mask)
                    {
                        switch (j)
                        {

                            case 0:
                                    GameObject localLinkUp = Instantiate(electroLinkVertical, slots[i].gameObject.transform, false);
                                    localLinkUp.transform.localPosition = new Vector3(0, depthlink, 0.5f);
                                    playerTrail.Add(localLinkUp);
                                break;

                            case 1:
                                if (i != 6 && i != 7 && i != 8)
                                {
                                    Debug.Log("test");
                                    GameObject localLinkDown = Instantiate(electroLinkVertical, slots[i].gameObject.transform, false);
                                    localLinkDown.transform.localPosition = new Vector3(0, depthlink, -0.5f);
                                    playerTrail.Add(localLinkDown);
                                }
                                break;
                            case 2:
                                if (i!=3 && i!=6)
                                {
                                    GameObject localLinkLeft = Instantiate(electroLinkHorizontal, slots[i].gameObject.transform, false);
                                    localLinkLeft.transform.localPosition = new Vector3(-0.4f, depthlink, 0);
                                    playerTrail.Add(localLinkLeft);
                                }

                                break;
                            case 3:
                                if (i != 5 && i != 8)
                                {
                                    GameObject localLinkRight = Instantiate(electroLinkHorizontal, slots[i].gameObject.transform, false);
                                    localLinkRight.transform.localPosition = new Vector3(0.4f, depthlink, 0);
                                    playerTrail.Add(localLinkRight);
                                }

                                break;
                            case 4:
                                if (i != 5 && i != 8)
                                {
                                    GameObject localLinkUpRight = Instantiate(electroLinkDiagonal, slots[i].gameObject.transform, false);
                                    localLinkUpRight.transform.localPosition = new Vector3(0.4f, depthlink, 0.5f);
                                    localLinkUpRight.transform.localEulerAngles = new Vector3(0, 28, 0);
                                    playerTrail.Add(localLinkUpRight);
                                }

                                break;
                            case 5:
                                if (i != 3 && i != 6)
                                {
                                    GameObject localLinkUpLeft = Instantiate(electroLinkDiagonal, slots[i].gameObject.transform, false);
                                    localLinkUpLeft.transform.localPosition = new Vector3(-0.4f, depthlink, 0.5f);
                                    localLinkUpLeft.transform.localEulerAngles = new Vector3(0, -28, 0);
                                    playerTrail.Add(localLinkUpLeft);
                                }
                                break;

                            case 6:
                                if (i != 6 && i != 7 && i != 8 && i != 5 )
                                {
                                    GameObject localLinkDownRight = Instantiate(electroLinkDiagonal, slots[i].gameObject.transform, false);
                                    localLinkDownRight.transform.localPosition = new Vector3(0.45f, depthlink, -0.5f);
                                    localLinkDownRight.transform.localEulerAngles = new Vector3(0, -28, 0);
                                    playerTrail.Add(localLinkDownRight);
                                }

                                break;
                            case 7:
                                if (i != 6 && i != 7 && i != 8 && i != 3)
                                {
                                    GameObject localLinkDownLeft = Instantiate(electroLinkDiagonal, slots[i].gameObject.transform, false);
                                    localLinkDownLeft.transform.localPosition = new Vector3(-0.45f, depthlink, -0.5f);
                                    localLinkDownLeft.transform.localEulerAngles = new Vector3(0, 28, 0);
                                    playerTrail.Add(localLinkDownLeft);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }


            
            }
        }


    }
    public void ClearEnemyTrails()
    {
        for (int i = 0; i < enemyTrail.Count; ++i)
        {
            Destroy(enemyTrail[i]);
        }
        enemyTrail.Clear();
    }
    public void ClearPlayerTrails()
    {
        for (int i = 0; i < playerTrail.Count; ++i)
        {
            Destroy(playerTrail[i]);
        }
        playerTrail.Clear();
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
        ClearPlayerTrails();
        InitPlayerTrails();

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
        heal = 0;
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
                {
                    //Enemy
                    enemyResult += cards[x][y].tempPower;
                    cards[x][y].ActualisePower(true);
                }
                else
                {
                    //player
                    if (cards[x][y].tempIsHeal)
                        heal += cards[x][y].tempPower;
                    else
                        playerResult += cards[x][y].tempPower;

                    cards[x][y].ActualisePower(false);
                }
            }
        }

        if (isFull)
        {
            Attack();
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

    private void Attack()
    {
        Timer.instance.StopTimer();
        playerDamage.SetActive(true);
        playerdamageText.gameObject.SetActive(true);
        StartCoroutine(LerpPower(playerResult,playerdamageText));
        //PlayerDamageAnimation.Play();

    }
    private IEnumerator LerpPower(float value,TextMeshPro text)
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < increaseDuration)
        {
            timeElapsed += Time.deltaTime;
            text.text = ((int)Mathf.Lerp(0, value, timeElapsed / increaseDuration)).ToString();
            yield return null;
        }
    }
    public void EndAttack()
    {
        Debug.Log("fin");
        playerdamageText.gameObject.SetActive(false);
        LifeSystem.instance.enemylife = LifeSystem.instance.enemylife - playerResult;
        playerDamage.SetActive(false);
        if (LifeSystem.instance.enemylife > 0)
        {
            EnemyAttack();
            //StartCoroutine(FightCoroutine(0.7f));
        }
    }
    private void EnemyAttack()
    {

        enemyDamage.SetActive(true);
        enemydamageText.gameObject.SetActive(true);
        StartCoroutine(LerpPower(enemyResult,enemydamageText));
        //PlayerDamageAnimation.Play();

    }
    public void EndEnemyAttack()
    {
        Timer.instance.ResetTime();
        Timer.instance.LaunchTimer();
        enemydamageText.gameObject.SetActive(false);
        LifeSystem.instance.playerLife = LifeSystem.instance.playerLife - enemyResult; 
        enemyDamage.SetActive(false);
        
    }
    //IEnumerator FightCoroutine(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    LifeSystem.instance.playerLife = LifeSystem.instance.playerLife - enemyResult;
    //    Debug.Log(LifeSystem.instance.playerLife);
    //    Debug.Log(LifeSystem.instance.enemylife);
    //}

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
