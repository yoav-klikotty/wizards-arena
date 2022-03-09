using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePrefab : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;
    private GameObject[] _dots;
    void Start()
    {
        _dots = GameObject.FindGameObjectsWithTag("dot");
    }

    public void SetPosition(Vector2 pos)
    {
        if(_lineRenderer.positionCount == 0) StartLine(pos);
        else
        {
            if(CanAppend(pos))
            {
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
            }
            else 
            {
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
            }
        }
    }

    private bool CanAppend(Vector2 pos)
    {
        for(int i = 0; i < _dots.Length; i++)
        {
            if(Vector2.Distance(_dots[i].transform.position, pos) < DrawManager.RESOLUTION)
            {
                return true;
            }
        }
        return false;
    }

    private void StartLine(Vector2 pos)
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
    }
}
