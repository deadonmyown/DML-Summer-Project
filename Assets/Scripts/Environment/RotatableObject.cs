using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private float rotationDuration;
    [SerializeField] private AnimationCurve animCurve;
    private bool _isObjectRotated = false;
    private Quaternion _initialRotation;

    private void Start()
    {
        _initialRotation = transform.rotation;
        StartRotating();
    }

    public void StartRotating()
    {
        var startRotation = transform.rotation;
        StartCoroutine(Rotate(startRotation, Quaternion.Euler(targetRotation)));
    }

    private IEnumerator Rotate(Quaternion startRotation, Quaternion endRotation)
    {
        float timeElapsed = 0;

        while (timeElapsed < rotationDuration)
        {
            float t = timeElapsed / rotationDuration;

            t = animCurve.Evaluate(t);

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = endRotation;
        _isObjectRotated = true;
        targetRotation += targetRotation;
    }
    
    public void ResetRotationInfo() => _isObjectRotated = false;
}
