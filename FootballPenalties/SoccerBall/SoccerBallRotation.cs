using UnityEngine;
using System;
using DG.Tweening;

public class SoccerBallRotation : MonoBehaviour
{
    [SerializeField] private SoccerBall soccerBall;
    [SerializeField] private GameObject arrow;

    [Space]
    [SerializeField] private float durationRotating;

    private Vector2 directionOfKick;
    private Vector3 arrowStartPosition;

    public static Action<Vector2> OnSetKickDirection;

    private void OnEnable()
    {
        SoccerBallInput.OnBallSelect += ArrowEnable;
        SoccerBallInput.OnChoosingPushDirection += ChoosingKickDirection;
        SoccerBallInput.OnFinishedChoosingPushDirection += SetKickDirection;
        KickResult.OnKickResultWindowIsClosed += RestartRotation;
    }

    private void OnDisable()
    {
        SoccerBallInput.OnBallSelect -= ArrowEnable;
        SoccerBallInput.OnChoosingPushDirection -= ChoosingKickDirection;
        SoccerBallInput.OnFinishedChoosingPushDirection -= SetKickDirection;
        KickResult.OnKickResultWindowIsClosed -= RestartRotation;
    }

    private void ArrowEnable()
    {
        arrow.SetActive(true);
        arrowStartPosition = arrow.transform.position;
    }

    private void ChoosingKickDirection(Vector3 _direction)
    {
        directionOfKick = new Vector2(arrowStartPosition.x, arrowStartPosition.y) - new Vector2(_direction.x, _direction.y);
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, directionOfKick);
        soccerBall.transform.DORotateQuaternion(toRotation, durationRotating);
    }

    private void SetKickDirection()
    {
        arrow.SetActive(false);
        OnSetKickDirection?.Invoke(directionOfKick);
    }

    private void RestartRotation()
    {
        soccerBall.transform.rotation = Quaternion.identity;
        directionOfKick = Vector2.zero;
    }
}

