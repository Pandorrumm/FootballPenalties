using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;

    [Space]
    [SerializeField] private float durationRotating;

    [Space]
    [SerializeField] private List<Quaternion> anglesOfRotations;

    private Tweener tweener;

    private void OnEnable()
    {
        GameController.OnPlayerSelected += RotateCamera;
    }

    private void OnDisable()
    {
        GameController.OnPlayerSelected -= RotateCamera;
    }

    private void RotateCamera(int _index)
    {
        for (int i = 0; i < anglesOfRotations.Count; i++)
        {
            tweener = mainCamera.DORotateQuaternion(anglesOfRotations[_index], durationRotating);
        }
    }

    private void OnDestroy()
    {
        if (tweener != null)
        {
            tweener.Kill();
        }
    }
}

