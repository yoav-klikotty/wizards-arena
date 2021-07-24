using UnityEngine;

public class LocalStorage : MonoBehaviour
{
    public static SessionManager.GameResult GetLastSessionResult()
    {
        return (SessionManager.GameResult)PlayerPrefs.GetInt("LastResult");
    }

    public static void SetLastSessionResult(SessionManager.GameResult result)
    {
        PlayerPrefs.SetInt("LastResult", (int)result);
    }
}
