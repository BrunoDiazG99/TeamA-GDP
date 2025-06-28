using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public Slider volMasterSlider;
    public Slider volSfxSlider;
    public Slider volMusicSlider;


    void Start()
    {
        SetSliderValues();

        volMasterSlider.onValueChanged.AddListener(SetMasterLevel);
        volSfxSlider.onValueChanged.AddListener(SetSfxLevel);
        volMusicSlider.onValueChanged.AddListener(SetBackgroundLevel);
    }

    private void OnDestroy()
    {
        volMasterSlider.onValueChanged.RemoveAllListeners();
        volSfxSlider.onValueChanged.RemoveAllListeners();
        volMusicSlider.onValueChanged.RemoveAllListeners();
    }

    private void SetSliderValues()
    {
        float audioMasterLevel = 0f;
        float audioSfxLevel = 0f;
        float audioBackgroundLevel = 0f;


        audioMasterLevel = AudioManager.instance.GetMasterLevel();
        audioSfxLevel = AudioManager.instance.GetSFXLevel();
        audioBackgroundLevel = AudioManager.instance.GetBackgroundLevel();

        //Debug.Log("msater");
        //Debug.Log(audioMasterLevel);
        //Debug.Log("sfx");
        //Debug.Log(audioSfxLevel);
        //Debug.Log("background");
        //Debug.Log(audioBackgroundLevel);

        volMasterSlider.value = audioMasterLevel;
        volSfxSlider.value = audioSfxLevel;
        volMusicSlider.value = audioBackgroundLevel;
    }

    public void SetMasterLevel(float sliderValue)
    {
        //Debug.Log(this.value);
        //Debug.Log("!!!111");
        //Debug.Log(sliderValue);
        //AudioManager.instance.master.audioMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        AudioManager.instance.SetMasterLevel(sliderValue);
    }
    public void SetSfxLevel(float sliderValue)
    {
        //AudioManager.instance.sfx.audioMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        AudioManager.instance.SetSFXLevel(sliderValue);
    }
    public void SetBackgroundLevel(float sliderValue)
    {
        //AudioManager.instance.background.audioMixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        AudioManager.instance.SetBackgroundLevel(sliderValue);
    }

}
