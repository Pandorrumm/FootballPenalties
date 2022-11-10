using UnityEngine;
using DG.Tweening;

public class GoalkeeperMovement : MonoBehaviour
{
    [SerializeField] private Transform goalkeeper;
    [SerializeField] private float duration;

    private Vector3 goalkeeperStartPosition;

    private Tweener tweener;

    private void OnEnable()
    {
        GoalkeeperInput.OnGoalkeeperStartedMoving += Move;
        KickResult.OnKickResultWindowIsClosed += RestartPosition;
    }

    private void OnDisable()
    {
        GoalkeeperInput.OnGoalkeeperStartedMoving -= Move;
        KickResult.OnKickResultWindowIsClosed -= RestartPosition;
    }

    private void Start()
    {
        goalkeeperStartPosition = goalkeeper.position;
    }

    private void Move(Vector3 _targetPosition)
    {
        tweener = goalkeeper.DOMoveX(_targetPosition.x, duration);
    }

    private void RestartPosition()
    {
        goalkeeper.position = goalkeeperStartPosition;
    }

    private void OnDestroy()
    {
        if (tweener != null)
        {
            tweener.Kill();
        }
    }
}

