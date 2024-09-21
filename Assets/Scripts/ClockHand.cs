using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(RectTransform))]
public class ClockHand : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Action OnDragStart;
    public Action OnDragEnd; 
    
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
            var angle = rotation.z + eventData.delta.x * sensitivity;
            var newRotation = Quaternion.Euler(0f, 0f, angle);
            rotation = Quaternion.Euler(rotation.eulerAngles + newRotation.eulerAngles);
            transform.rotation = rotation;
            print("Dragging.");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        
        OnDragEnd?.Invoke();
        print("Pointer up.");
    }
}