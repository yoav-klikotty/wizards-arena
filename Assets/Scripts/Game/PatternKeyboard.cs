using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternKeyboard : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] Line _linePrefab;
    private Line _currentLine;
    public const float DOT_THRESHOLD_RADIUS = 0.3f;
    public const float MINIMAL_DOT_DISTANCE = 1f;
    private string[] _patterns = {"012", "042", "345"};
    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)) _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);

        if(Input.GetMouseButton(0)) _currentLine.SetPosition(mousePos);

        if(Input.GetMouseButtonUp(0)) LineResolver();
    }

    private void LineResolver()
    {
        bool destroyed = false;
        var lr = _currentLine.GetComponent<LineRenderer>();
        lr.positionCount--;
        for(int i = 0; i < _patterns.Length; i++)
        {
            if(_patterns[i] == _currentLine.GetPatternPhrase())
            {
                Destroy(_currentLine.gameObject, 2);
                destroyed = true;
            }
        }
        if(!destroyed) Destroy(_currentLine.gameObject);
    }
}
