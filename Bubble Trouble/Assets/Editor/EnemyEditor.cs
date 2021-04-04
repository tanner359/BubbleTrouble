using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{

    public Texture2D pointSprite;
    public bool edit;
    public float handleSize = 2;
    private Enemy lastInspected;
    private Enemy enemy;
    static Vector2 lastPos;
    Vector2 newPos;

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        if ((Enemy)target != lastInspected)
        {
            enemy = (Enemy)target;
            lastPos = enemy.transform.position;
        }

        GUILayout.BeginArea(new Rect(10, 340, Screen.width-100, 30));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();     

        if (GUILayout.Button("Edit Locations", GUILayout.Width(Screen.width/2 - 100), GUILayout.Height(30)))
        {
            edit = !edit;
            EditorWindow view = EditorWindow.GetWindow<SceneView>();
            view.Repaint();           
        }       
        if (GUILayout.Button("Edit Boundry", GUILayout.Width(Screen.width / 2 - 100), GUILayout.Height(30)))
        {
            Debug.Log("edit boundry activated");
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        lastInspected = enemy;
    }

    public void OnSceneGUI()
    {
        newPos = enemy.transform.position;

        if (edit)
        {
            for (int i = 0; i < enemy.locations.Count; i++)
            {
                Handles.color = Color.green;
                if (i == enemy.locations.Count - 1)
                {
                    Handles.color = Color.red;
                }
                else if (i > 0)
                {
                    Handles.color = Color.yellow;
                }

                EditorGUI.BeginChangeCheck();
                Vector2 handlePos = Handles.FreeMoveHandle(enemy.locations[i], Quaternion.identity, handleSize, new Vector3(0, 0, 0), Handles.SphereHandleCap);               

                if (i > enemy.speeds.Count - 1)
                {
                    enemy.speeds.Add(0);
                }
                else if (i < enemy.speeds.Count - 1)
                {
                    enemy.speeds.Remove(0);
                }

                if (Event.current.type == EventType.Repaint)
                {
                    Handles.Label(enemy.locations[0] + Vector2.up * 2.5f + Vector2.left * 2f, "START");
                    Handles.Label(enemy.locations[enemy.locations.Count - 1] + Vector2.up * 2.5f + Vector2.left * 2f, "STOP");
                    Handles.SphereHandleCap(i, enemy.locations[i], Quaternion.identity, handleSize, EventType.Repaint);

                    if (i + 1 < enemy.locations.Count)
                    {
                        Handles.color = Color.magenta;
                        Handles.DrawLine(enemy.locations[i], enemy.locations[i + 1], 2f);
                        Handles.Label((enemy.locations[i] + enemy.locations[i + 1]) / 2, "Speed: " + enemy.speeds[i]);
                    }
                }

                if(EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(enemy, "Locations Changed");
                    enemy.locations[i] = handlePos;
                    enemy.UpdateValues();
                }
            }            
            
        }
        if (newPos != lastPos && !enemy.attack)
        {
            for (int i = 0; i < enemy.locations.Count; i++)
            {
                enemy.locations[i] = enemy.locations[i] + (newPos - lastPos);
            }
        }
        lastPos = newPos;
    }  
}
