using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterButton : MonoBehaviour
{
    [SerializeField] ItemType _filterValue;
    [SerializeField] GameObject _activeFiller;

    public void SetFilter(ItemType filter){
        if(filter == _filterValue) {
            _activeFiller.SetActive(true);
        }
        else {
            _activeFiller.SetActive(false);
        }
    }
}
