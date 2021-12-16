using UnityEngine;
using TMPro;
public class Counter : MonoBehaviour
{
    [SerializeField] public int SecondsLeft;
    [SerializeField] TMP_Text Seconds;

    void Start()
    {
        Seconds.text = SecondsLeft + "";
        InvokeRepeating("DecreaseSeconds", 1, 1);
    }

    void DecreaseSeconds(){
        if (SecondsLeft > 0){
            SecondsLeft--;
            Seconds.text = SecondsLeft + "";
        }
    }

    public bool IsCounterEnd(){
        return SecondsLeft == 0;
    }
}
