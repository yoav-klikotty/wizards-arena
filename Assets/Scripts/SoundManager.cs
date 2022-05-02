using System.Collections;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public bool isMusicOn = true;
    public bool isSfxOn = true;
    [SerializeField] AudioSource _buttonPressSound;
    [SerializeField] AudioSource _negativeButtonPressSound;
    [SerializeField] AudioSource _battleButtonPressSound;
    [SerializeField] AudioSource _switchItemsTabSound;
    [SerializeField] AudioSource _magicLearnedSound;
    [SerializeField] AudioSource _masteryOpenedSound;
    [SerializeField] AudioSource _battleBackgroundSound;
    [SerializeField] AudioSource _equipItemSound;
    [SerializeField] AudioSource _purchaseSound;
    [SerializeField] AudioSource _youWinSound;
    [SerializeField] AudioSource _youLoseSound;
    [SerializeField] AudioSource _timesUpSound;
    [SerializeField] AudioSource _gameThemeSound;
    [SerializeField] AudioSource _clockTickingSound;
    [SerializeField] AudioSource _playerFoundSound;
    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayButtonSound()
    {
        if(isSfxOn) _buttonPressSound.Play();
    }
    public void PlayNegativeButtonSound()
    {
        if(isSfxOn) _negativeButtonPressSound.Play();
    }
    public void PlayBattleButtonSound()
    {
        if(isSfxOn) _battleButtonPressSound.Play();
    }
    public void PlaySwitchTabSound()
    {
        if(isSfxOn) _switchItemsTabSound.Play();
    }
    public void PlayMagicLearnedSound()
    {
        if(isSfxOn) _magicLearnedSound.Play();
    }
    public void PlayMasteryUpgradeSound()
    {
        if(isSfxOn) _masteryOpenedSound.Play();
    }
    public void PlayBattleBackgroundSound()
    {
        if(isMusicOn)
        {
            _battleBackgroundSound.Play();
        }
    }
    public void PlayEquipItemSound()
    {
        if(isSfxOn) _equipItemSound.Play();
    }
    public void PlayPurchaseSound()
    {
        if(isSfxOn) _purchaseSound.Play();
    }
    public void PlayYouWinSound()
    {
        if(isSfxOn) _youWinSound.Play();
    }
    public void PlayYouLoseSound()
    {
        if(isSfxOn) _youLoseSound.Play();
    }
    public void PlayTimesUpSound()
    {
        // if(isSfxOn) _timesUpSound.Play();
    }
    public void PlayGameThemeSound()
    {
        if(isMusicOn)
        {
            _gameThemeSound.Play();
        }
    }
    public void StopGameThemeSound()
    {
        _gameThemeSound.Stop();
    }
    public void StopBattleBackgroundSound()
    {
        _battleBackgroundSound.Stop();
    }
    public IEnumerator FadeOutGameThemeSong(float FadeTime)
    {
        float startVolume = _gameThemeSound.volume;

        while (_gameThemeSound.volume > 0)
        {
            _gameThemeSound.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        _gameThemeSound.Stop();
        _gameThemeSound.volume = startVolume;
    }
    public void PlayClockTickingSound()
    {
        // _clockTickingSound.Play();
    }
    public void StopClockTickingSound()
    {
        _clockTickingSound.Stop();
    }
    public bool IsClockTickingSoundPlay()
    {
        return _clockTickingSound.isPlaying;
    }
}