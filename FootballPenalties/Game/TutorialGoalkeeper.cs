using UnityEngine;
using DG.Tweening;

public class TutorialGoalkeeper : MonoBehaviour
{
    [SerializeField] private GameObject objectContainer;

    [Space]
    [SerializeField] private Transform hand;
    [SerializeField] private Transform target;

    [Space]
    [SerializeField] private float durationMovement;

    private const string SAVE_KEY = "TutorialGoalkeeper";

    private void OnEnable()
    {
        KickResult.OnKickResultWindowIsClosed += StartTutorial;
    }

    private void OnDisable()
    {
        KickResult.OnKickResultWindowIsClosed -= StartTutorial;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(SAVE_KEY) == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            objectContainer.SetActive(false);
        }
    }

    private void StartTutorial()
    {
        objectContainer.SetActive(true);
        hand.DOMove(target.position, durationMovement).SetLoops(2, LoopType.Yoyo).OnComplete(() => StopTutorial());
    }

    private void StopTutorial()
    {
        PlayerPrefs.SetInt(SAVE_KEY, 1);
        gameObject.SetActive(false);
    }
}

