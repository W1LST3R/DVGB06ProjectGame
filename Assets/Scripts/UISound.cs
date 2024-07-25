using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public void playUI()
    {
        AudioManager.audio.playSFX(AudioManager.audio.UI);
    }
}
