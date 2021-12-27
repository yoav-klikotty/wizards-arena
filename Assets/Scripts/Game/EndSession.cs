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
    SessionManager.GameResult result;
    [SerializeField] SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] Material _victoryMaterial;
    [SerializeField] Material _defeatMaterial;
    
    [SerializeField] Animation _starsAnim;
    [SerializeField] bool _debug;
    [SerializeField] bool _isWon;

    void Start()
    {
        result = LocalStorage.GetLastSessionResult();
        if (!_debug)
        {
            _isWon = result == SessionManager.GameResult.Win;
        }
        RenderScore();
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

    void RenderScore()
    {
        RenderTextScore();
        RenderWizardScore();
        StartStarsAnim();
    }

    void RenderTextScore()
    {
        if (!_isWon)
        {
            Result.text = "Defeat";
        }
        else
        {
            Result.text = "Victory";
        }
    }
    void RenderWizardScore()
    {
        Material materialToAdd;
        if (!_isWon)
        {
            materialToAdd = _defeatMaterial;
        }
        else
        {
            materialToAdd = _victoryMaterial;
        }
        Material[] mts = 
        {
            materialToAdd
        };
        _skinnedMeshRenderer.materials = mts;

    }

    void StartStarsAnim()
    {
        if (_isWon)
        {
            _starsAnim.Play();
        }
    }

    
}
