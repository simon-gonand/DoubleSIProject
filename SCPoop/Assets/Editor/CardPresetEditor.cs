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
    private SerializedProperty texture;
    private SerializedProperty scpMesh;
    private SerializedProperty scpMaterial;
    private SerializedProperty cardName;
    private SerializedProperty cardType;
    private SerializedProperty cardDescription;
    private SerializedProperty cardImage;


    private string[] directionStr = { "Up", "Down", "Left", "Right", "Up Right", "Up Left", "Down Right", "Down Left" };

    private void OnEnable()
    {
        power = serializedObject.FindProperty("power");
        effect = serializedObject.FindProperty("effect");
        direction = serializedObject.FindProperty("direction");
        texture = serializedObject.FindProperty("texture");
        scpMesh = serializedObject.FindProperty("scpMesh");
        scpMaterial = serializedObject.FindProperty("scpMaterial");
        cardName = serializedObject.FindProperty("cardName");
        cardType = serializedObject.FindProperty("cardType");
        cardDescription = serializedObject.FindProperty("cardDescription");
        cardImage = serializedObject.FindProperty("cardImage");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(texture);
        EditorGUILayout.PropertyField(power);
        EditorGUILayout.PropertyField(effect);
        EditorGUILayout.PropertyField(scpMesh);
        EditorGUILayout.PropertyField(scpMaterial);
        EditorGUILayout.PropertyField(cardName);
        EditorGUILayout.PropertyField(cardType);
        EditorGUILayout.PropertyField(cardDescription);
        EditorGUILayout.PropertyField(cardImage);

        direction.intValue = EditorGUILayout.MaskField("Direction", direction.intValue, directionStr);

        serializedObject.ApplyModifiedProperties();

    }
}
