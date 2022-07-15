using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tabs : MonoBehaviour
{
    [SerializeField] GameObject firstTab;
    [SerializeField] GameObject secondTab;
    [SerializeField] GameObject thirdTab;
    public void ActiveFirstTab()
    {
        firstTab.GetComponent<Image>().enabled = true;
        secondTab.GetComponent<Image>().enabled = false;
        thirdTab.GetComponent<Image>().enabled = false;
    }
    public void ActiveSecondTab()
    {
        firstTab.GetComponent<Image>().enabled = false;
        secondTab.GetComponent<Image>().enabled = true;
        thirdTab.GetComponent<Image>().enabled = false;
    }
    public void ActiveThirdTab()
    {
        firstTab.GetComponent<Image>().enabled = false;
        secondTab.GetComponent<Image>().enabled = false;
        thirdTab.GetComponent<Image>().enabled = true;
    }
}
