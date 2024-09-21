using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private ClockDisplay[] clocks;
    [SerializeField] private TimeKeeper timeKeeper;
    [SerializeField] private string url;
    [SerializeField] [Range(-11, 11)] private int timezoneOffset;
    
    private ServerTime _serverTime;
    private DateTime _lastUpdate;

    private void Awake()
    {
        _serverTime = new ServerTime("https://" + url, timezoneOffset);
        UpdateTimeFromServer();
        
        foreach (var clock in clocks)
            clock.CanDisplayed = true;
    }

    private void FixedUpdate()
    {
        if (timeKeeper.CurrentTime.Hour - _lastUpdate.Hour >= 1)
            UpdateTimeFromServer();
    }

    [ContextMenu("Update Time")]
    public async void UpdateTimeFromServer()
    {
        _lastUpdate = timeKeeper.CurrentTime;
        timeKeeper.CurrentTime = await _serverTime.GetCurrentTime();
        Debug.Log("Time updated");
    }
}