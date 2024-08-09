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
    public AudioClip woodShoot;
    public AudioClip UI;
    public AudioClip wallJump;
    public AudioClip finish;

    //Sets instanxe of the audio object
    private void Awake()
    {
        if (audio == null)
        {
            audio = this;
        }
    }

    //Starts playing game music
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play ();
    }

    //Plays sound effects
    public void playSFX(AudioClip clip)
    {
       SFXEfects.PlayOneShot(clip);
    }

}
