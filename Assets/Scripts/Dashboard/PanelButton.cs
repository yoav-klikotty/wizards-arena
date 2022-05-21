using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    [SerializeField] PageSwiper _pageSwiper;
    [SerializeField] int _panelNum;
    [SerializeField] GameObject _activeFiller;
    [SerializeField] GameObject _text;

    private bool _isActive;

    void Start()
    {
        _isActive = _pageSwiper.getPage() == _panelNum;
        if (_isActive)
        {
            _activeFiller.SetActive(true);
            Scale(true);
        }
    }

    public void SetPage(int page)
    {
        if (page == _panelNum)
        {
            _pageSwiper.SetPage(_panelNum);
            _isActive = true;
            _activeFiller.SetActive(true);
            Scale(true);
        }
        else
        {
            _isActive = false;
            _activeFiller.SetActive(false);
            Scale(false);
        }
    }

    private void Scale(bool active)
    {
        if (active)
        {
            gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 0);
            _text.SetActive(true);
        }
        else
        {
            _text.SetActive(false);
            gameObject.transform.localScale = new Vector3(1f, 1f, 0);
        }
    }
}
