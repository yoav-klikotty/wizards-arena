using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Diagnostics;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler{
    private Vector3 panelLocation;
    public float percentThreshold = 0.5f;
    public float easing = 0.5f;
    public int totalPages = 5;
    private int currentPage = 3;
    Stopwatch stopwatch;
    void Start(){
        panelLocation = transform.position;
    }
    public void OnDrag(PointerEventData data){
        stopwatch = new Stopwatch();
        stopwatch.Start();
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }
    public void OnEndDrag(PointerEventData data){
        stopwatch.Stop();
        float localThreshold = percentThreshold;
        if (stopwatch.ElapsedMilliseconds < 20){
            localThreshold = 0;
        }
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if(Mathf.Abs(percentage) >= localThreshold){
            Vector3 newLocation = panelLocation;
            if(percentage > 0 && currentPage < totalPages){
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }else if(percentage < 0 && currentPage > 1){
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }else{
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
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

    public void setPage(int page) {
        int delta = page - currentPage;
        currentPage = page;
        Vector3 newLocation = transform.position;
        newLocation += new Vector3((-1*delta*Screen.width), 0, 0);
        StartCoroutine(SmoothMove(transform.position, newLocation, easing));
    }

    public int getPage() {
        return currentPage;
    }
}