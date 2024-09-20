using UnityEngine;

public class AnalogClocks : MonoBehaviour
{
    [SerializeField] private TimeKeeper timeKeeper;
    [SerializeField] private Transform hourHand, minuteHand, secondHand;

    private const float hoursDegree = 360f / 12f, minutesDegree = 360f / 60f, secondsDegree = 360f / 60f;

    private void FixedUpdate()
    {
        var time = timeKeeper.CurrentTime;
        RotateHand(hourHand, time.Hour * -hoursDegree);
        RotateHand(minuteHand, time.Minute * -minutesDegree);
        RotateHand(secondHand, time.Second * -secondsDegree);
    }

    private void RotateHand(Transform hand, float angle) =>
        hand.localRotation = Quaternion.Euler(0f, 0f, angle);
}