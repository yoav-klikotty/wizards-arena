using UnityEngine;
using Lovatto.Countdown;

public class Counter : MonoBehaviour
{
    public bl_Countdown countdownRoot;
    bool isCountingOver = false;
    void Start()
    {
        countdownRoot.SetStartTime(120);
        countdownRoot.StartCountdown();
    }
    public void DoneCounting(){
        isCountingOver = true;
    }
    public bool IsCounterEnd(){
        return isCountingOver;
    }
}
