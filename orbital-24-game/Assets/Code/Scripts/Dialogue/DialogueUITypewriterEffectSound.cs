using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUITypewriterEffectSound : MonoBehaviour
{
    [SerializeField] private DialogueUITypewriterEffect dialogueUITypewriterEffect;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FloatReference waitTime;

    private Coroutine typingSoundCoroutine;

    public void SetAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }
    
    public void Run()
    {
        typingSoundCoroutine = StartCoroutine(TypeTextSound(audioSource));
    }

    public void Stop()
    {
        StopCoroutine(typingSoundCoroutine);
    }

    private IEnumerator TypeTextSound(AudioSource audioSource)
    {
        while (dialogueUITypewriterEffect.IsRunning)
        {
            while (!dialogueUITypewriterEffect.IsTalking)
            {
                yield return null;
            }
            audioSource.Play();
            yield return new WaitForSeconds(waitTime.Value);
        }
    }
}
