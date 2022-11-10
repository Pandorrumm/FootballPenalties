using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class GoalkeeperInput : MonoBehaviour, IDragHandler
{
    private Vector3 fingerPosition;

    public static Action<Vector3> OnGoalkeeperStartedMoving;

    public void OnDrag(PointerEventData eventData)
    {
        fingerPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        OnGoalkeeperStartedMoving?.Invoke(fingerPosition);
    }
}

