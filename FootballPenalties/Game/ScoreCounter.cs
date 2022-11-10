using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private int firstPlayerIndex;
    [SerializeField] private int secondPlayerIndex;

    private int scoreFirstPlayer;
    private int scoreSecondPlayer;
    private int currentPlayerIndex;

    public static Action<int, int> OnPlayersReceivedPoints;
    public static Action<int> OnPlayerWin;
    public static Action OnGameIsDraw;

    private void Start()
    {
        OnPlayersReceivedPoints?.Invoke(scoreFirstPlayer, scoreSecondPlayer);
    }

    private void OnEnable()
    {
        SoccerBall.OnPlayerGotPoints += GetScore;
        GameController.OnPlayerSelected += GetIndexCurrentPlayer;
        GameController.OnPlayersHaveMadeTheirMove += CheckingPointsToWin;
    }

    private void OnDisable()
    {
        SoccerBall.OnPlayerGotPoints -= GetScore;
        GameController.OnPlayerSelected -= GetIndexCurrentPlayer;
        GameController.OnPlayersHaveMadeTheirMove -= CheckingPointsToWin;
    }

    private void GetScore(int _value)
    {
        if (currentPlayerIndex == firstPlayerIndex)
        {
            scoreFirstPlayer += _value;
        }
        else if (currentPlayerIndex == secondPlayerIndex)
        {
            scoreSecondPlayer += _value;
        }

        OnPlayersReceivedPoints?.Invoke(scoreFirstPlayer, scoreSecondPlayer);
    }

    private void GetIndexCurrentPlayer(int _index)
    {
        currentPlayerIndex = _index;
    }

    private void CheckingPointsToWin()
    {
        if (scoreFirstPlayer > scoreSecondPlayer)
        {
            OnPlayerWin?.Invoke(firstPlayerIndex);
        }
        else if (scoreFirstPlayer < scoreSecondPlayer)
        {
            OnPlayerWin?.Invoke(secondPlayerIndex);
        }
        else if (scoreFirstPlayer == scoreSecondPlayer)
        {
            OnPlayerWin?.Invoke(-1);
            OnGameIsDraw?.Invoke();
        }
    }
}

