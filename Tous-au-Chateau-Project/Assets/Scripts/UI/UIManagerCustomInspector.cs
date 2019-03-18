using System.Collections;
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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("controlPanel"));

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

        EditorGUILayout.LabelField(serializedObject.FindProperty("gameOverButton").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gameOverButton"), GUIContent.none);

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

        EditorGUILayout.LabelField(serializedObject.FindProperty("buildingNotPuchasable").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("buildingNotPuchasable"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("paginationButtonsPrefab").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("paginationButtonsPrefab"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("buttonsPosition").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonsPosition"), GUIContent.none);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField(serializedObject.FindProperty("constructionPosition1").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("constructionPosition1"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("constructionPosition2").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("constructionPosition2"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("constructionPosition3").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("constructionPosition3"), GUIContent.none);

        EditorGUILayout.LabelField(serializedObject.FindProperty("constructionPosition4").displayName);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("constructionPosition4"), GUIContent.none);

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }
}
