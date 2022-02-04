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
    public float requiredManaForSoftAttack;
    [SerializeField] GameObject _softAttackMagic;
    public float requiredManaForModerateAttack;
    [SerializeField] GameObject _moderateAttackMagic;
    public float requiredManaForHardAttack;
    [SerializeField] GameObject _hardAttackMagic;
    [SerializeField] Animation _animation;
    [SerializeField] TMP_Text _indicationText;
    [SerializeField] TMP_Text _wizardIndex;


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

    public void RenderAvailableAttacks(float mana)
    {
        if (mana >= requiredManaForSoftAttack)
        {
            _softAttackMagic.SetActive(true);
        }
        else
        {
            _softAttackMagic.SetActive(false);
        }
        if (mana >= requiredManaForModerateAttack)
        {
            _moderateAttackMagic.SetActive(true);
        }
        else
        {
            _moderateAttackMagic.SetActive(false);
        }
        if (mana >= requiredManaForHardAttack)
        {
            _hardAttackMagic.SetActive(true);
        }
        else
        {
            _hardAttackMagic.SetActive(false);
        }

    }

    public void ActivateIndication(string indicationText)
    {
        _indicationText.text = indicationText;
        _animation.Play();
    }
    public void SetWizardIndex(int wizardIndex)
    {
        _wizardIndex.text = wizardIndex + "";
    }
}
