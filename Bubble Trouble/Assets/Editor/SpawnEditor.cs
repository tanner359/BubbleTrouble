using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Spawn))]
//[CanEditMultipleObjects]
//public class SpawnEditor : Editor
//{
//    Spawn spawner;
//    void OnEnable()
//    {
//        spawner = (Spawn)target;
//    }
//    public override void OnInspectorGUI()
//    {
//        spawner.wave = (Spawn.Wave)EditorGUILayout.EnumPopup("Wave", spawner.wave);
//        switch (spawner.wave)
//        {
//            case Spawn.Wave.Wave_1:
//                {
//                    var serializedObject = new SerializedObject(target);
//                    serializedObject.Update();
//                    EditorGUILayout.PropertyField(serializedObject.FindProperty("W1SpawnInterval"), true);
//                    EditorGUILayout.PropertyField(serializedObject.FindProperty("wave1Enemies"), true);
//                    serializedObject.ApplyModifiedProperties();
//                    break;
//                }
//            case Spawn.Wave.Wave_2:
//                {
//                    var serializedObject = new SerializedObject(target);
//                    serializedObject.Update();
//                    EditorGUILayout.PropertyField(serializedObject.FindProperty("W2SpawnInterval"), true);
//                    EditorGUILayout.PropertyField(serializedObject.FindProperty("wave2Enemies"), true);
//                    serializedObject.ApplyModifiedProperties();
//                    break;
//                }
//            case Spawn.Wave.Wave_3:
//                {
//                    var serializedObject = new SerializedObject(target);
//                    var spawnList = serializedObject.FindProperty("W3SpawnInterval");
//                    serializedObject.Update();
//                    EditorGUILayout.PropertyField(spawnList, true);
//                    EditorGUILayout.PropertyField(serializedObject.FindProperty("wave3Enemies"), true);
//                    //for (int i = 0; i < spawn.wave3Enemies.Count; i++)
//                    //{
//                    //    spawnList.GetArrayElementAtIndex(i).FindPropertyRelative("gameObject").objectReferenceValue = EditorGUILayout.ObjectField("GameObject", spawn.wave3Enemies[i].gameObject, typeof(GameObject), true);
//                    //    spawnList.GetArrayElementAtIndex(i).FindPropertyRelative("spawnPoint").vector3Value = EditorGUILayout.Vector3Field("SpawnPoint", spawn.wave3Enemies[i].spawnPoint.transform.position);
//                    //}
//                    serializedObject.ApplyModifiedProperties();
//                    break;
//                }
//            case Spawn.Wave.Boss:
//                {
//                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Boss"), true);
//                    break;
//                }
//        }
//    }
//}
