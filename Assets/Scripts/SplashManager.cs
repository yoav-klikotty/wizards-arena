using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    [SerializeField] Image _icon;


    public void Start()
    {
        var color = _icon.color;
        var fadeoutcolor = color;
        fadeoutcolor.a = 0;
        LeanTween.value(_icon.gameObject, updateValueExampleCallback, fadeoutcolor, new Color(1f, 1f, 1f, 1f), 2f).setEase(LeanTweenType.easeOutQuint)
        .setDelay(1f)
        .setOnComplete(FadeFinished);
    }
    void updateValueExampleCallback(Color val)
    {
        _icon.color = val;
    }
    void FadeFinished()
    {
        SceneManager.LoadScene("Dashboard");

    }
}
