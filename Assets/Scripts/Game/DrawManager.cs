using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] LinePrefab _linePrefab;
    private LinePrefab _currentLine;
    public const float RESOLUTION = 0.1f;
    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)) _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);

        if(Input.GetMouseButton(0)) _currentLine.SetPosition(mousePos);
    }
}
