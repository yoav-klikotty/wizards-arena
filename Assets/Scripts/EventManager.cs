using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public event Action updatePlayerStats;
    public event Action updateWizardStats;
    public event Action levelUpgrade;

    public static EventManager Instance { get; private set; }
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

    public void UpdatePlayerStats()
    {
        updatePlayerStats?.Invoke();
    }
    public void UpdateWizardStats()
    {
        updateWizardStats?.Invoke();
    }
    public void LevelUpgrade()
    {
        Debug.Log("level upgrade");
        levelUpgrade?.Invoke();
    }
}
