using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerImagesUpdater : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [Space]
    [SerializeField] private List<Image> playersImage;

    [Space]
    [SerializeField] private GameObject[] soccerBallIcons;

    [Space]
    [SerializeField] private Image victoryPlayerImage;

    private int currentPlayerIndex;

    private void OnEnable()
    {
        GameController.OnPlayerSelected += ActivateIconKickingPlayer;
        KickResult.OnEndGameInfoActivated += AssignWinnerColor;
    }

    private void OnDisable()
    {
        GameController.OnPlayerSelected -= ActivateIconKickingPlayer;
        KickResult.OnEndGameInfoActivated -= AssignWinnerColor;
    }

    private void Start()
    {
        AssignPlayerColors();
    }

    private void AssignPlayerColors()
    {
        for (int i = 0; i < playersImage.Count; i++)
        {
            playersImage[i].color = gameController.GetPlayerColorByIndex(i);
        }
    }

    private void ActivateIconKickingPlayer(int _index)
    {
        currentPlayerIndex = _index;

        for (int i = 0; i < soccerBallIcons.Length; i++)
        {
            soccerBallIcons[i].SetActive(false);
            soccerBallIcons[currentPlayerIndex].SetActive(true);
        }
    }

    private void AssignWinnerColor(int _index)
    {
        if (_index >= 0)
        {
            victoryPlayerImage.color = gameController.GetPlayerColorByIndex(_index);
        }
        else
        {
            victoryPlayerImage.color = Color.clear;
        }
    }
}

