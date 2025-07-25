using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixerGroup master;
    public AudioMixerGroup sfx;
    public AudioMixerGroup background;

    public Sound[] sounds;

    private float ConvertLevelToSliderValue(float level)
    {
        return Mathf.Exp(level / 20);

    }

    private float ConvertSliderToLevelValue(float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    public void Start()
    {
        
    }

    public void PlaySound(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void StopAll()
    {
        // Find all AudioSources routed to the target group
        List<AudioSource> audioSources = new List<AudioSource>();
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource source in allAudioSources)
        {
            if (source.outputAudioMixerGroup == background)
            {
                audioSources.Add(source);
            }
        }

        // Stop all found AudioSources
        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }
    }

    public void StopSound(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }

    public float GetMasterLevel()
    {
        float audioLevel = 0f;

        master.audioMixer.GetFloat("masterVolume", out audioLevel);

        float sliderValue = ConvertLevelToSliderValue(audioLevel);

        return sliderValue;
    }

    public float GetSFXLevel()
    {
        float audioLevel = 0f;

        sfx.audioMixer.GetFloat("sfxVolume", out audioLevel);

        float sliderValue = ConvertLevelToSliderValue(audioLevel);

        return sliderValue;

    }

    public float GetBackgroundLevel()
    {
        float audioLevel = 0f;

        background.audioMixer.GetFloat("backgroundVolume", out audioLevel);

        float sliderValue = ConvertLevelToSliderValue(audioLevel);

        return sliderValue;

    }

    public void SetMasterLevel(float sliderValue)
    {
        //Debug.Log(sliderValue);
        //Debug.Log("!!!");
        //Debug.Log(Mathf.Log10(sliderValue) * 20);
        //Debug.Log("!!!222");

        master.audioMixer.SetFloat("masterVolume", ConvertSliderToLevelValue(sliderValue));
    }

    public void SetSFXLevel(float sliderValue)
    {
        sfx.audioMixer.SetFloat("sfxVolume", ConvertSliderToLevelValue(sliderValue));
    }

    public void SetBackgroundLevel(float sliderValue)
    {
        background.audioMixer.SetFloat("backgroundVolume", ConvertSliderToLevelValue(sliderValue));
    }

}
