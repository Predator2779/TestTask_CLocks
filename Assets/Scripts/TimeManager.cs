using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TimeKeeper timeKeeper;
    [SerializeField] private string url;
    [SerializeField] [Range(-11, 11)] private int timezoneOffset;

    private ServerTime _serverTime;
    private DateTime _currentTime;
    
    private void Awake()
    {
        _serverTime = new ServerTime(url, timezoneOffset);
        UpdateTimeFromServer();
    }

    private void FixedUpdate()
    {
        _currentTime = _currentTime.AddSeconds(Time.fixedDeltaTime);
        timeKeeper.CurrentTime = _currentTime;
    }

    [ContextMenu("Update Time")]
    public async void UpdateTimeFromServer()
    {
        _currentTime = await _serverTime.GetCurrentTime();
        timeKeeper.CurrentTime = _currentTime;            
    }
}
