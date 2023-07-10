using System.Collections;
using Environment.Interactable;
using UnityEngine;

public class RotatableObject : Changeable
{
    [SerializeField] private bool rotateAtStart;
    [SerializeField] private bool rotateBack;
    [SerializeField] private bool isLoop;
    [SerializeField] private Vector3 currentRotation;
    [SerializeField] private Vector3 targetRotation;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float rotationDuration;
    [SerializeField] private float pauseDuration;

    public Vector3 CurrentRotation => currentRotation;

    private void Start()
    {
        transform.eulerAngles = currentRotation;
        if (rotateAtStart)
            ChangeWith();
    }

    public override void Change()
    {
        StartCoroutine(BaseSetup());
    }
    
    public override void ChangeWith(Button interactable = null)
    {
        /*var startRotation = transform.rotation;
        var endVector = targetRotation;
        if (interactable && interactable.useCustomRotation)
        {
            endVector = interactable.customRotation;
        }
        
        SetupRotation(startRotation, endVector, interactable);*/
        StartCoroutine(BaseSetup(interactable));
    }

    /*private void SetupRotation(Quaternion startRotation, Vector3 endVector, Button interactable = null)
    {
        //Debug.Log(endVector);
        //Debug.Log($"before {currentRotation}");
        currentRotation += endVector;
        currentRotation.Set(currentRotation.x % 360, currentRotation.y % 360, currentRotation.z % 360);
        //Debug.Log($"after {currentRotation}");
        var endRotation = Quaternion.Euler(currentRotation);
        //Debug.Log($"finish strange result {endRotation.eulerAngles}");
        StartCoroutine(Rotate(startRotation, endRotation, interactable));
    }*/

    private IEnumerator BaseSetup(Button interactable = null)
    {
        do
        {
            var startRotation = transform.rotation;
            var endVector = targetRotation;
            if (interactable && interactable.useCustomRotation)
            {
                endVector = interactable.customRotation;
            }

            currentRotation += endVector;
            currentRotation.Set(currentRotation.x % 360, currentRotation.y % 360, currentRotation.z % 360);
            var endRotation = Quaternion.Euler(currentRotation);
            yield return Rotate(startRotation, endRotation, interactable);

            yield return new WaitForSeconds(pauseDuration);
            
        } while (isLoop);
    }

    private IEnumerator Rotate(Quaternion startRotation, Quaternion endRotation, Button interactable = null)
    {
        IsChanging = true;
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
        if (rotateBack)
        {
            if (interactable && interactable.useCustomRotation)
            {
                interactable.customRotation = -interactable.customRotation;
            }
            else
            {
                targetRotation = -targetRotation;
            }
        }

        IsChanging = isLoop;
    }
    
    public void TurnLoop(bool loop) => isLoop = loop;
}
