using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audio;
    [Header ("--------Audio Source--------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXEfects;

    [Header("--------Audio Clip--------")]
    public AudioClip background;
    public AudioClip enemyDeath;
    public AudioClip death;
    public AudioClip UI;
    public AudioClip wallJump;
    public AudioClip finish;

    private void Awake()
    {
        if (audio == null)
        {
            audio = this;
        }
    }
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play ();
    }
    public void playSFX(AudioClip clip)
    {
       SFXEfects.PlayOneShot(clip);
    }

}
