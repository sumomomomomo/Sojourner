using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsHandler : MonoBehaviour
{
    [SerializeField] private BoolVariable isVolumeSettingsInitialized;
    [SerializeField] private FloatVariable masterVolume;
    [SerializeField] private FloatVariable sfxVolume;
    [SerializeField] private FloatVariable bgmVolume;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;

    void Start()
    {
        if (!isVolumeSettingsInitialized.Value)
        {
            masterVolume.Value = 1;
            sfxVolume.Value = 1;
            bgmVolume.Value = 1;
            isVolumeSettingsInitialized.Value = true;
        }

        masterVolumeSlider.value = masterVolume.Value;
        sfxVolumeSlider.value = sfxVolume.Value;
        bgmVolumeSlider.value = bgmVolume.Value;
    }

    public void UpdateMasterVolume(float newVolume)
    {
        masterVolume.Value = newVolume;
    }

    public void UpdateSFXVolume(float newVolume)
    {
        sfxVolume.Value = newVolume;
    }

    public void UpdateBGMVolume(float newVolume)
    {
        bgmVolume.Value = newVolume;
    }
}
