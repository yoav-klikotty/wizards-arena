using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    
    [SerializeField] GameObject _indicationText;
    [SerializeField] Image _healthBar;
    [SerializeField] TMP_Text _healthLabel;
    Transform _camera;
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

    public void ActivateIndication(string indicationText, indicationEvents indicationEvent)
    {
        GameObject indicationTextObj = Instantiate(_indicationText, transform.position, Quaternion.identity, gameObject.transform);
        indicationTextObj.GetComponent<TextIndication>().Activate(indicationText, indicationEvent);
    }
}
