using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicLoop
{
    public AudioClip soundClip;
    public float introLength;
    public float loopLength;
}

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public MusicLoop[] musicLoops;
    public MusicLoop currentLoop;

    private void Awake()
    {
        StartLoop(musicLoops[0]);
    }

    public void StartLoop(MusicLoop musicLoop)
    {
        currentLoop = musicLoop;
        musicSource.clip = currentLoop.soundClip;
        musicSource.loop = false;
        musicSource.Play();
    }

    private void Update()
    {
        float currentTime = musicSource.time;
        float trackLength = currentLoop.introLength + currentLoop.loopLength;

        if(currentTime > trackLength)
        {
            musicSource.time -= currentLoop.loopLength;
        }
    }
}
