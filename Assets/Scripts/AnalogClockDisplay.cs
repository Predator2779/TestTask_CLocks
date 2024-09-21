using System;
using UnityEngine;

public class AnalogClockDisplay : ClockDisplay
{
    public ClockHand hourHand, minuteHand, secondHand;

    public override void DisplayTime(DateTime time)
    {
        RotateHand(hourHand.transform, time.Hour * -GlobalConstants.HoursDegree);
        RotateHand(minuteHand.transform, time.Minute * -GlobalConstants.MinutesDegree);
        RotateHand(secondHand.transform, time.Second * -GlobalConstants.SecondsDegree);
    }

    private void RotateHand(Transform hand, float angle) =>
        hand.localRotation = Quaternion.Euler(0f, 0f, angle);
}