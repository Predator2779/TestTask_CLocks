using TMPro;
using UnityEngine;

namespace Clock
{
    public class TimezoneChanger : MonoBehaviour
    {
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private TMP_Text timezoneDisplay;
        
        public void ChangeTimezone(float timezone)
        {
            timeManager.TimezoneOffset = (int)timezone;
            var sign = Mathf.Sign(timeManager.TimezoneOffset) >= 0 ? "+" : "-";
            timezoneDisplay.text = sign + timeManager.TimezoneOffset;
        }
    }
}