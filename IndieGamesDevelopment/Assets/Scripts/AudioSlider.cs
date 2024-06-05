using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSlider : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private AudioSource m_BackgroundAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_BackgroundAudioSource = GameObject.Find("----AudioManager----").GetComponent<AudioSource>();
        m_AudioSource = GameObject.Find("Background_Music").GetComponent<AudioSource>();
    }

    public void changeBackgroundMusic(float value)
    {
        m_BackgroundAudioSource.volume = value;
    }
    public void changeAudioManager(float value)
    {
        m_AudioSource.volume = value;
    }
}
