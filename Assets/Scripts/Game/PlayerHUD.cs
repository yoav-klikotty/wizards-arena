using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] Transform _camera;
    public float requiredManaForSoftAttack;
    [SerializeField] GameObject _softAttackMagic;
    public float requiredManaForModerateAttack;
    [SerializeField] GameObject _moderateAttackMagic;
    public float requiredManaForHardAttack;
    [SerializeField] GameObject _hardAttackMagic;

    public void SetHealthBar(float health)
    {
        _healthBar.value = health;
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
}
