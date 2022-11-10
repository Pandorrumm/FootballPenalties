using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class SoccerBallInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    private Vector3 fingerPosition;
    private BoxCollider2D _boxCollider2D;

    public static Action OnBallSelect;
    public static Action<Vector3> OnChoosingPushDirection;
    public static Action OnFinishedChoosingPushDirection;

    private void OnEnable()
    {
        KickResult.OnKickResultWindowIsClosed += EnableCollider;
    }

    private void OnDisable()
    {
        KickResult.OnKickResultWindowIsClosed -= EnableCollider;
    }

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnBallSelect?.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        fingerPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        OnChoosingPushDirection?.Invoke(fingerPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnFinishedChoosingPushDirection?.Invoke();

        _boxCollider2D.enabled = false;
    }

    private void EnableCollider()
    {
        _boxCollider2D.enabled = true;
    }
}

