using System.Collections.Generic;
using UnityEngine;

public class PlayerBreathingAudioControl : MonoBehaviour
{
    [Space(10), Header("Player Breathing")]
    public List<AudioClip> audioClipsBreathingInhale = new();
    public List<AudioClip> audioClipsBreathingExhale = new();
    [Space(10)]
    public float breathingInterval = 0;

    [Space(10)]
    public AudioSource audioSourceBreathingPlayer;

    float currentBreathingInterval = 0;
    bool isInhale = false;

    void Update()
    {
        currentBreathingInterval += Time.deltaTime;

        if (currentBreathingInterval >= breathingInterval)
        {
            currentBreathingInterval %= breathingInterval;
            PlayBreathingSound();
        }
    }

    void PlayBreathingSound()
    {
        AudioClip clip;

        clip = isInhale ? audioClipsBreathingInhale[Random.Range(0, audioClipsBreathingInhale.Count)] : audioClipsBreathingExhale[Random.Range(0, audioClipsBreathingExhale.Count)];

        isInhale = !isInhale;

        audioSourceBreathingPlayer.PlayOneShot(clip);
    }
}
