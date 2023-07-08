using System.Collections;
using System.Collections.Generic;
using Environment.Interactable;
using UnityEngine;
using UnityEngine.Serialization;

public class MovableObject : Changeable
{
    [SerializeField] private bool moveAtStart;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float moveDuration;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private bool moveBack;
    [SerializeField] private float pauseDuration;
    [SerializeField] private bool isLoop;

    public override bool IsChangingReadonly { get => isLoop || IsChanging; }

    private void Start()
    {
        if (moveAtStart)
            ChangeWith();
    }


    public override void Change()
    {
        
    }
    
    public override void ChangeWith(Button interactable = null)
    {
        /*var startPosition = transform.position;
        var addPosition = targetPosition;
        if (interactable && interactable.useCustomPosition)
        {
            addPosition = interactable.customPosition;
        }
        
        SetupMovement(startPosition, addPosition, interactable);*/
        StartCoroutine(BaseSetup(interactable));
    }

    private IEnumerator BaseSetup(Button interactable = null)
    {
        while (true)
        {
            var startPosition = transform.position;
            var addPosition = targetPosition;
            if (interactable && interactable.useCustomPosition)
            {
                addPosition = interactable.customPosition;
            }
        
            var endPosition = startPosition + addPosition;
            yield return Move(startPosition, endPosition, interactable);
            
            yield return new WaitForSeconds(pauseDuration);
            
            if (!isLoop)
            {
                yield break;
            }
        }
    }

    /*private IEnumerator SetupMovement(Vector3 startPosition, Vector3 addPosition, Button interactable = null)
    {
        var endPosition = startPosition + addPosition;
        yield return Move(startPosition, endPosition, interactable);
    }*/
    
    private IEnumerator Move(Vector3 startPosition, Vector3 endPosition, Button interactable = null)
    {
        IsChanging = true;
        float timeElapsed = 0;

        while (timeElapsed < moveDuration)
        {
            float t = timeElapsed / moveDuration;

            t = animCurve.Evaluate(t);

            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = endPosition;
        if (moveBack)
        {
            if (interactable && interactable.useCustomPosition)
            {
                interactable.customPosition = -interactable.customPosition;
            }
            else
            {
                targetPosition = -targetPosition;
            }
        }

        IsChanging = false;
    }
}
