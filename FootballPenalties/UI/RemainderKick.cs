using UnityEngine;
using TMPro;

public class RemainderKick : MonoBehaviour
{
    [SerializeField] private TMP_Text remainderKickText;

    private void OnEnable()
    {
        GameController.OnPlayerRemainderKick += GetRemainderKick;
    }

    private void OnDisable()
    {
        GameController.OnPlayerRemainderKick -= GetRemainderKick;
    }

    private void GetRemainderKick(int _value)
    {
        remainderKickText.text = _value.ToString();
    }
}

