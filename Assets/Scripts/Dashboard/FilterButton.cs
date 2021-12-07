using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterButton : MonoBehaviour
{
    [SerializeField] string _filterValue;
    [SerializeField] GameObject _activeFiller;

    public void SetFilter(string filter){
        if(filter == _filterValue || filter == "all") {
            _activeFiller.SetActive(true);
        }
        else {
            _activeFiller.SetActive(false);
        }
    }
}
