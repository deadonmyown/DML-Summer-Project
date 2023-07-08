using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PhysicsHandler : MonoBehaviour
{
    [HideInInspector] public bool useStartCustomGravity = false;
    [HideInInspector] public Vector3 customGravity;
    [HideInInspector] public bool shouldRotateObjects;
    [HideInInspector] public GameObject rotateObjectsHandler;
    [HideInInspector] public Vector3 rotateObjectsDegree;
    [HideInInspector] public float rotationObjectsSpeed;
    [HideInInspector] public bool shouldRotateScene;
    [HideInInspector] public GameObject rotateSceneHandler;
    [HideInInspector] public Vector3 rotateSceneDegree;
    [HideInInspector] public float rotationSceneSpeed;
    private bool _isAllRotated = false;
    private bool _isSceneRotated = false;
    
    void Start()
    {
        if(useStartCustomGravity)
            Physics.gravity = customGravity;
        
    }

    void Update()
    {
        if (shouldRotateObjects && !_isAllRotated)
        {
            Debug.Log("Hello");
            RotateObjects(Quaternion.Euler(rotateObjectsDegree), rotationObjectsSpeed);
            CheckRotatedObjects(Quaternion.Euler(rotateObjectsDegree));
        }
        if (shouldRotateScene && !_isSceneRotated)
        {
            RotateScene(Quaternion.Euler(rotateSceneDegree), rotationSceneSpeed);
            CheckRotatedScene(Quaternion.Euler(rotateSceneDegree));
        }

        /*if (_isSceneRotated)
        {
            Physics.gravity = new Vector3(10, -10, 0);
        }*/
    }

    private void RotateObjects(Quaternion targetRotation, float speed)
    {
        Debug.Log("Entering rotating object");
        for (int i = 0; i < rotateObjectsHandler.transform.childCount; i++)
        {
            Debug.Log("Rotating...");
            var childObj = rotateObjectsHandler.transform.GetChild(i);
            var startRotation = childObj.localRotation;
            childObj.localRotation = Quaternion.Slerp(startRotation, targetRotation, Time.deltaTime * speed);
        }
    }

    private void CheckRotatedObjects(Quaternion targetRotation)
    {
        int lastChildCount = rotateObjectsHandler.transform.childCount;
        
        if (lastChildCount > 0)
        {
            var childObj = rotateObjectsHandler.transform.GetChild(lastChildCount - 1);
            if (childObj.localRotation == targetRotation)
            {
                _isAllRotated = true;
                rotateObjectsDegree += rotateObjectsDegree;
            }
        }
    }

    private void RotateScene(Quaternion targetRotation, float speed)
    {
        var startRotation = rotateSceneHandler.transform.rotation;
        rotateSceneHandler.transform.rotation =
            Quaternion.Slerp(startRotation, targetRotation, Time.deltaTime * speed);
    }

    private void CheckRotatedScene(Quaternion targetRotation)
    {
        if (rotateSceneHandler.transform.rotation == targetRotation)
        {
            _isSceneRotated = true;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PhysicsHandler))]
public class PhysicsHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        PhysicsHandler script = (PhysicsHandler)target;

        // draw checkbox for the bool
        script.useStartCustomGravity = EditorGUILayout.Toggle("Use custom Gravity from the start?", script.useStartCustomGravity);
        if (script.useStartCustomGravity) // if bool is true, show other fields
        {
            script.customGravity = EditorGUILayout.Vector3Field("Custom Gravity", script.customGravity);
        }
        
        script.shouldRotateObjects = EditorGUILayout.Toggle("Should rotate objects?", script.shouldRotateObjects);
        if (script.shouldRotateObjects)
        {
            script.rotateObjectsHandler =
                EditorGUILayout.ObjectField("Rotate Object Handler", script.rotateObjectsHandler, typeof(GameObject),
                    true) as GameObject;
            script.rotateObjectsDegree =
                EditorGUILayout.Vector3Field("Rotate objects degree", script.rotateObjectsDegree);
            script.rotationObjectsSpeed =
                EditorGUILayout.FloatField("Speed of objects rotation", script.rotationObjectsSpeed);
        }

        script.shouldRotateScene = EditorGUILayout.Toggle("Should rotate scene?", script.shouldRotateScene);
        if (script.shouldRotateScene)
        {
            script.rotateSceneHandler =
                EditorGUILayout.ObjectField("Rotate Scene Handler", script.rotateSceneHandler, typeof(GameObject),
                    true) as GameObject;
            script.rotateSceneDegree = EditorGUILayout.Vector3Field("Rotate scene degree", script.rotateSceneDegree);
            script.rotationSceneSpeed =
                EditorGUILayout.FloatField("Speed of scene rotation", script.rotationSceneSpeed);
        }
    }
}
#endif
