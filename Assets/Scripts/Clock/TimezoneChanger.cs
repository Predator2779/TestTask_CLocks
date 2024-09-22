using System;
using TMPro;
using UnityEngine;

namespace Clock
{
    public class TimezoneChanger : MonoBehaviour
    {
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private TMP_Text timezoneDisplay;

        private void Awake()
        {
            UpdateDisplay();
        }

        public void ChangeTimezone(float timezone)
        {
            timeManager.TimezoneOffset = (int)timezone;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            var sign = Mathf.Sign(timeManager.TimezoneOffset) > 0 ? "+" : "";
            timezoneDisplay.text = sign + timeManager.TimezoneOffset;
        }
    }
}