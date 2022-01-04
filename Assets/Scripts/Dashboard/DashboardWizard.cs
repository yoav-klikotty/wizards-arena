using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardWizard : MonoBehaviour
{
    [SerializeField] Wizard _wizard;

    public void ChangePage (int page){
        switch(page) {
            case 1: 
                InventoryPage();
                break;
            
            case 2:
                ArenaPage();
                break;

            case 3:
                StorePage();
                break;

        }
    }

    private void InventoryPage() {
        gameObject.SetActive(true);
        Vector3 newLocation = new Vector3(-0.9f, -0.1f, 0);
        StartCoroutine(SmoothMove(newLocation));
        _wizard.transform.localRotation = Quaternion.Euler(0,180,0);
    }
    private void ArenaPage() {
        gameObject.SetActive(true);
        Vector3 newLocation = new Vector3(-0.9f, -0.1f, 0);
        StartCoroutine(SmoothMove(newLocation));
        _wizard.transform.localRotation = Quaternion.Euler(20, 150, -10);
        _wizard.IdleAni();
    }
    private void StorePage() {
        gameObject.SetActive(false);
    }

    private IEnumerator SmoothMove(Vector3 endpos){
        float t = 0f;
        while(t <= 1.0){
            t += Time.deltaTime / 1f;
            transform.position = Vector3.Lerp(transform.position, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
