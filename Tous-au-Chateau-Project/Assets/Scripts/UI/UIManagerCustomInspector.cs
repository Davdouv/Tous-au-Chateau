﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIManager))]
[CanEditMultipleObjects]
public class UIManagerCustomInspector : Editor {

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_ResourceManager"));

        DisplayResources();

        DisplayGameOver();

        DisplayConstructionParts();

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayResources()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Resources Text Fields", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("woodTxt"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("stoneTxt"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("foodTxt"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("villagersTxt"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("motivation"));

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }

    private void DisplayGameOver()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Game Endings Management", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField(serializedObject.FindProperty("GameOverPanel").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("GameOverPanel"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("gameOverTitleText").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameOverTitleText"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("gameOverVillagersText").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameOverVillagersText"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("victoryTextColor").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("victoryTextColor"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("gameoverTextColor").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameoverTextColor"), GUIContent.none);

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }

    private void DisplayConstructionParts()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Construction Pagination", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField(serializedObject.FindProperty("_BuildingTypeGroup").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_BuildingTypeGroup"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("ConstructionPagination").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ConstructionPagination"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("defaultMaterial").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultMaterial"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("notEnoughResourceMaterial").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("notEnoughResourceMaterial"), GUIContent.none);

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }
}