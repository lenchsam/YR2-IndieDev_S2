using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource AS;
    void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();
    }

    public void SetVolume()
    {
        Debug.Log("set volume");
    }
    public AudioClip getRandAudio(AudioClip[] audioSounds)
    {
        int rand = Random.Range(0, audioSounds.Length - 1);
        return audioSounds[rand];
    }
    public void playSound(AudioClip audioToPlay)
    {
        AS.PlayOneShot(audioToPlay);
    }
}
