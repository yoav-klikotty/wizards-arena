using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterButton : MonoBehaviour
{
    [SerializeField] string filterValue;
    [SerializeField] GameObject activeFiller;

    public void SetFilter(string filter){
        if(filter == filterValue || filter == "all") {
            activeFiller.SetActive(true);
        }
        else {
            activeFiller.SetActive(false);
        }
    }
}
