using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardStats : MonoBehaviour
{
    [SerializeField] TMP_Text _baseDmg;
    [SerializeField] TMP_Text _critRate;
    [SerializeField] TMP_Text _critDmg;
    [SerializeField] TMP_Text _armorPen;
    [SerializeField] TMP_Text _maxHP;
    [SerializeField] TMP_Text _recovery;
    [SerializeField] TMP_Text _avoidability;
    [SerializeField] TMP_Text _mirroring;
    [SerializeField] TMP_Text _maxMana;
    [SerializeField] TMP_Text _startMana;
    [SerializeField] TMP_Text _manaRegenration;
    [SerializeField] TMP_Text _passiveManaRegeneration;

    public void SetDmg(int min, int max) {
        _baseDmg.text = "Base damage: " + min + "-" + max;
    }
    public void SetCritRate(float num) {
        _critRate.text = "Crit rate: " + num*100 + "%";
    }
    public void SetCritDmg(float num) {
        _critDmg.text = "Crit damage: " + num*100 + "%";
    }
    public void SetArmorPenetration(float num) {
        _armorPen.text = "Armor penetration: " + num*100 + "%";
    }
    public void SetMaxHP(int num) {
        _maxHP.text = "Max HP: " + num;
    }
    public void SetRecovery(int num) {
        _recovery.text = "Recovery: " + num;
    }
    public void SetMirroring(float num) {
        _mirroring.text = "Mirroring: " + num*100 + "%";
    }
    public void SetAvoidability(float num) {
        _avoidability.text = "Avoidability: " + num*100 + "%";
    }
    public void SetMaxMana(int num) {
        _maxMana.text = "Max mana: " + num;
    }
    public void SetStartMana(float num) {
        _startMana.text = "Starting mana: " + num*100 + "%";
    }
    public void SetManaRegeneration(int num) {
        _manaRegenration.text = "Mana regeneration: " + num;
    }
    public void SetPassiveManaRegeneration(int num) {
        _passiveManaRegeneration.text = "Passive mana regeneration: " + num;
    }
}
