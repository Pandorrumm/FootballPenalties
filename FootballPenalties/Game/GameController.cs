using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    [Space]
    [SerializeField] private List<Color> colorPlayers = new List<Color>();

    private int currentNumberOfKick = -1;
    private int indexCurrentPlayer = -1;
    private int totalNumberOfKick;

    public static Action<int> OnPlayerSelected;
    public static Action<Color> OnPlayerColorSelected;
    public static Action<int> OnPlayerRemainderKick;
    public static Action OnPlayersHaveMadeTheirMove;

    private void OnEnable()
    {
        KickResult.OnScoresShow += CountingKicks;
        ScoreCounter.OnGameIsDraw += ChangeTotalNumberOfKick;
    }

    private void OnDisable()
    {
        KickResult.OnScoresShow -= CountingKicks;
        ScoreCounter.OnGameIsDraw -= ChangeTotalNumberOfKick;
    }

    private void Awake()
    {
        totalNumberOfKick = gameSettings.NumberOfKick;
    }

    private void Start()
    {
        PlayerChange();
    }

    private void PlayerChange()
    {
        indexCurrentPlayer++;

        if (indexCurrentPlayer == colorPlayers.Count)
        {
            indexCurrentPlayer = 0;
            OnPlayersHaveMadeTheirMove?.Invoke();
        }

        OnPlayerSelected?.Invoke(indexCurrentPlayer);
        OnPlayerColorSelected?.Invoke(colorPlayers[indexCurrentPlayer]);
    }

    public Color GetPlayerColorByIndex(int _index)
    {
        return colorPlayers[_index];
    }

    private void CountingKicks()
    {
        currentNumberOfKick++;

        if (currentNumberOfKick == totalNumberOfKick)
        {
            PlayerChange();
            currentNumberOfKick = 0;
        }

        OnPlayerRemainderKick?.Invoke(totalNumberOfKick - currentNumberOfKick);
    }

    private void ChangeTotalNumberOfKick()
    {
        totalNumberOfKick = gameSettings.NumberOfKickAfterDraw;
    }
}

