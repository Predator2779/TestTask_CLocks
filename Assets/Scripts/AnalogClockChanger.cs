using System;
using UnityEngine;

public class AnalogClockChanger : MonoBehaviour
{
    [SerializeField] private TimeKeeper timeKeeper;
    [SerializeField] private AnalogClockDisplay analogClockDisplay;

    private bool _isMidday;
    
    private void OnEnable()
    {
        analogClockDisplay.hourHand.OnDragStart += DragStart;
        analogClockDisplay.hourHand.OnDragEnd += DragEnd;
        analogClockDisplay.hourHand.OnTwelveHourPassed += ChangeHoursFormat;

        analogClockDisplay.minuteHand.OnDragStart += DragStart;
        analogClockDisplay.minuteHand.OnDragEnd += DragEnd;

        analogClockDisplay.secondHand.OnDragStart += DragStart;
        analogClockDisplay.secondHand.OnDragEnd += DragEnd;
    }

    private void DragStart()
    {
        analogClockDisplay.CanDisplayed = false;
    }

    private void DragEnd()
    {
        timeKeeper.CurrentTime = GetChangedTime();
        analogClockDisplay.CanDisplayed = true;
    }

    private void ChangeHoursFormat() => _isMidday = timeKeeper.CurrentTime.Hour < 12;

    private DateTime GetChangedTime()
    {
        float hourAngle = Mathf.Abs(analogClockDisplay.hourHand.transform.localEulerAngles.z - 360);
        float minuteAngle = Mathf.Abs(analogClockDisplay.minuteHand.transform.localEulerAngles.z - 360);
        float secondAngle = Mathf.Abs(analogClockDisplay.secondHand.transform.localEulerAngles.z - 360);
        
        int hours = Mathf.RoundToInt(hourAngle / GlobalConstants.HoursDegree) % 12;
        int minutes = Mathf.RoundToInt(minuteAngle / GlobalConstants.MinutesDegree) % 60;
        int seconds = Mathf.RoundToInt(secondAngle / GlobalConstants.SecondsDegree) % 60;
        
        if (_isMidday) hours += 12;
        
        DateTime newTime = new DateTime(
            timeKeeper.CurrentTime.Year,
            timeKeeper.CurrentTime.Month,
            timeKeeper.CurrentTime.Day,
            hours,
            minutes,
            seconds
        );

        return newTime;
    }
}
