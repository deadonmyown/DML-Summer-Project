using Environment.Interactable;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Button : MonoBehaviour, IInteractable
{
    [HideInInspector] public bool useCustomRotation;
    [HideInInspector] public Vector3 customRotation;
    [HideInInspector] public bool useCustomPosition;
    [HideInInspector] public Vector3 customPosition;
    [SerializeReference] private ChangeableHandler[] changeableHandlers;
    private int _changeableHandlersCount;

    private void Start()
    {
        if (changeableHandlers != null)
        {
            _changeableHandlersCount = changeableHandlers.Length;
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (CheckChangeableHandlers())
        {
            for (int i = 0; i < _changeableHandlersCount; i++)
            {
                changeableHandlers[i].ChangeWith(this);
            }

            return true;
        }

        return false;
    }

    private bool CheckChangeableHandlers()
    {
        for (int i = 0; i < _changeableHandlersCount; i++)
        {
            if (changeableHandlers[i].CheckOnChanging())
                return false;
        }

        return true;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Button))]
    public class ButtonRotatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // for other non-HideInInspector fields

            Button script = (Button)target;

            // draw checkbox for the bool
            script.useCustomRotation = EditorGUILayout.Toggle("Use custom rotation?", script.useCustomRotation);
            if (script.useCustomRotation) // if bool is true, show other fields
            {
                script.customRotation = EditorGUILayout.Vector3Field("Custom Rotation", script.customRotation);
            }
        
            script.useCustomPosition = EditorGUILayout.Toggle("Use custom position?", script.useCustomPosition);
            if (script.useCustomPosition)
            {
                script.customPosition = EditorGUILayout.Vector3Field("Custom Position", script.customPosition);
            }
        }
    }
#endif
}
