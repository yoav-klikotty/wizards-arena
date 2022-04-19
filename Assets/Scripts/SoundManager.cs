using System.Collections;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
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
        _buttonPressSound.Play();
    }
    public void PlayYouWinSound()
    {
        _youWinSound.Play();
    }
    public void PlayYouLoseSound()
    {
        _youLoseSound.Play();
    }
    public void PlayTimesUpSound()
    {
        _timesUpSound.Play();
    }
    public void PlayGameThemeSound()
    {
        _gameThemeSound.Play();
    }
    public void StopGameThemeSound()
    {
        _gameThemeSound.Stop();
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
        _clockTickingSound.Play();
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