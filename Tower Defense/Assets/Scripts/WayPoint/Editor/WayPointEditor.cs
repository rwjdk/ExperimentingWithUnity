using UnityEditor;
using UnityEngine;

namespace WayPoint.Editor
{
    [CustomEditor(typeof(WayPoint))]
    public class WayPointEditor : UnityEditor.Editor
    {
        private WayPoint WayPoint => target as WayPoint;

        private void OnSceneGUI()
        {
            Handles.color = Color.red;
            for (int i = 0; i < WayPoint.Points.Length; i++)
            {
                EditorGUI.BeginChangeCheck();

                var currentPosition = WayPoint.CurrentPosition + WayPoint.Points[i];
                var sizeOfHandle = 0.7f;
                var snap = new Vector3(0.3f, 0.3f, 0.3f);
                var handle = Handles.FreeMoveHandle(currentPosition, Quaternion.identity, sizeOfHandle, snap, Handles.SphereHandleCap);

                //Text on handle
                var textStyle = new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 16,
                    normal =
                    {
                        textColor = Color.white
                    }
                };

                //Show WayPoint number on Handle 
                var textAlignment = Vector3.down * 0.35f + Vector3.right * 0.35f;
                Handles.Label(WayPoint.CurrentPosition + WayPoint.Points[i] + textAlignment, i.ToString(), textStyle);

                EditorGUI.EndChangeCheck();

                if (EditorGUI.EndChangeCheck())
                {
                    //Update Editor Position
                    Undo.RecordObject(target, @"Free Move Handle");
                    WayPoint.Points[i] = handle - WayPoint.CurrentPosition;
                }
            }
        }
    }
}