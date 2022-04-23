using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] GameObject magics;
    [SerializeField] GameObject masteries;
    [SerializeField] GameObject items;

    [SerializeField] Wizard dashboardWizard;
    public void SetMagics()
    {
        magics.SetActive(true);
        masteries.SetActive(false);
        items.SetActive(false);
        dashboardWizard.DisabledWizard();
    }
    public void SetMasteries()
    {
        magics.SetActive(false);
        masteries.SetActive(true);
        items.SetActive(false);
        dashboardWizard.DisabledWizard();
    }
    public void SetItems()
    {
        magics.SetActive(false);
        masteries.SetActive(false);
        items.SetActive(true);
        dashboardWizard.EnableWizard();
    }

}
