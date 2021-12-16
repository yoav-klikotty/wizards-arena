using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
public class EndSession : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text Result;
    [SerializeField] Button PlayAgainBtn;

    void Start()
    {
        SessionManager.GameResult result = LocalStorage.GetLastSessionResult();
        if (result == SessionManager.GameResult.Lose)
        {
            Result.text = "You Lose";
        }
        else
        {
            Result.text = "You Win";
        }
        InvokeRepeating("Disconnect", 1, 0);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Search");
    }

    void Disconnect(){
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        PlayAgainBtn.interactable = true;
    }

    
}
