using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardPreset))]
public class CardPresetEditor : Editor
{
    private SerializedProperty power;
    private SerializedProperty effect;
    private SerializedProperty direction;
    private SerializedProperty material;

    private string[] directionStr = { "Up", "Down", "Left", "Right", "Up Right", "Up Left", "Down Right", "Down Left" };

    private void OnEnable()
    {
        power = serializedObject.FindProperty("power");
        effect = serializedObject.FindProperty("effect");
        direction = serializedObject.FindProperty("direction");
        material = serializedObject.FindProperty("texture");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(material);
        EditorGUILayout.PropertyField(power);
        EditorGUILayout.PropertyField(effect);
        direction.intValue = EditorGUILayout.MaskField("Direction", direction.intValue, directionStr);

        serializedObject.ApplyModifiedProperties();

    }
}
