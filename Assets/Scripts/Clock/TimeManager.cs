using System;
using UnityEngine;

namespace Clock
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private ClockChanger[] changers;
        [SerializeField] private TimeKeeper timeKeeper;
        [SerializeField] private string url;
        [SerializeField] [Range(-11, 11)] public int timezoneOffset;

        private ServerTime _serverTime;
        private DateTime _lastTime;

        public int TimezoneOffset
        {
            get => timezoneOffset;
            set => timezoneOffset = value;
        }

        private void Awake()
        {
            foreach (var changer in changers)
                changer.OnTimeChanged += UpdateLastUpdatedTime;

            _serverTime = new ServerTime("https://" + url, timezoneOffset);
            UpdateTimeFromServer();
        }

        private void FixedUpdate()
        {
            if (timeKeeper.CurrentTime.Hour - _lastTime.Hour >= 1)
                UpdateTimeFromServer();
        }

        [ContextMenu("Update Time")]
        public async void UpdateTimeFromServer()
        {
            UpdateLastUpdatedTime();
            timeKeeper.CurrentTime = await _serverTime.GetCurrentTime();
            Debug.Log("Time updated");
        }

        private void UpdateLastUpdatedTime() => _lastTime = timeKeeper.CurrentTime;
    }
}