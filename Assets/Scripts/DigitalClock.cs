using System;
using TMPro;
using UnityEngine;

public class DigitalClocks : ClockDisplay
{
    [SerializeField] private TMP_Text timeText;
    
    public override void DisplayTime(DateTime time) => timeText.text = time.ToString("HH:mm:ss");
}