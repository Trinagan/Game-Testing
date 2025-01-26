using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[CustomEditor(typeof(EnemyController))]
public class EnemyTypesEditor : Editor
{
    private SerializedProperty enemyTypeProperty;

    private void OnEnable()
    {
        enemyTypeProperty = serializedObject.FindProperty("enemyType");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(enemyTypeProperty, new GUIContent("Enemy Type"));
        EditorGUILayout.Space();

        EnemyController.EnemyTypes enemyType = (EnemyController.EnemyTypes)enemyTypeProperty.enumValueIndex;
        

        switch (enemyType)
        {
            case EnemyController.EnemyTypes.SoliderAI:
                DisplaySoldierVars();
                break;

            case EnemyController.EnemyTypes.ScientistAI:
                DisplayScientistVars();
                break;
            
            case EnemyController.EnemyTypes.AlienAI:
                DisplayAlienVars();
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }

    void DisplaySoldierVars()
    {
        EditorGUILayout.LabelField("Navmesh debug", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), new GUIContent("Player target"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("detector"), new GUIContent("Enemy Collider detector"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("stoppingDistance"), new GUIContent("Distance from player"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerLayer"), new GUIContent("Players layer"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("wallLayer"), new GUIContent("Walls layer"));
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Navmesh options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("minStoppingDistance"), new GUIContent("Minimum distance"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxStoppingDistance"), new GUIContent("Maximum distance"));
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bullet"), new GUIContent("Projectile"));
        SerializedProperty dealDamage = serializedObject.FindProperty("dealDamage");
        dealDamage.boolValue = true;
        EditorGUILayout.PropertyField(dealDamage, new GUIContent("Deal damage to player?"));
        if (dealDamage.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contactDamage"), new GUIContent("Enemy contact damage"));
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("health"), new GUIContent("Enemy max health"));
    }

    void DisplayScientistVars()
    {
        EditorGUILayout.LabelField("Navmesh debug", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), new GUIContent("Player target"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("detector"), new GUIContent("Enemy Collider detector"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerLayer"), new GUIContent("Players layer"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("wallLayer"), new GUIContent("Walls layer"));
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Navmesh options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fleeDistance"), new GUIContent("Distance to keep"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fleeAngleSteps"), new GUIContent("Direction check amount"));
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage options", EditorStyles.boldLabel);
        SerializedProperty dealDamage = serializedObject.FindProperty("dealDamage");
        dealDamage.boolValue = false;
        EditorGUILayout.PropertyField(dealDamage, new GUIContent("Deal damage to player?"));
        if (dealDamage.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contactDamage"), new GUIContent("Enemy contact damage"));
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("health"), new GUIContent("Enemy max health"));
    }

    void DisplayAlienVars()
    {
        EditorGUILayout.LabelField("Navmesh debug", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), new GUIContent("Player target"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("detector"), new GUIContent("Enemy Collider detector"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("stoppingDistance"), new GUIContent("Distance from player"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playerLayer"), new GUIContent("Players layer"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("wallLayer"), new GUIContent("Walls layer"));
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage options", EditorStyles.boldLabel);
        SerializedProperty dealDamage = serializedObject.FindProperty("dealDamage");
        dealDamage.boolValue = true;
        EditorGUILayout.PropertyField(dealDamage, new GUIContent("Deal damage to player?"));
        if (dealDamage.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contactDamage"), new GUIContent("Enemy contact damage"));
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("health"), new GUIContent("Enemy max health"));
    }
}
