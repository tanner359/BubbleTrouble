using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{

    public Texture2D pointSprite;
    public bool edit;
    private static GUIStyle buttonStyle;
    private static GUIStyle buttonStyleToggled;
    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        

        Enemy enemy = (Enemy)target;

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Edit Points", GUILayout.Width(75), GUILayout.Height(30)))
        {
            edit = !edit;
            Debug.Log(edit);
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();       
    }

    static Vector2 lastPos;

    private void OnSceneGUI()
    {
        if (EditorApplication.isPlaying)
        {
            Debug.Log(edit);
            edit = false;
        }

        Enemy enemy = (Enemy)target;
        Vector2 enemyPos = enemy.transform.position;

        for (int i = 0; i < enemy.GetLocations().Count; i++)
        {           
            Handles.color = Color.red;
            
            enemy.GetLocations()[i] = Handles.PositionHandle(enemy.GetLocations()[i], Quaternion.identity);
            if(i > enemy.speeds.Count-1)
            {
                enemy.speeds.Add(0);
            }
            else if (i < enemy.speeds.Count-1)
            {
                enemy.speeds.Remove(0);
            }

            if (Event.current.type == EventType.Repaint)
            {
                Handles.SphereHandleCap(i, enemy.GetLocations()[i], Quaternion.identity, 1, EventType.Repaint);
                if(i + 1 < enemy.GetLocations().Count)
                {
                    Handles.DrawLine(enemy.GetLocations()[i], enemy.GetLocations()[i + 1]);
                }               
            }
        }      
        if (enemyPos != lastPos && edit)
        {
            for(int i = 0; i < enemy.GetLocations().Count; i++)
            {
                enemy.GetLocations()[i] += (enemyPos - lastPos);
            }            
        }
        lastPos = enemyPos;      
    }  
}
