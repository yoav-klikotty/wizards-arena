using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
public class LoginManager : MonoBehaviour
{
    public void Start()
    {
        Debug.Log(PlayFabSettings.staticSettings.TitleId);
        Debug.Log(SystemInfo.deviceType);
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
}
