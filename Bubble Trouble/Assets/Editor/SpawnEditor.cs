using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawn))]
[CanEditMultipleObjects]
public class SpawnEditor : Editor
{
    Spawn spawn;
    void OnEnable()
    {
        spawn = (Spawn)target;
    }
    public override void OnInspectorGUI()
    {
        spawn.wave = (Spawn.Wave)EditorGUILayout.EnumPopup("Wave", spawn.wave);
        switch (spawn.wave)
        {
            case Spawn.Wave.Wave_1:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        spawn.wave1Enemies[i] = (GameObject)EditorGUILayout.ObjectField(spawn.wave1Enemies[i], typeof(GameObject), true);
                    }
                    break;
                }
            case Spawn.Wave.Wave_2:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        spawn.wave2Enemies[i] = (GameObject)EditorGUILayout.ObjectField(spawn.wave2Enemies[i], typeof(GameObject), true);
                    }
                    break;
                }
            case Spawn.Wave.Wave_3:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        spawn.wave3Enemies[i] = (GameObject)EditorGUILayout.ObjectField(spawn.wave3Enemies[i], typeof(GameObject), true);
                    }
                    break;
                }
            case Spawn.Wave.Boss:
                {
                    spawn.BossEnemy = (GameObject)EditorGUILayout.ObjectField("Boss", spawn.BossEnemy, typeof(GameObject), true);
                    break;
                }
        }
    }
}
