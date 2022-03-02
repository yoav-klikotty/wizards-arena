using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum indicationEvents
{
    hit,
    crit,
    mana,
    heal,
    avoid
}
public class TextIndication : MonoBehaviour
{
    [SerializeField] Animation _animation;
    [SerializeField] TMP_Text _indicationText;

    public void Activate(string value, indicationEvents indicationEvent)
    {
        switch (indicationEvent) {
            case indicationEvents.hit:
                _indicationText.color = new Color(1f, 0, 0, 255);
                break;
            case indicationEvents.crit:
                _indicationText.color = new Color(1f, 1f, 0, 255);
                break;
            case indicationEvents.heal:
                _indicationText.color = new Color(0, 1f, 0, 255);
                break;
            case indicationEvents.mana:
                _indicationText.color = new Color(0, 0, 1f, 255);
                break;
            case indicationEvents.avoid:
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
