using System.Collections;
using Environment.Interactable;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class RotatableRigidbodyObject : Changeable
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

    [SerializeField] private Rigidbody objectRigidbody;
    
    private void Start()
    {
        var objectTransform = transform;
        objectTransform.eulerAngles = currentRotation;
        objectRigidbody.rotation = objectTransform.rotation;
        if (rotateAtStart)
            ChangeWith();
    }

    public override void Change()
    {
        StartCoroutine(BaseSetup());
    }
    
    public override void ChangeWith(Button interactable = null)
    {
        
        StartCoroutine(BaseSetup(interactable));
    }

    

    private IEnumerator BaseSetup(Button interactable = null)
    {
        do
        {
            var startRotation = objectRigidbody.rotation;
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

            objectRigidbody.MoveRotation(Quaternion.Lerp(startRotation, endRotation, t));
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        objectRigidbody.rotation = endRotation;
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
