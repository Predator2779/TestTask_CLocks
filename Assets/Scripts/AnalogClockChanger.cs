using System;
using UnityEngine;

public class AnalogClockChanger : MonoBehaviour
{
    [SerializeField] private TimeKeeper timeKeeper;
    [SerializeField] private AnalogClockDisplay analogClockDisplay;

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
        return timeKeeper.CurrentTime; ///////  логика считывания стрелок
    }
}