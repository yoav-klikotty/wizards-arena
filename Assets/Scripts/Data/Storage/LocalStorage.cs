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

    public static void SaveWizardStatsData(WizardStatsData wizardStatsData)
    {
        PlayerPrefs.SetString("WizardStatsData", JsonUtility.ToJson(wizardStatsData));
    }
    public static WizardStatsData LoadWizardStatsData()
    {
        string WizardStatsDataRaw = PlayerPrefs.GetString("WizardStatsData");
        return JsonUtility.FromJson<WizardStatsData>(WizardStatsDataRaw);
    }

    public static void SavePlayerStatsData(PlayerStatsData playerStatsData)
    {
        PlayerPrefs.SetString("PlayerStatsData", JsonUtility.ToJson(playerStatsData));
    }
    public static PlayerStatsData LoadPlayerStatsData()
    {
        string PlayerStatsDataRaw = PlayerPrefs.GetString("PlayerStatsData");
        return JsonUtility.FromJson<PlayerStatsData>(PlayerStatsDataRaw);
    }
    public static void SetDashboardPage(int page)
    {
        PlayerPrefs.SetInt("dashboardPage", page);
    }
    public static int GetDashboardPage()
    {
        return PlayerPrefs.GetInt("dashboardPage");
    }
}
