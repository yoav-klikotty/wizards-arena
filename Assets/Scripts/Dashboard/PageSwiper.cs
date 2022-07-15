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
    public int TotalPages = 5;
    private int CurrentPage = 3;
    Stopwatch stopwatch;
    float w;
    void Start(){
        _panelLocation = transform.localPosition;
        w = GetComponent<RectTransform>().rect.width;
        int initialPage = LocalStorage.GetDashboardPage();
        if (initialPage > 0)
        {
            SetPage(initialPage);
            LocalStorage.SetDashboardPage(0);
        }
    }
    public void OnDrag(PointerEventData data){
        stopwatch = new Stopwatch();
        stopwatch.Start();
        float difference = data.pressPosition.x - data.position.x;
        transform.localPosition = _panelLocation - new Vector3(difference, 0, 0);
    }
    public void OnEndDrag(PointerEventData data){
        stopwatch.Stop();
        float localThreshold = PercentThreshold;
        if (stopwatch.ElapsedMilliseconds < 20){
            localThreshold = 0;
        }
        float percentage = (data.pressPosition.x - data.position.x) / w;
        if(Mathf.Abs(percentage) >= localThreshold){
            Vector3 newLocation = _panelLocation;
            if(percentage > 0 && CurrentPage < TotalPages){
                CurrentPage++;
                newLocation += new Vector3(-w, 0, 0);
                _navigationPanel.PageChange(CurrentPage);
            }else if(percentage < 0 && CurrentPage > 1){
                CurrentPage--;
                newLocation += new Vector3(w, 0, 0);
                _navigationPanel.PageChange(CurrentPage);
            }
            StartCoroutine(SmoothMove(transform.localPosition, newLocation, Easing));
            _panelLocation = newLocation;
        }else{
            StartCoroutine(SmoothMove(transform.localPosition, _panelLocation, Easing));
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds){
        float t = 0f;
        while(t <= 1.0){
            t += Time.deltaTime / seconds;
            transform.localPosition = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void SetPage(int page) {
        int delta = page - CurrentPage;
        CurrentPage = page;
        Vector3 newLocation = _panelLocation;
        newLocation += new Vector3((-1*delta*w), 0, 0);
        _panelLocation = newLocation;
        StartCoroutine(SmoothMove(transform.localPosition, newLocation, Easing));
    }

    public int getPage() {
        return CurrentPage;
    }
}