using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip SFXPlayCard;
    public AudioClip SFXAttack;
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

    public void PlaySFXAttack()
    {
        audioSource.PlayOneShot(SFXAttack);
    }
}
