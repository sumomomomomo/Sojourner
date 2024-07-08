using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeUpdater : MonoBehaviour
{
    [SerializeField] private VolumeGetter volumeGetter;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool isSfx = false;
    void Update()
    {
        if (isSfx)
        {
            audioSource.volume = volumeGetter.GetSfxVolume();
        }
        else
        {
            audioSource.volume = volumeGetter.GetBgmVolume();
        }
    }
}
