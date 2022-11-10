using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class KickResult : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [Space]
    [SerializeField] private GameObject kickResultPanel;
    [SerializeField] private Button closeKickResult;

    [Header("InfoObjects")]
    [SerializeField] private GameObject remainderKick;
    [SerializeField] private GameObject endGameInfo;

    [Space]
    [SerializeField] private TMP_Text scoreFirstPlayer;
    [SerializeField] private TMP_Text scoreSecondPlayer;

    public static Action OnKickResultWindowIsClosed;
    public static Action OnScoresShow;
    public static Action<int> OnEndGameInfoActivated;

    private void Start()
    {
        closeKickResult.onClick.AddListener(CloseKickResult);
    }

    private void OnEnable()
    {
        ScoreCounter.OnPlayersReceivedPoints += GetScoreData;
        ScoreCounter.OnPlayerWin += EndGame;
    }

    private void OnDisable()
    {
        ScoreCounter.OnPlayersReceivedPoints -= GetScoreData;
        ScoreCounter.OnPlayerWin -= EndGame;
    }

    private void GetScoreData(int _firstPlayerScore, int _secondPlayerScore)
    {
        OpenKickResultPanel();

        scoreFirstPlayer.text = _firstPlayerScore.ToString();
        scoreSecondPlayer.text = _secondPlayerScore.ToString();

        OnScoresShow?.Invoke();
    }

    private void OpenKickResultPanel()
    {
        kickResultPanel.SetActive(true);
    }

    private void CloseKickResult()
    {
        kickResultPanel.SetActive(false);

        OnKickResultWindowIsClosed?.Invoke();
    }

    private void EndGame(int _index)
    {
        remainderKick.SetActive(false);
        endGameInfo.SetActive(true);

        if (_index >= 0)
        {
            closeKickResult.onClick.RemoveListener(CloseKickResult);
        }

        OnEndGameInfoActivated?.Invoke(_index);
    }
}

