using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Clock.AnalogClock
{
    [RequireComponent(typeof(RectTransform))]
    public class ClockHand : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Action OnDragStart;
        public Action OnDragEnd;
        public Action OnTwelveHourPassed;

        [SerializeField] private ClockHandType handType;
        [SerializeField] private float sensitivity = 0.1f;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDragStart?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            RotateClockHand(eventData.delta);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SnapToNearestDivision();
            OnDragEnd?.Invoke();
        }

        private void RotateClockHand(Vector2 delta)
        {
            var rotation = transform.rotation;
            var angle = rotation.eulerAngles.z;
            angle -= delta.x * sensitivity;
            angle -= delta.y * sensitivity;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (IsInRange(transform.rotation.eulerAngles.z, 0, 20))
                OnTwelveHourPassed?.Invoke();
        }

        private bool IsInRange(float value, float target, float tolerance) =>
            value >= target - tolerance && value <= target + tolerance;

        private void SnapToNearestDivision()
        {
            float angle = transform.rotation.eulerAngles.z;
            float snappedAngle = GetSnappedAngle(angle);
            transform.rotation = Quaternion.Euler(0f, 0f, snappedAngle);
        }

        private float GetSnappedAngle(float angle)
        {
            switch (handType)
            {
                case ClockHandType.Hour:
                    return Mathf.Round(angle / GlobalConstants.HoursDegree) * GlobalConstants.HoursDegree;
                case ClockHandType.Minute:
                case ClockHandType.Second:
                    return Mathf.Round(angle / GlobalConstants.SecondsDegree) * GlobalConstants.SecondsDegree;
                default:
                    return angle;
            }
        }
    }

    public enum ClockHandType
    {
        Hour,
        Minute,
        Second
    }
}