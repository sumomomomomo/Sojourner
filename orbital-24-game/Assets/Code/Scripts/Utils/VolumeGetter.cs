using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Utils/Volume Getter")]
public class VolumeGetter : ScriptableObject
{
    [SerializeField] private FloatVariable masterVolume;
    [SerializeField] private FloatVariable sfxVolume;
    [SerializeField] private FloatVariable bgmVolume;

    public float GetSfxVolume()
    {
        return Mathf.Clamp(sfxVolume.Value * masterVolume.Value, 0, 1);
    }

    public float GetBgmVolume()
    {
        return Mathf.Clamp(bgmVolume.Value * masterVolume.Value, 0, 1);
    }
}
