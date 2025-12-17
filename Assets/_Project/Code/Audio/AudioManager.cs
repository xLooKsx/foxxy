using NUnit.Framework.Constraints;
using UnityEngine;

public class AudioManager : MonoBehaviour, IAudioManager
{
    [Header("Audio Controllers")]
    [SerializeField] AudioSource sfxAudioSource;
    
    [Header("Sounds")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip DamageTaken;
    [SerializeField] AudioClip Checkpoint;

    void Start()
    {
        Core.Instance.IAudioManagerRegistration(this);
    }

    public void PlaySfx(SfxType sfxType)
    {
        sfxAudioSource.PlayOneShot(ChooseSound(sfxType));
    }

    private AudioClip ChooseSound(SfxType sfxType)
    {
        switch (sfxType)
        {
            case SfxType.Jump:
            return jumpSound;
            
            case SfxType.Death:
            return Death;
            
            case SfxType.DamageTaken:
            return DamageTaken;
            
            case SfxType.Checkpoint:
            return Checkpoint;

            default:
            throw new System.ArgumentOutOfRangeException();
        }
    }
}
