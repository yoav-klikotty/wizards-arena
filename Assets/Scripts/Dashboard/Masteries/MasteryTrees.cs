using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasteryTrees : MonoBehaviour
{
    [SerializeField] GameObject _attackTree;
    [SerializeField] GameObject _defenceTree;
    [SerializeField] GameObject _manaTree;

    public void ChooseAttackTree()
    {
        _attackTree.SetActive(true);
        _defenceTree.SetActive(false);
        _manaTree.SetActive(false);
    }
    public void ChooseDefenceTree()
    {
        _attackTree.SetActive(false);
        _defenceTree.SetActive(true);
        _manaTree.SetActive(false);
    }
    public void ChooseManaTree()
    {
        _attackTree.SetActive(false);
        _defenceTree.SetActive(false);
        _manaTree.SetActive(true);
    }

    public MasteryTree GetCurrentTree()
    {
        if (_attackTree.activeSelf)
        {
            return _attackTree.GetComponent<MasteryTree>();
        }
        else if (_defenceTree.activeSelf)
        {
            return _defenceTree.GetComponent<MasteryTree>();
        }
        else
        {
            return _manaTree.GetComponent<MasteryTree>();
        }
    }
}
