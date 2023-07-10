using System;
using System.Collections;
using System.Collections.Generic;
using Environment.Interactable;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class MovableObject : Changeable
{
    [SerializeField] private bool moveAtStart;
    [SerializeField] private bool moveBack;
    [SerializeField] private bool isLoop;
    private Vector3 _currentPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float moveDuration;
    [SerializeField] private float pauseDuration;
    
    public Vector3 CurrentPosition => _currentPosition;
    
    private void Start()
    {
        _currentPosition = transform.localPosition;
        
        if (moveAtStart)
            ChangeWith();
    }


    public override void Change()
    {
        StartCoroutine(BaseSetup());
    }
    
    public override void ChangeWith(Button interactable = null)
    {
        /*var startPosition = transform.localPosition;
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
        do
        {
            var startPosition = transform.localPosition;
            var addPosition = targetPosition;
            if (interactable && interactable.useCustomPosition)
            {
                addPosition = interactable.customPosition;
            }

            var endPosition = startPosition + addPosition;
            _currentPosition = endPosition;
            yield return Move(startPosition, endPosition, interactable);

            yield return new WaitForSeconds(pauseDuration);
            
        } while (isLoop);
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

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            timeElapsed += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        transform.localPosition = endPosition;
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

        IsChanging = isLoop;
    }

    public void TurnLoop(bool loop) => isLoop = loop;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerMesh"))
        {
            other.gameObject.transform.parent.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerMesh"))
        {
            other.gameObject.transform.parent.parent = PlayerManager.Instance.transform;
        }
    }
}
