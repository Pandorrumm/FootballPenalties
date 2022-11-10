using UnityEngine;

public class SoccerBallMovement : MonoBehaviour
{
    [SerializeField] private SoccerBall soccerBall;

    [Space]
    [SerializeField] private float speed;

    private Vector3 soccerBallStartPosition;
    private Vector2 directionOfKick;
    private bool isSoccerBallMove = false;

    private void OnEnable()
    {
        SoccerBallRotation.OnSetKickDirection += GetKickDirection;
        SoccerBall.OnTouchObstacle += TouchObstacle;
        SoccerBall.OnStopSoccerBallMovement += StopSoccerBallMovement;
        KickResult.OnKickResultWindowIsClosed += RestartSoccerBall;
    }

    private void OnDisable()
    {
        SoccerBallRotation.OnSetKickDirection -= GetKickDirection;
        SoccerBall.OnTouchObstacle -= TouchObstacle;
        SoccerBall.OnStopSoccerBallMovement -= StopSoccerBallMovement;
        KickResult.OnKickResultWindowIsClosed -= RestartSoccerBall;
    }

    private void Start()
    {
        soccerBallStartPosition = soccerBall.transform.position;
    }

    private void TouchObstacle(Collision2D obj)
    {
        if (directionOfKick == Vector2.zero)
        {
            directionOfKick = Vector2.down;
        }
        else
        {
            directionOfKick = Vector2.Reflect(directionOfKick, obj.contacts[0].normal);
        }
    }

    private void GetKickDirection(Vector2 _direction)
    {
        directionOfKick = _direction;

        StartMoveSoccerBall();
    }

    private void StartMoveSoccerBall()
    {
        isSoccerBallMove = true;
        soccerBall.CircleCollider2D.enabled = true;
    }

    private void FixedUpdate()
    {
        if (isSoccerBallMove)
        {
            MoveSoccerBall(directionOfKick);
        }
    }

    private void MoveSoccerBall(Vector2 _direction)
    {
        _direction.Normalize();

        if (_direction == Vector2.zero)
        {
            _direction = Vector2.up;
        }

        soccerBall.Rigidbody2D.MovePosition(soccerBall.Rigidbody2D.position + _direction * speed * Time.fixedDeltaTime);
    }

    private void StopSoccerBallMovement()
    {
        isSoccerBallMove = false;
    }

    private void RestartSoccerBall()
    {
        soccerBall.CircleCollider2D.enabled = false;
        soccerBall.Rigidbody2D.position = soccerBallStartPosition;
        soccerBall.isGoal = false;
    }
}

