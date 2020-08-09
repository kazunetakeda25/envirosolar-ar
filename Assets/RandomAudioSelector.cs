using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioSelector : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = RandomClip();
        if (source.playOnAwake) source.Play();
    }

    AudioClip RandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }

    public void PlayRandom()
    {
        source.clip = RandomClip();
        source.Play();
    }
}
