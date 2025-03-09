using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(UISetup.InspectorButtonAttribute))]
public class InspectorButtonDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        UISetup.InspectorButtonAttribute buttonAttribute = attribute as UISetup.InspectorButtonAttribute;

        if (GUI.Button(position, "Setup UI"))
        {
            var target = property.serializedObject.targetObject;
            var methodInfo = target.GetType().GetMethod(buttonAttribute.MethodName);
            methodInfo?.Invoke(target, null);
        }
    }
}
