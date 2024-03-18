using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerFootstepsAudioControl : MonoBehaviour
{
    [Header("Player Movement")]
    public List<AudioClip> audioSourceMovementWalking = new();
    public List<AudioClip> audioSourceMovementRunning = new();
    public List<AudioClip> audioSourceMovementSprinting = new();

    [Space(10)]
    public float walkingInterval = 0.5f;
    public float runningInterval = 0.3f;
    public float sprintingInterval = 0.1f;

    [Space(10), Range(0,1f)]
    public float PanStrength = 0.5f;

    bool isLeftFoot = false;
    float footInterval = 0;
    float currentSetInterval = 0f;

    [Space(10)]
    public AudioSource audioSourceFootstepPlayer;
    public FirstPersonController _firstPersonController;

    void Update()
    {
        switch (_firstPersonController.GetCurrentMovementState)
        {
            case PlayerMovementState.WALK:
                currentSetInterval = walkingInterval;
                break;
            case PlayerMovementState.RUN:
                currentSetInterval = runningInterval;
                break;
            case PlayerMovementState.SPRINT:
                currentSetInterval = sprintingInterval;
                break;
            default:
                return;
        }

        footInterval += Time.deltaTime;

        if (footInterval >= currentSetInterval)
        {
            footInterval %= currentSetInterval;
            PlayMovementFootstepSound();
        }
    }

    void PlayMovementFootstepSound()
    {
        AudioClip clip;

        switch (_firstPersonController.GetCurrentMovementState)
        {
            case PlayerMovementState.WALK:
                clip = audioSourceMovementWalking[Random.Range(0, audioSourceMovementWalking.Count)];
                break;
            case PlayerMovementState.RUN:
                clip = audioSourceMovementRunning[Random.Range(0, audioSourceMovementRunning.Count)];
                break;
            case PlayerMovementState.SPRINT:
                clip = audioSourceMovementSprinting[Random.Range(0, audioSourceMovementSprinting.Count)];
                break;
            default:
                return;
        }

        audioSourceFootstepPlayer.panStereo = isLeftFoot ? -PanStrength : PanStrength;
        audioSourceFootstepPlayer.PlayOneShot(clip);

        isLeftFoot = !isLeftFoot;
    }
}