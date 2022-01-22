using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Diagnostics;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler{
    [SerializeField] NavigationPanel _navigationPanel;
    [SerializeField] Wizard _wizard;
    private Vector3 _panelLocation;
    [SerializeField] float PercentThreshold;
    public float Easing = 0.5f;
    public int TotalPages = 3;
    private int CurrentPage = 2;
    Stopwatch stopwatch;
    void Start(){
        _panelLocation = transform.position;
    }
    public void OnDrag(PointerEventData data){
        stopwatch = new Stopwatch();
        stopwatch.Start();
        float difference = data.pressPosition.x - data.position.x;
        transform.position = _panelLocation - new Vector3(difference, 0, 0);
    }
    public void OnEndDrag(PointerEventData data){
        stopwatch.Stop();
        float localThreshold = PercentThreshold;
        if (stopwatch.ElapsedMilliseconds < 20){
            localThreshold = 0;
        }
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if(Mathf.Abs(percentage) >= localThreshold){
            Vector3 newLocation = _panelLocation;
            if(percentage > 0 && CurrentPage < TotalPages){
                CurrentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
                _navigationPanel.PageChange(CurrentPage);
            }else if(percentage < 0 && CurrentPage > 1){
                CurrentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
                _navigationPanel.PageChange(CurrentPage);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, Easing));
            _panelLocation = newLocation;
        }else{
            StartCoroutine(SmoothMove(transform.position, _panelLocation, Easing));
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds){
        float t = 0f;
        while(t <= 1.0){
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void SetPage(int page) {
        int delta = page - CurrentPage;
        CurrentPage = page;
        Vector3 newLocation = _panelLocation;
        newLocation += new Vector3((-1*delta*Screen.width), 0, 0);
        _panelLocation = newLocation;
        StartCoroutine(SmoothMove(transform.position, newLocation, Easing));
        switch(page) {
            case 1: 
                _wizard.ResetLocation();
                break;
            
            case 2:
                _wizard.TiltLocation();
                break;

            case 3:
                _wizard.DisabledWizard();
                break;
        }
    }

    public int getPage() {
        return CurrentPage;
    }
}