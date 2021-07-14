using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultResolver : MonoBehaviour
{
    // Start is called before the first frame update
    public Image OpponentChoice;
    public Image PlayerChoice;
    public Sprite Shield;
    public Sprite Ammo;
    public Sprite Shoot;
    public bool isResultResolverDone;

    public void ResetResultResolver(){
        isResultResolverDone = false;
    }

    public void RenderDecitions(DecitionManager.Option playerDecition, DecitionManager.Option opponentDecition){
        RenderDecition(opponentDecition, OpponentChoice);
        RenderDecition(playerDecition, PlayerChoice);
        InvokeRepeating("FinishResolving", 1, 0);
    }

    void RenderDecition(DecitionManager.Option decition, Image image){
        if (decition == DecitionManager.Option.Reload){
            image.sprite = Ammo;
        }
        else if (decition == DecitionManager.Option.Protect){
            image.sprite = Shield;
        }
        else {
            image.sprite = Shoot;
        }
    }
    void FinishResolving(){
        isResultResolverDone = true;
    }
}
