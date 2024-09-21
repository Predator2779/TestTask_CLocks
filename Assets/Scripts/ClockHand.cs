using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(RectTransform))]
public class ClockHand : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Action OnDragStart;
    public Action OnDragEnd;
    
    [SerializeField] private ClockHandType handType;
    [SerializeField] private float sensitivity = 0.1f;

    private bool isDragging;

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        OnDragStart?.Invoke();
        print("Pointer down.");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            var rotation = transform.rotation;
            var angle = rotation.eulerAngles.z - eventData.delta.x * sensitivity;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            print("Dragging.");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        SnapToNearestDivision();
        OnDragEnd?.Invoke();
        print("Pointer up.");
    }
    
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