using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DigitalClockChanger : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TimeKeeper timeKeeper;
    [SerializeField] private DigitalClock digitalClock;

    private string inputTime = "";
    private const int maxLength = 6;
    private bool isEditing;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartInput();
    }

    private void Update()
    {
        if (!isEditing) return;

        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c) && inputTime.Length < maxLength)
            {
                inputTime += c;
                UpdateDisplay();
            }

            if (Input.GetKeyDown(KeyCode.Backspace) && inputTime.Length > 0)
            {
                inputTime = inputTime.Remove(inputTime.Length - 1);
                UpdateDisplay();
            }

            if (Input.GetKeyDown(KeyCode.Return) && inputTime.Length == maxLength)
            {
                SetTime();
                EndInput();
            }
        }
    }

    private void StartInput()
    {
        inputTime = "";
        isEditing = true;
        digitalClock.CanDisplayed = false;
        timeText.text = "HH:MM:SS";
    }

    private void EndInput()
    {
        isEditing = false;
        digitalClock.CanDisplayed = true;
    }

    private void UpdateDisplay()
    {
        string formattedTime = inputTime.PadRight(maxLength, '0');
        timeText.text =
            $"{formattedTime.Substring(0, 2)}:{formattedTime.Substring(2, 2)}:{formattedTime.Substring(4, 2)}";
    }

    private void SetTime()
    {
        if (DateTime.TryParseExact(inputTime, "HHmmss", null, System.Globalization.DateTimeStyles.None,
            out DateTime parsedTime))
        {
            timeKeeper.CurrentTime = new DateTime(
                timeKeeper.CurrentTime.Year,
                timeKeeper.CurrentTime.Month,
                timeKeeper.CurrentTime.Day,
                parsedTime.Hour,
                parsedTime.Minute,
                parsedTime.Second
            );
        }
        else
        {
            Debug.LogWarning("Incorrect time format");
        }
    }
}