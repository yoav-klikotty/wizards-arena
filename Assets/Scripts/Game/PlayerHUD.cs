using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image _healthBar;
    [SerializeField] TMP_Text _healthLabel;
    Transform _camera;
    [SerializeField] Animation _animation;
    [SerializeField] TMP_Text _indicationText;
    [SerializeField] Image _manaBar;
    [SerializeField] TMP_Text _manaText;
    private float _barSpeed = 0.05f;

    void Start()
    {
        _camera = GameObject.Find("Camera").GetComponent<Transform>();
    }

    IEnumerator AdjustBar(Image bar, float targetValue)
    {
        yield return new WaitForSeconds(_barSpeed);
        float direction = (targetValue - bar.fillAmount) > 0 ? 1 : -1;
        float newFillAmount = bar.fillAmount + (_barSpeed*direction);
        if (direction == 1 && newFillAmount > targetValue) {
            bar.fillAmount = targetValue;
        }
        else if (direction == -1 && newFillAmount < targetValue) {
            bar.fillAmount = targetValue;
        }
        else {
            bar.fillAmount = newFillAmount;
            StartCoroutine(AdjustBar(bar, targetValue));
        }
    }

    public void UpdateHealth(float health, float maxHP)
    {
        _healthLabel.text = (int) health + "/" + maxHP;
        float barFillAmountTarget = health / maxHP;
        StartCoroutine(AdjustBar(_healthBar, barFillAmountTarget));
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }

    public void UpdateMana(float mana, float maxMana)
    {
        _manaText.text = (int) mana + " / " + maxMana;
        float barFillAmountTarget = mana / maxMana;
        StartCoroutine(AdjustBar(_manaBar, barFillAmountTarget));
    }

    public void ActivateIndication(string indicationText, string indicationEvent)
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
        _indicationText.text = indicationText;
        _animation.Play();
    }
}
