using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clock.DigitalClock
{
    public class DigitalClockChanger : ClockChanger, IPointerClickHandler
    {
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private DigitalClockDisplay _digitalClockDisplay;

        private string inputTime = "";
        private const int maxLength = 6;
        private bool isEditing;

        private void Awake()
        {
            Observable.EveryUpdate()
                .Where(_ => isEditing)
                .Subscribe(_ => HandleInput());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StartInput();
        }

        private void StartInput()
        {
            inputTime = "";
            isEditing = true;
            _digitalClockDisplay.CanDisplayed = false;
            timeText.text = "HH:MM:SS";
        }

        private void EndInput()
        {
            isEditing = false;
            _digitalClockDisplay.CanDisplayed = true;
            OnTimeChanged?.Invoke();
        }

        private void HandleInput()
        {
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

                if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) &&
                    inputTime.Length == maxLength)
                {
                    SetTime();
                    EndInput();
                }
            }
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
                Debug.LogWarning("Incorrect time format.");
            }
        }
    }
}