using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] TMP_Text _healthLabel;
    Transform _camera;
    [SerializeField] Animation _animation;
    [SerializeField] TMP_Text _indicationText;
    [SerializeField] TMP_Text _manaText;

    void Start()
    {
        _camera = GameObject.Find("Camera").GetComponent<Transform>();
    }

    public void SetHealthBar(float health, float maxHP)
    {
        _healthBar.maxValue = maxHP;
        _healthBar.value = health;
        _healthLabel.text = health + "/" + maxHP;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }

    public void UpdateMana(float mana)
    {
        _manaText.text = (int) mana + "";
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
