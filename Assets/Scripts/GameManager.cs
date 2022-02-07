using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool IsSFXOn = true;
    public int NumOfDeathmatchPlayers = 2;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetSFX()
    {
        this.IsSFXOn = !this.IsSFXOn;
    }
    
}
