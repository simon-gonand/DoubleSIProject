using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip SFXPlayCard;
    public AudioClip SFXHover;
    public AudioClip SFXAttackpart1;
    public AudioClip SFXAttackpart2;
    public float timeBetweenAttackSounds;
    public AudioClip mainMusic;

    public AudioSource audioSource;

    private int i = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        audioSource.clip = mainMusic;
        audioSource.Play();
    }

    public void PlaySFXPlayCard()
    {
        audioSource.PlayOneShot(SFXPlayCard);
    }
    public void PlaySFXHover()
    {
        audioSource.PlayOneShot(SFXHover,0.3f);
    }

    public void PlaySFXAttack1()
    {
        audioSource.PlayOneShot(SFXAttackpart1);

    }
    public void PlaySFXAttack2()
    {
        audioSource.PlayOneShot(SFXAttackpart2);
    }
}
