using System.Collections;
using UnityEngine;

public class WizardSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _actionsSound;
    [SerializeField] AudioSource _wizardHitSound;
    [SerializeField] AudioSource _shieldHitSound;

    public void PlayActionsSound(AudioClip sound)
    {
        _actionsSound.clip = sound;
        _actionsSound.Play();
    }
    public void PlayShieldHitSound()
    {
        _shieldHitSound.Play();
    }
    public void PlayWizardHitSound()
    {
        _wizardHitSound.Play();
    }
}
