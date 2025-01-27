using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyController))]
public class EnemyTypesEditor : Editor
{
    private SerializedProperty enemyTypeProperty;
    private SerializedProperty detectionRadius;
    private SerializedProperty dealDamage;

    private void OnEnable()
    {
        enemyTypeProperty = serializedObject.FindProperty("enemyType");
        detectionRadius = serializedObject.FindProperty("detectRange");
        dealDamage = serializedObject.FindProperty("dealDamage");
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
                detectionRadius.floatValue = 12f;
                dealDamage.boolValue = true;
                DisplaySoldierVars();
                break;

            case EnemyController.EnemyTypes.ScientistAI:
                detectionRadius.floatValue = 10f;
                dealDamage.boolValue = false;
                DisplayScientistVars();
                break;
            
            case EnemyController.EnemyTypes.AlienAI:
                detectionRadius.floatValue = 8f;
                dealDamage.boolValue = true;
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
        EditorGUILayout.PropertyField(detectionRadius);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bullet"), new GUIContent("Projectile"));
        EditorGUILayout.PropertyField(dealDamage, new GUIContent("Deal damage to player?"));
        if (dealDamage.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contactDamage"), new GUIContent("Enemy contact damage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeThreshold"), new GUIContent("Time to reapply damage"));
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
        EditorGUILayout.PropertyField(detectionRadius);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(dealDamage, new GUIContent("Deal damage to player?"));
        if (dealDamage.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contactDamage"), new GUIContent("Enemy contact damage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeThreshold"), new GUIContent("Time to reapply damage"));
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
        EditorGUILayout.LabelField("Navmesh options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(detectionRadius);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Damage options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(dealDamage, new GUIContent("Deal damage to player?"));
        if (dealDamage.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("contactDamage"), new GUIContent("Enemy contact damage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeThreshold"), new GUIContent("Time to reapply damage"));
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("health"), new GUIContent("Enemy max health"));
    }
}
