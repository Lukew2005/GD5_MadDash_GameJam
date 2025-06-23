using UnityEngine;

using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public Slider volumeSlider;

    private void Start()
    {
        //musicSource.clip = musicSounds[0];
       // musicSource.Play();
    }

    private void Update()
    {
        //musicSource.volume = volumeSlider.value;
    }

    public void playSoundEffects()
    {
        //sfxSource.PlayOneShot(sfxSounds[Random.Range(0, sfxSounds.Length)]);
    }
}