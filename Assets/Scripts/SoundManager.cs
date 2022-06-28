using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains methods for playing audio. This along side with animations handle the sound for the game.
public class SoundManager : MonoBehaviour
{

    public AudioSource src;
    public AudioClip run, jump, swing, hit, damage, upgrade;


    public void RunSound()
    {
        src.clip = run;
        src.Play();
    }

    public void JumpSound()
    {
        src.clip = jump;
        src.Play();
    }

    public void SwingSound()
    {
        src.clip = swing;
        src.Play();
    }

    public void HitSound()
    {
        src.clip = hit;
        src.Play();
    }

    public void DamageSound()
    {
        src.clip = damage;
        src.Play();
    }

    public void UpgradeSound()
    {
        src.clip = upgrade;
        src.Play();
    }



    
}
