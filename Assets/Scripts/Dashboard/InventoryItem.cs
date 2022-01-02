using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private bool _equiped;
    [SerializeField] string _type;
    [SerializeField] string _name;
    [SerializeField] int _requiredLevel;
    [SerializeField] List<string> _attributes = new List<string>();
    [SerializeField] string _description;
    [SerializeField] Sprite _icon;
    [SerializeField] ItemPanel _itemPanel;
    [SerializeField] DefenceStatsData _defenceStatsData;
    [SerializeField] AttackStatsData _attackStatsData;
    [SerializeField] ManaStatsData _manaStatsData;

    void Start(){
        _icon = GetComponent<Image>().sprite;
        _itemPanel = Resources.FindObjectsOfTypeAll<ItemPanel>()[0];
    }
    public void SetEquipedStatus(bool equiped) {
        _equiped = equiped;
    }
    public bool GetEquipedStatus() {
        return _equiped;
    }
    public void ItemClicked(){
        _itemPanel.OpenPanel(this);
        RectTransform itemRect = this.GetComponent<RectTransform>();
    }
    public string GetName() {
        return _name;
    }
    public int GetRequiredLevel() {
        return _requiredLevel;
    }
    public string GetDescription() {
        return _description;
    }
    public string GetType() {
        return _type;
    }
    public Sprite GetIcon() {
        return _icon;
    }
    public List<string> GetAttributes() {
        return _attributes;
    }
    public AttackStatsData GetAttackStatsData() {
        return _attackStatsData;
    }
    public DefenceStatsData GetDefenceStatsData() {
        return _defenceStatsData;
    }
    public ManaStatsData GetManaStatsData() {
        return _manaStatsData;
    }
}
