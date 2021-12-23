using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class MonoBehaviourID : MonoBehaviour
{
    [SerializeField] private UniqueID _id;
    protected List<object> _parameters = new List<object>();

    protected abstract void Validate();

    public string ID
    {
        get { return _id.Value; }
    }

    [ContextMenu("Force reset ID")]
    private void ResetId()
    {
        _id.Value = GenerateUniqueID();
    }

    string GenerateUniqueID()
    {
        string name = "";
        foreach (var param in _parameters)
        {
            name += ((GameObject) param).name;
        }

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(name));
    }

    protected void OnValidate()
    {
        Validate();
        ResetId();
    }

    [Serializable]
    private struct UniqueID
    {
        public string Value;
    }

#if UNITY_EDITOR

    [UnityEditor.CustomPropertyDrawer(typeof(UniqueID))]
    private class UniqueIdDrawer : UnityEditor.PropertyDrawer
    {
        private const float buttonWidth = 120;
        private const float padding = 2;

        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = UnityEditor.EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            GUI.enabled = false;
            Rect valueRect = position;
            valueRect.width -= buttonWidth + padding;
            UnityEditor.SerializedProperty idProperty = property.FindPropertyRelative("Value");
            UnityEditor.EditorGUI.PropertyField(valueRect, idProperty, GUIContent.none);

            GUI.enabled = true;

            Rect buttonRect = position;
            buttonRect.x += position.width - buttonWidth;
            buttonRect.width = buttonWidth;
            if (GUI.Button(buttonRect, "Copy to clipboard"))
            {
                UnityEditor.EditorGUIUtility.systemCopyBuffer = idProperty.stringValue;
            }

            UnityEditor.EditorGUI.EndProperty();
        }
    }
#endif
}