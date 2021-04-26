using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.isLoop;
        }
    }

    // Calls Play on audio clip
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    // Checks if audio clip is playing
    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.isPlaying;
    }

    // Calls Stop on audio clip
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }

    private void FixedUpdate()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Checks which scene user is on and changes the audio based on result.
        if (currentScene.name == "Menu" && !isPlaying("MainMenuTheme"))
        {
            if (isPlaying("LevelTheme")) Stop("LevelTheme");
            Play("MainMenuTheme");
        }
        else if ((currentScene.name == "Level_1" || currentScene.name == "Level_2") && !isPlaying("LevelTheme"))
        {
            if (isPlaying("MainMenuTheme")) Stop("MainMenuTheme");
            Play("LevelTheme");
        }
        
    }

}
