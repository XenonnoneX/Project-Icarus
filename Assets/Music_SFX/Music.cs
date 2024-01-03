using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] music;
    public AudioSource audioSource;
    public int currentTrack = 0;
    public bool isPlaying = false;
    public bool isPaused = false;
    public bool isMuted = false;
    public bool isLooping = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music[currentTrack];
        audioSource.loop = isLooping;
        audioSource.Play();
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
