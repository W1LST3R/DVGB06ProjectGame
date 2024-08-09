using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    //Plays sfx sound, used by buttons
    public void playUI()
    {
        AudioManager.audio.playSFX(AudioManager.audio.UI);
    }
}
