using Environment;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CustomDataHandler : MonoBehaviour
{
    [HideInInspector] public bool useCustomData;
    [HideInInspector] public ChangeableData changeableData;
}

#if UNITY_EDITOR
    [CustomEditor(typeof(CustomDataHandler), true)]
    [CanEditMultipleObjects]
    public class CustomDataHandlerEditor : Editor
    {
        private SerializedObject _object;
        private bool _show;
        
        public void OnEnable()
        {
            _object = new SerializedObject(target);
        }
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // for other non-HideInInspector fields

            CustomDataHandler script = (CustomDataHandler)target;

            // draw checkbox for the bool
            script.useCustomData = EditorGUILayout.Toggle("Use custom Data?", script.useCustomData);
            if (script.useCustomData) // if bool is true, show other fields
            {
                var changeableData = _object.FindProperty("changeableData");
                _show = EditorGUILayout.Foldout(_show, "Changeable Data");
                if (_show)
                {
                    var changeBack = changeableData.FindPropertyRelative("changeBack");
                    var isLoop = changeableData.FindPropertyRelative("isLoop");
                    var targetPosition = changeableData.FindPropertyRelative("targetPosition");
                    var targetRotation = changeableData.FindPropertyRelative("targetRotation");
                    var animCurve = changeableData.FindPropertyRelative("animCurve");
                    var duration = changeableData.FindPropertyRelative("duration");
                    var pauseDuration = changeableData.FindPropertyRelative("pauseDuration");
                    EditorGUILayout.PropertyField(changeBack, true);
                    EditorGUILayout.PropertyField(isLoop, true);
                    EditorGUILayout.PropertyField(targetPosition, true);
                    EditorGUILayout.PropertyField(targetRotation, true);
                    EditorGUILayout.PropertyField(animCurve, true);
                    EditorGUILayout.PropertyField(duration, true);
                    EditorGUILayout.PropertyField(pauseDuration, true);
                }

                _object.ApplyModifiedProperties();
            }
        }
    }
#endif