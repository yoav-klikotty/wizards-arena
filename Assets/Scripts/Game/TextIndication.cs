using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextIndication : MonoBehaviour
{
    [SerializeField] Animation _animation;
    [SerializeField] TMP_Text _indicationText;

    public void Activate(string value, string indicationEvent)
    {
        switch (indicationEvent) {
            case "hit":
                _indicationText.color = new Color(1f, 0, 0, 255);
                break;
            case "crit":
                _indicationText.color = new Color(1f, 1f, 0, 255);
                break;
            case "heal":
                _indicationText.color = new Color(0, 1f, 0, 255);
                break;
            case "mana":
                _indicationText.color = new Color(0, 0, 1f, 255);
                break;
            case "avoid":
                _indicationText.color = new Color(0.5f, 0.5f, 0.5f, 255);
                break;
        }
        _indicationText.text = "" + value;
        _animation.Play();
    }

    public void AnimationFinished()
    {
        Destroy(gameObject);
    }
}
