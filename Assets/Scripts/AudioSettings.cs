using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Image soundButton;
    [SerializeField] private Image musicButton;

    [SerializeField] private Sprite[] sound;
    [SerializeField] private Sprite[] music;

    public int soundOn = 1;
    public int musicOn = 1;

    private void Start()
    {
         soundOn = PlayerPrefs.GetInt("soundOn", 1);
         musicOn = PlayerPrefs.GetInt("musicOn", 1);
         ApplySettings();
    }
    
    public void Sound()
    {
        soundOn = soundOn == 0 ? 1 : 0;
        ApplySettings();
    }

    public void Music()
    {
        musicOn = musicOn == 0 ? 1 : 0;
        ApplySettings();
    }

    private void ApplySettings()
    {
        PlayerPrefs.SetInt("soundOn", soundOn);
        PlayerPrefs.SetInt("musicOn", musicOn);

        soundButton.sprite = soundOn == 1 ? sound[1] : sound[0];
        musicButton.sprite = musicOn == 1 ? music[1] : music[0];

        Sound[] sounds = FindObjectOfType<AudioManager>().sounds;
        Sound s = Array.Find(sounds, sound => sound.name == "Theme");
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        
        s.source.volume = musicOn == 1 ? 0.3f : 0;

        AudioListener.volume = soundOn == 0 ? 0 : 1;
    }
}
