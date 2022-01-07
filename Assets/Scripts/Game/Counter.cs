using UnityEngine;
using Lovatto.Countdown;

public class Counter : MonoBehaviour
{
    public bl_Countdown countdownRoot;
    void Start()
    {
        countdownRoot.StartCountdown();
    }

    public bool IsCounterEnd(){
        return countdownRoot.CurrentCountValue == 0;
    }
}
