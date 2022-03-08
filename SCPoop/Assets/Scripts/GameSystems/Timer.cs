using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    private float _timer;
    public float timer { get { return _timer; } }
    public float maxTimer;

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
        ResetTime();
    }

    public void ResetTime()
    {
        _timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0.0f)
        {
            GameManager.instance.TimerEnd();
        }
    }
}
