using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathmatchSliderSetter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _text;

    void Start()
    {
        _text.text = _slider.value + "";   
    }
    public void OnSliderChange()
    {
        GameManager.Instance.NumOfDeathmatchPlayers = (int) _slider.value;
        _text.text = _slider.value + "";   
    }
}
