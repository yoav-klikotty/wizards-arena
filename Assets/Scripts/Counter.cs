using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Counter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int secondsLeft;
    [SerializeField] TMP_Text Seconds;
    public bool isCounterDisabled;
    void Start()
    {
        Seconds.text = secondsLeft + "";
        InvokeRepeating("DecreaseSeconds", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DecreaseSeconds(){
        if (!isCounterDisabled && secondsLeft > 0){
            secondsLeft--;
            Seconds.text = secondsLeft + "";
        }
    }

    public bool IsCounterEnd(){
        return secondsLeft == 0;
    }

    public void ResetCounter(){
        secondsLeft = 5;
        Seconds.text = secondsLeft + "";
    }
}
