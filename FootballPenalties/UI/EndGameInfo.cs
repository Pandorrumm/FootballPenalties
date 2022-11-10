using UnityEngine;
using TMPro;

public class EndGameInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text endGameText;

    [Space]
    [SerializeField] private GameObject restartGameButton;

    [Header("EndGameText")]
    [SerializeField] private string winText;
    [SerializeField] private string drawText;

    private void OnEnable()
    {
        KickResult.OnEndGameInfoActivated += AssignText;
    }

    private void OnDisable()
    {
        KickResult.OnEndGameInfoActivated -= AssignText;
    }

    private void Start()
    {
        restartGameButton.SetActive(false);
    }

    private void AssignText(int _index)
    {
        if (_index >= 0)
        {
            endGameText.text = winText;
            restartGameButton.SetActive(true);
        }
        else
        {
            endGameText.text = drawText;
        }
    }
}

