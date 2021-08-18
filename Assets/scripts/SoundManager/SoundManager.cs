using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip[] soundEffects;
    public AudioClip[] songs;


    public AudioSource soundEffectAudioSource;
    public AudioSource songAudioSource;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null){
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySoundEffect(string name) {
        soundEffectAudioSource.PlayOneShot(soundEffects.First(x => x.name == name));
    }
    public void PlaySoundEffect(string name, float volume) {
        soundEffectAudioSource.PlayOneShot(soundEffects.First(x => x.name == name),volume);
    }
    public void PlaySong(string name, float volume){
        songAudioSource.clip = songs.First(x => x.name == name);
        songAudioSource.volume = volume;
        songAudioSource.loop = true;
        songAudioSource.Play();
    }
    public void PauseSong(){
        songAudioSource.Pause();
    }
    public void ResumeSong(){
        songAudioSource.Play();
    }
}
