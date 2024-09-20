using TMPro;
using UnityEngine;

public class DigitalClocks : MonoBehaviour
{
    [SerializeField] private TimeKeeper timeKeeper;
    [SerializeField] private TMP_Text timeText;

    private void FixedUpdate()
    {
        timeText.text = timeKeeper.CurrentTime.ToString("HH:mm:ss");
    }
}