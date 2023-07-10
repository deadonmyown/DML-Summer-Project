using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PhysicsHandler : MonoBehaviour
{
    [HideInInspector] public bool useStartCustomGravity = false;
    [HideInInspector] public Vector3 customGravity;

    void Start()
    {
        if(useStartCustomGravity)
            Physics.gravity = customGravity;
        
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
    }
}
#endif
