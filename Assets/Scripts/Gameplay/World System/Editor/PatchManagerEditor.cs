using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatchManager))]
public class PatchManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PatchManager patchManager = (PatchManager) target;
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Fetch Patches"))
        {
            patchManager.FetchPatchLinks();
        }
        
        if (GUILayout.Button("Disable Patches"))
        {
            patchManager.DisablePatches();
        }
        GUILayout.EndHorizontal();
    }
}
