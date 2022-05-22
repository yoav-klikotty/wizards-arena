using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;
    private GameObject[] _dots;
    private Vector2 _currentDotPosition;
    private bool _passedMinimalDistance = true;
    private string _patternPhrase = "";
    void Awake()
    {
        _dots = GameObject.FindGameObjectsWithTag("dot");
    }

    public void SetPosition(Vector2 pos)
    {
        if(CanAppend(pos))
        {
            if(_lineRenderer.positionCount == 0) {
                StartLine(pos);
            }
            else {
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 2, _currentDotPosition);
            }
        }
        else 
        {
            if(_lineRenderer.positionCount != 0) _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
        }
    }

    private bool CanAppend(Vector2 pos)
    {
        if(!_passedMinimalDistance)
        {
            Vector2 lastPosition = _lineRenderer.GetPosition(_lineRenderer.positionCount - 2);
            if(Vector2.Distance(lastPosition, pos) < PatternKeyboard.MINIMAL_DOT_DISTANCE) return false;
            _passedMinimalDistance = true;
        }
        else {
            for(int i = 0; i < _dots.Length; i++)
            {
                Debug.Log(Vector2.Distance(_dots[i].transform.position, pos));
                if(Vector2.Distance(_dots[i].transform.position, pos) < PatternKeyboard.DOT_THRESHOLD_RADIUS)
                {
                    _currentDotPosition = _dots[i].transform.position;
                    _patternPhrase += i;
                    _passedMinimalDistance = false;
                    return true;
                }
            }
        }
        return false;
    }

    private void StartLine(Vector2 pos)
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _currentDotPosition);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
    }

    public string GetPatternPhrase()
    {
        return _patternPhrase;
    }
}
