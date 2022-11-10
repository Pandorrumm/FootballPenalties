using UnityEngine;
using System;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SoccerBall : MonoBehaviour
{
    private CircleCollider2D _circleCollider2D;
    private Rigidbody2D _rigidbody2D;

    public bool isGoal { private get; set; } = false;

    public static Action<Collision2D> OnTouchObstacle;
    public static Action<int> OnPlayerGotPoints;
    public static Action OnStopSoccerBallMovement;

    public CircleCollider2D CircleCollider2D
    {
        get { return _circleCollider2D ??= GetComponent<CircleCollider2D>(); }
    }

    public Rigidbody2D Rigidbody2D
    {
        get { return _rigidbody2D ??= GetComponent<Rigidbody2D>(); }
    }

    private void Start()
    {
        CircleCollider2D.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<GoalPost>() || col.collider.GetComponent<Goalkeeper>())
        {
            OnTouchObstacle?.Invoke(col);
        }

        if (col.collider.GetComponent<SoccerNet>() != null)
        {
            if (!isGoal)
            {
                CircleCollider2D.enabled = false;

                OnStopSoccerBallMovement?.Invoke();
                OnPlayerGotPoints?.Invoke(1);

                isGoal = true;
            }
        }
    }

    private void OnBecameInvisible()
    {
        OnStopSoccerBallMovement?.Invoke();
        OnPlayerGotPoints?.Invoke(0);
        CircleCollider2D.enabled = false;
    }
}

