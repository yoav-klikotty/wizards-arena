using UnityEngine;
using TMPro;
public class Counter : MonoBehaviour
{
    [SerializeField] public int secondsLeft;
    [SerializeField] TMP_Text Seconds;

    void Start()
    {
        Seconds.text = secondsLeft + "";
        InvokeRepeating("DecreaseSeconds", 1, 1);
    }

    void DecreaseSeconds(){
        if (secondsLeft > 0){
            secondsLeft--;
            Seconds.text = secondsLeft + "";
        }
    }

    public bool IsCounterEnd(){
        return secondsLeft == 0;
    }
}
