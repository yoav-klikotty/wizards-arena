using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardStats : MonoBehaviour
{
    [SerializeField] TMP_Text _baseDmg;
    [SerializeField] TMP_Text _baseDmgDiff;
    [SerializeField] TMP_Text _critRate;
    [SerializeField] TMP_Text _critRateDiff;
    [SerializeField] TMP_Text _critDmg;
    [SerializeField] TMP_Text _critDmgDiff;
    [SerializeField] TMP_Text _armorPen;
    [SerializeField] TMP_Text _armorPenDiff;
    [SerializeField] TMP_Text _maxHP;
    [SerializeField] TMP_Text _maxHPDiff;
    [SerializeField] TMP_Text _recovery;
    [SerializeField] TMP_Text _recoveryDiff;
    [SerializeField] TMP_Text _avoidability;
    [SerializeField] TMP_Text _avoidabilityDiff;
    [SerializeField] TMP_Text _maxMana;
    [SerializeField] TMP_Text _maxManaDiff;
    [SerializeField] TMP_Text _startMana;
    [SerializeField] TMP_Text _startManaDiff;
    [SerializeField] TMP_Text _manaRegenration;
    [SerializeField] TMP_Text _manaRegenrationDiff;
    [SerializeField] TMP_Text _passiveManaRegeneration;
    [SerializeField] TMP_Text _passiveManaRegenerationDiff;
    WizardStatsData _wizardStatsData;
    public void SetDmg()
    {
        _baseDmg.text = "Base damage: " + _wizardStatsData.BaseAttackStatsData.BaseDamage;
        int append = _wizardStatsData.GetTotalBaseDamage() - _wizardStatsData.BaseAttackStatsData.BaseDamage;
        if (append > 0)
        {
            _baseDmg.text += " [+" + append + "]";
        }
    }
    public void SetCritRate()
    {
        _critRate.text = "Crit rate: " + _wizardStatsData.BaseAttackStatsData.CriticalRate * 100 + "%";
        float append = _wizardStatsData.GetTotalCriticalRate() - _wizardStatsData.BaseAttackStatsData.CriticalRate;
        if (append > 0)
        {
            _critRate.text += " [+" + append * 100 + "%]";
        }
    }
    public void SetCritDmg()
    {
        _critDmg.text = "Crit damage: " + _wizardStatsData.BaseAttackStatsData.CriticalDmg * 100 + "%";
        float append = _wizardStatsData.GetTotalCriticalDmg() - _wizardStatsData.BaseAttackStatsData.CriticalDmg;
        if (append > 0)
        {
            _critDmg.text += " [+" + append * 100 + "%]";
        }
    }
    public void SetArmorPenetration()
    {
        _armorPen.text = "Armor penetration: " + _wizardStatsData.BaseAttackStatsData.ArmorPenetration * 100 + "%";
        float append = _wizardStatsData.GetTotalArmorPenetration() - _wizardStatsData.BaseAttackStatsData.ArmorPenetration;
        if (append > 0)
        {
            _armorPen.text += " [+" + append * 100 + "%]";
        }
    }
    public void SetMaxHP()
    {
        _maxHP.text = "HP: " + _wizardStatsData.BaseDefenceStatsData.HP;
        float append = _wizardStatsData.GetTotalHP() - _wizardStatsData.BaseDefenceStatsData.HP;
        if (append > 0)
        {
            _maxHP.text += " [+" + append + "]";
        }
    }
    public void SetRecovery()
    {
        _recovery.text = "Recovery: " + _wizardStatsData.BaseDefenceStatsData.Recovery;
        float append = _wizardStatsData.GetTotalRecovery() - _wizardStatsData.BaseDefenceStatsData.Recovery;
        if (append > 0)
        {
            _recovery.text += " [+" + append + "]";
        }
    }
    public void SetAvoidability()
    {
        _avoidability.text = "Avoidability: " + _wizardStatsData.BaseDefenceStatsData.Avoidability * 100 + "%";
        float append = _wizardStatsData.GetTotalAvoidability() - _wizardStatsData.BaseDefenceStatsData.Avoidability;
        if (append > 0)
        {
            _avoidability.text += " [+" + append * 100 + "%]";
        }
    }
    public void SetMaxMana()
    {
        _maxMana.text = "Max mana: " + _wizardStatsData.BaseManaStatsData.MaxMana;
        float append = _wizardStatsData.GetTotalMaxMana() - _wizardStatsData.BaseManaStatsData.MaxMana;
        if (append > 0)
        {
            _maxMana.text += " [+" + append + "]";
        }
    }
    public void SetStartMana()
    {
        _startMana.text = "Starting mana: " + _wizardStatsData.BaseManaStatsData.StartMana;
        float append = _wizardStatsData.GetTotalStartMana() - _wizardStatsData.BaseManaStatsData.StartMana;
        if (append > 0)
        {
            _startMana.text += " [+" + append + "]";
        }
    }
    public void SetManaRegeneration()
    {
        _manaRegenration.text = "Regeneration: " + _wizardStatsData.BaseManaStatsData.ManaRegeneration;
        float append = _wizardStatsData.GetTotalManaRegeneration() - _wizardStatsData.BaseManaStatsData.ManaRegeneration;
        if (append > 0)
        {
            _manaRegenration.text += " [+" + append + "]";
        }
    }
    public void SetPassiveManaRegeneration()
    {
        _passiveManaRegeneration.text = "Passive Regeneration: " + _wizardStatsData.BaseManaStatsData.PassiveManaRegeneration;
        float append = _wizardStatsData.GetTotalPassiveManaRegeneration() - _wizardStatsData.BaseManaStatsData.PassiveManaRegeneration;
        if (append > 0)
        {
            _passiveManaRegeneration.text += " [+" + append + "]";
        }
    }
    void OnEnable()
    {
        EventManager.Instance.updateWizardStats += WriteWizardStats;
    }
    void OnDisable()
    {
        EventManager.Instance.updateWizardStats -= WriteWizardStats;
    }
    public void WriteWizardStats()
    {
        _wizardStatsData = WizardStatsController.Instance.GetWizardStatsData();
        SetDmg();
        SetCritDmg();
        SetCritRate();
        SetArmorPenetration();
        SetMaxHP();
        SetRecovery();
        SetAvoidability();
        SetMaxMana();
        SetStartMana();
        SetManaRegeneration();
        SetPassiveManaRegeneration();
        RemoveAllDiff();
    }
    public void RemoveAllDiff()
    {
        _baseDmgDiff.gameObject.SetActive(false);
        _critRateDiff.gameObject.SetActive(false);
        _critDmgDiff.gameObject.SetActive(false);
        _armorPenDiff.gameObject.SetActive(false);
        _maxHPDiff.gameObject.SetActive(false);
        _recoveryDiff.gameObject.SetActive(false);
        _avoidabilityDiff.gameObject.SetActive(false);
        _maxManaDiff.gameObject.SetActive(false);
        _startManaDiff.gameObject.SetActive(false);
        _manaRegenrationDiff.gameObject.SetActive(false);
        _passiveManaRegenerationDiff.gameObject.SetActive(false);
    }


    public void SetDiff(TMP_Text diffText, float diff)
    {
        if (diff != 0)
        {
            diffText.text = "" + diff;
            if (diff > 0)
            {
                diffText.color = Color.green;
            }
            else
            {
                diffText.color = Color.red;
            }
            diffText.gameObject.SetActive(true);
        }
        else
        {
            diffText.gameObject.SetActive(false);
        }
    }
}
