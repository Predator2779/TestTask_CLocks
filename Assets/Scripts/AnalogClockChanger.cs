using System;
using UnityEngine;

public class AnalogClockChanger : MonoBehaviour
{
    [SerializeField] private TimeKeeper timeKeeper;
    [SerializeField] private AnalogClockDisplay analogClockDisplay;

    private const float hoursDegree = 360f / 12f, minutesDegree = 360f / 60f, secondsDegree = 360f / 60f;

    private void OnEnable()
    {
        analogClockDisplay.hourHand.OnDragStart += DragStart;
        analogClockDisplay.hourHand.OnDragEnd += DragEnd;

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

    private DateTime GetChangedTime()
    {
        float hourAngle = Mathf.Abs(analogClockDisplay.hourHand.transform.localEulerAngles.z - 360);
        float minuteAngle = Mathf.Abs(analogClockDisplay.minuteHand.transform.localEulerAngles.z - 360);
        float secondAngle = Mathf.Abs(analogClockDisplay.secondHand.transform.localEulerAngles.z - 360);
        
        int hours = Mathf.RoundToInt(hourAngle / hoursDegree) % 12;
        int minutes = Mathf.RoundToInt(minuteAngle / minutesDegree) % 60;
        int seconds = Mathf.RoundToInt(secondAngle / secondsDegree) % 60;
        
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
