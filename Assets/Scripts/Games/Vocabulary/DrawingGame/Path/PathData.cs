using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathData", menuName = "Path/PathData")]
public class PathData : ScriptableObject
{
    public List<Vector2> Waypoints;

    public void Capture(Transform t)
    {
        Waypoints = new List<Vector2>();
        foreach (Transform c in t)
        {
            Waypoints.Add(c.position);
        }
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }
}
