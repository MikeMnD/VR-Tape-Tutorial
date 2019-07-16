using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestMeshViewer))]
public class MeshViewerEditor : Editor
{
    private void OnSceneGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        TestMeshViewer viewer = target as TestMeshViewer;
        Dictionary<Vector3, StringBuilder> posList = new Dictionary<Vector3, StringBuilder>();

        for (int i = 0, imax = viewer.verticesList.Count; i < imax; ++i)
        {
            Vector3 vPos = viewer.transform.TransformPoint(viewer.verticesList[i]);

            StringBuilder sb;
            if (posList.TryGetValue(vPos, out sb))
            {
                sb.AppendLine(i.ToString());
            }
            else
            {
                sb = new StringBuilder();
                sb.AppendLine(i.ToString());
                posList.Add(vPos, sb);

            }

            Handles.Label(vPos, sb.ToString(), style);
        }
    }
}