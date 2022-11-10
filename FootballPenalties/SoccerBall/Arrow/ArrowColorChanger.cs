using UnityEngine;

public class ArrowColorChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer arrowImage;

    private void OnEnable()
    {
        GameController.OnPlayerColorSelected += AssignArrowColor;
    }

    private void OnDisable()
    {
        GameController.OnPlayerColorSelected -= AssignArrowColor;
    }

    private void AssignArrowColor(Color _color)
    {
        arrowImage.color = _color;
    }
}

