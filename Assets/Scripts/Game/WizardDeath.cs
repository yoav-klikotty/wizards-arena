using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardDeath : MonoBehaviour
{
    SessionManager _sessionManager;

    // Start is called before the first frame update
    void Start()
    {
        _sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Leave()
    {
        StartCoroutine(_sessionManager.HandleSessionEndEvent());
    }
}
