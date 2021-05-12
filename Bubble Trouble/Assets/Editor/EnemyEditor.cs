using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Enemy), true)]
public class EnemyEditor : Editor
{

    public Texture2D pointSprite;
    public bool pathEdit = false;
    public bool boundryEdit = false;
    public float handleSize = 2;
    private Enemy lastInspected;
    private Enemy enemy;
    static Vector2 lastPos;
    Vector2 handlePos;

    Vector2 Bottom_L, Bottom_R, Top_L, Top_R;



    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();      

        GUILayout.BeginArea(new Rect(10, 340, Screen.width-100, 30));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();     

        if (GUILayout.Button("Edit Locations", GUILayout.Width(Screen.width/2 - 100), GUILayout.Height(30)))
        {
            pathEdit = !pathEdit;
            EditorWindow view = EditorWindow.GetWindow<SceneView>();
            view.Repaint();           
        }       
        if (GUILayout.Button("Add Location", GUILayout.Width(Screen.width / 2 - 100), GUILayout.Height(30)))
        {
            enemy.locations.Add(enemy.transform.position);
            enemy.speeds.Add(5f);
        }
        if(GUILayout.Button("Modify Boundry", GUILayout.Width(Screen.width / 2 - 100), GUILayout.Height(30)))
        {
            boundryEdit = !boundryEdit;
            EditorWindow view = EditorWindow.GetWindow<SceneView>();
            view.Repaint();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();       
    }

    public void OnSceneGUI()
    {
        if ((Enemy)target != lastInspected || enemy == null)
        {
            enemy = (Enemy)target;           
        }

        #region BOUNDRY MODIFIER

        if (boundryEdit)
        {
            Handles.color = Color.red;
            EditorGUI.BeginChangeCheck();
            Bottom_L = Handles.FreeMoveHandle(enemy.Bottom_L, Quaternion.identity, handleSize, new Vector3(-10, 0, 0), Handles.SphereHandleCap);
            Bottom_R = Handles.FreeMoveHandle(enemy.Bottom_R, Quaternion.identity, handleSize, new Vector3(10, 0, 0), Handles.SphereHandleCap);
            Top_L = Handles.FreeMoveHandle(enemy.Top_L, Quaternion.identity, handleSize, new Vector3(10, 10, 0), Handles.SphereHandleCap);
            Top_R = Handles.FreeMoveHandle(enemy.Top_R, Quaternion.identity, handleSize, new Vector3(-10, 10, 0), Handles.SphereHandleCap);

            if (Event.current.type == EventType.Repaint)
            {

                Handles.SphereHandleCap(0, enemy.Bottom_L, Quaternion.identity, handleSize, EventType.Repaint);
                Handles.SphereHandleCap(1, enemy.Bottom_R, Quaternion.identity, handleSize, EventType.Repaint);
                Handles.SphereHandleCap(2, enemy.Top_R, Quaternion.identity, handleSize, EventType.Repaint);
                Handles.SphereHandleCap(3, enemy.Top_L, Quaternion.identity, handleSize, EventType.Repaint);

                Handles.DrawLine(enemy.Bottom_L, enemy.Bottom_R);
                Handles.DrawLine(enemy.Bottom_R, enemy.Top_R);
                Handles.DrawLine(enemy.Top_R, enemy.Top_L);
                Handles.DrawLine(enemy.Top_L, enemy.Bottom_L);
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(enemy, "Boundry Changed");
                enemy.Bottom_L = Bottom_L;
                enemy.Bottom_R = Bottom_R;
                enemy.Top_L = Top_L;
                enemy.Top_R = Top_R;
                enemy.UpdateValues();
            }
        }
        #endregion

        #region PATH EDITOR
        if (pathEdit)
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

                handlePos = Handles.FreeMoveHandle(enemy.locations[i], Quaternion.identity, handleSize, new Vector3(0, 0, 0), Handles.SphereHandleCap);

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
                    Debug.Log("Draw Handles");
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

            if (enemy.locations[0] != (Vector2)enemy.transform.position && !enemy.attack)
            {
                Vector2 diff = (Vector2)enemy.transform.position - enemy.locations[0];
                enemy.locations[0] = enemy.transform.position;

                for (int i = 1; i < enemy.locations.Count; i++)
                {
                    enemy.locations[i] += diff;
                }
            }
        }
        #endregion
    }
}
