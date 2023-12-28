using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkSounds : MonoBehaviour
{
    public List<AudioClip> WalkSounds;
    public AudioSource AudioSource;

    public int Position;

    public void PlayWalkSound()
    {
        Position = (int)Mathf.Floor(Random.Range(0, WalkSounds.Count));
        AudioSource.PlayOneShot(WalkSounds[Position]);
    }

}
