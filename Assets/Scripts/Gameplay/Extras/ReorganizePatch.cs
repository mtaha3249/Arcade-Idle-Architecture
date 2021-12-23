using UnityEngine;

public class ReorganizePatch : MonoBehaviour
{
#if UNITY_EDITOR
    public void Reorganize()
    {
        GameObject newParent = new GameObject(name);
        newParent.transform.position = transform.position;
        transform.parent = newParent.transform;
        UnityEditorInternal.ComponentUtility.CopyComponent(GetComponent<PatchInformation>());
        UnityEditorInternal.ComponentUtility.PasteComponentAsNew(newParent);
        DestroyImmediate(GetComponent<PatchInformation>());
        GameObject canvas = GetComponentInChildren<Canvas>().gameObject;
        UnityEditor.PrefabUtility.UnpackPrefabInstance(gameObject, UnityEditor.PrefabUnpackMode.OutermostRoot, UnityEditor.InteractionMode.AutomatedAction);
        canvas.transform.parent = newParent.transform;
        DestroyImmediate(this);
    }
#endif
}

#if UNITY_EDITOR

[UnityEditor.CustomEditor(typeof(ReorganizePatch))]
public class ReorganizeEditor : UnityEditor.Editor
{
    private ReorganizePatch _patch;
    public override void OnInspectorGUI()
    {
        _patch = (ReorganizePatch) target;
        if (GUILayout.Button("Organize Patch"))
        {
            _patch.Reorganize();
        }
    }
}
#endif