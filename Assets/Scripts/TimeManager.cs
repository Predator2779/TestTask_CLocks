using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] [Range(-11, 11)] private int timezoneOffset;
    
    private ServerTime _serverTime;
    
    private void Awake()
    {
        _serverTime = new ServerTime(url, timezoneOffset);
        GetCurrentTime();
    }

    public UniTask<DateTime> GetCurrentTime() => _serverTime.GetCurrentTime();
}