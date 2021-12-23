using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PatchInfo
{
    public GameObject _linker;
    public PatchInformation _linkerScript;
}

[Serializable]
public class Patch
{
    public PatchInfo _patchData;
    public Collider _collider;
}

[Serializable]
public class PatchLink
{
    public PatchInformation _currentPatchInfo;
    public List<Patch> _linkers;

    public PatchLink(PatchInformation currentPatchInfo, List<Patch> linkers)
    {
        _linkers = linkers;
        _currentPatchInfo = currentPatchInfo;
    }
}

[DefaultExecutionOrder(-10)]
public class PatchManager : MonoBehaviour
{
    [Tag, SerializeField] private string _tag;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _boundsCollider;
    [SerializeField] private List<PatchLink> _patches;
    [Header("Default")] [SerializeField] private Transform _startPatch;
    [Range(1, 4), SerializeField] private int _enabledLayer = 2;

    [Header("Price Handler")] [SerializeField]
    private string _patchID;

    [SerializeField] private GenericReference<int> _collectedCoin;
    [SerializeField] private GameEvent _collectedRaise;

    [Header("Gizmos")] [SerializeField] private bool _showGizmo;
    [ColorUsage(true)] [SerializeField] private Color _linkGizmoColor;
    [SerializeField] private Vector3 _offset;
    [Header("Editor")] [SerializeField] private bool applyPrefabImmediate;
    readonly List<Vector3> direction = new List<Vector3> {Vector3.right, Vector3.left, Vector3.forward, Vector3.back};

    private void Start()
    {
        AssignPatchPrice();
    }

    void AssignPatchPrice()
    {
        foreach (var patch in _patches)
        {
            patch._currentPatchInfo._price = DBManager.GetItemValue(_patchID, patch._currentPatchInfo._exploreIndex);
            if (patch._currentPatchInfo._hasItemMounted)
                patch._currentPatchInfo._price += DBManager.GetItemValue(patch._currentPatchInfo._mountedID, 0);
            patch._currentPatchInfo.Init();
        }

        foreach (var patch in _patches)
        {
            patch._currentPatchInfo.RefreshBounds();
            if (!patch._currentPatchInfo.isLocked)
                patch._currentPatchInfo.SetBoundingPatchVisibility();
        }
    }

    /// <summary>
    /// Editor Code Only
    /// </summary>
    public void FetchPatchLinks()
    {
#if UNITY_EDITOR
        ShowAllPatches();
        _patches = new List<PatchLink>();
        foreach (var _patch in FetchAllPatches())
        {
            List<Patch> linkedPatches = new List<Patch>();
            List<Collider> colliders = new List<Collider>();

            GameObject patch = _patch.transform.parent.gameObject;

            // find adjacent patches
            foreach (var _dir in direction)
            {
                colliders.AddRange(DetectFromSide(patch, _dir));
            }

            // add all adjacent patches
            foreach (var col in colliders)
            {
                if (col.transform.tag == _tag && col.transform != patch.transform)
                {
                    if (!col.transform.parent.gameObject.GetComponent<PatchInformation>())
                    {
                        col.transform.parent.gameObject.AddComponent<PatchInformation>();
                    }

                    Patch p = new Patch();
                    p._patchData = new PatchInfo();
                    p._patchData._linker = col.transform.parent.gameObject;
                    p._patchData._linkerScript = col.transform.parent.gameObject.GetComponent<PatchInformation>();

                    linkedPatches.Add(p);
                }
            }

            // caching my Info
            PatchInfo _myInfo = new PatchInfo();
            _myInfo._linker = patch;
            _myInfo._linkerScript = patch.GetComponent<PatchInformation>();

            // Find Bound Colliders
            for (int i = 0; i < linkedPatches.Count; i++)
            {
                linkedPatches[i]._collider = FindBoundCollider(patch,
                    (linkedPatches[i]._patchData._linker.transform.position - patch.transform.position).normalized);
                // add information
                if (linkedPatches[i]._collider.GetComponent<BoundInformation>())
                {
                    // Debug.Log(patch.name + " "+ linkedPatches[i].);
                    linkedPatches[i]._collider.GetComponent<BoundInformation>()._otherPatch =
                        linkedPatches[i]._patchData;
                    linkedPatches[i]._collider.GetComponent<BoundInformation>()._myPatch = _myInfo;
                }
                else
                {
                    BoundInformation _bi = linkedPatches[i]._collider.gameObject.AddComponent<BoundInformation>();
                    _bi._otherPatch = linkedPatches[i]._patchData;
                    _bi._myPatch = _myInfo;
                }
            }

            // Adding Component to Patch
            if (patch.GetComponent<PatchInformation>())
                patch.GetComponent<PatchInformation>()._linkers = linkedPatches;
            else
            {
                patch.AddComponent<PatchInformation>();
                patch.GetComponent<PatchInformation>()._linkers = linkedPatches;
            }

            patch.GetComponent<PatchInformation>()._resourceUI =
                patch.GetComponent<PatchInformation>()._FetchResourceUIView();
            patch.GetComponent<PatchInformation>()._currentCollected = _collectedCoin;
            patch.GetComponent<PatchInformation>()._collectedRaise = _collectedRaise;

            // Add path to patch Manager
            _patches.Add(new PatchLink(patch.GetComponent<PatchInformation>(), linkedPatches));
        }

        ApplyPrefab();
#endif
    }

    void ApplyPrefab()
    {
#if UNITY_EDITOR
        if (applyPrefabImmediate)
            UnityEditor.PrefabUtility.ApplyPrefabInstance(gameObject, UnityEditor.InteractionMode.UserAction);
#endif
    }

    /// <summary>
    /// Detect Joining Patch
    /// </summary>
    /// <param name="patch">Patch to find adjacent patches</param>
    /// <param name="side">Side to detect which is Right, Left, Forward, Back</param>
    /// <returns>List of patch connected on given side</returns>
    List<Collider> DetectFromSide(GameObject patch, Vector3 side)
    {
        Collider[] colliders =
            Physics.OverlapBox(patch.transform.position + (side * _offset.x) + (Vector3.up * _offset.y),
                _offset / 2, Quaternion.identity, _groundMask);
        List<Collider> _col = new List<Collider>();
        foreach (var collider in colliders)
        {
            if (collider != patch.transform.GetChild(0).GetComponent<Collider>())
            {
                _col.Add(collider);
            }
        }

        return _col;
    }

    /// <summary>
    /// Find Bound Collider
    /// </summary>
    /// <param name="patch">patch which bound collider to find</param>
    /// <param name="side">side to find</param>
    /// <returns>Bound Collider on the given side</returns>
    Collider FindBoundCollider(GameObject patch, Vector3 side)
    {
        RaycastHit hit;
        Physics.Raycast(patch.transform.position + (Vector3.up * _offset.y), side, out hit, Mathf.Infinity,
            _boundsCollider);
        return hit.collider;
    }

    /// <summary>
    /// Fetch all patches by tag
    /// </summary>
    /// <returns>Game object containing the tag</returns>
    GameObject[] FetchAllPatches()
    {
        return GameObject.FindGameObjectsWithTag(_tag);
    }

    /// <summary>
    /// Disable patches base on given layer
    /// </summary>
    public void DisablePatches()
    {
        foreach (var patch in _patches)
        {
            patch._currentPatchInfo._resourceUI = patch._currentPatchInfo._FetchResourceUIView();
            if (patch._currentPatchInfo._exploreIndex > _enabledLayer + 1)
            {
                patch._currentPatchInfo.isLocked = true;
                patch._currentPatchInfo.SetPatchVisibility();
                patch._currentPatchInfo.SetUIVisibility(false);
            }
            else if (patch._currentPatchInfo._exploreIndex > _enabledLayer)
            {
                patch._currentPatchInfo.isLocked = true;
                patch._currentPatchInfo.SetPatchVisibility();
                patch._currentPatchInfo.SetUIVisibility(true);
            }
            else
            {
                patch._currentPatchInfo.isLocked = false;
                patch._currentPatchInfo.SetPatchVisibility();
                patch._currentPatchInfo.SetUIVisibility(false);
            }
        }

        ApplyPrefab();
    }

    /// <summary>
    /// Enable all patches
    /// Must used in editor
    /// </summary>
    void ShowAllPatches()
    {
#if UNITY_EDITOR
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                if (transform.GetChild(i).GetChild(j).childCount > 0)
                {
                    transform.GetChild(i).GetChild(j).GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(i).GetChild(j).GetChild(1).gameObject.SetActive(true);
                }
            }
        }
#endif
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!_showGizmo)
            return;

        DefaultLinkGizmo();
        foreach (var patch in _patches)
        {
            GUI.color = _linkGizmoColor;
            if (patch._currentPatchInfo)
                UnityEditor.Handles.Label(patch._currentPatchInfo.transform.position + (Vector3.up * 0.2f),
                    patch._currentPatchInfo._exploreIndex.ToString());
        }
    }

    void DefaultLinkGizmo()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            _startPatch.position + (Vector3.right * _offset.x) + (Vector3.up * _offset.y),
            _offset);
        Gizmos.DrawWireCube(
            _startPatch.position + (Vector3.left * _offset.x) + (Vector3.up * _offset.y),
            _offset);
        Gizmos.DrawWireCube(
            _startPatch.position + (Vector3.forward * _offset.x) + (Vector3.up * _offset.y),
            _offset);
        Gizmos.DrawWireCube(
            _startPatch.position + (Vector3.back * _offset.x) + (Vector3.up * _offset.y),
            _offset);
        foreach (var patch in _patches)
        {
            foreach (var linker in patch._linkers)
            {
                Gizmos.color = _linkGizmoColor;
                if (patch._currentPatchInfo)
                    Gizmos.DrawLine(patch._currentPatchInfo.transform.position + (Vector3.up * 0.2f),
                        linker._patchData._linker.transform.position + (Vector3.up * 0.2f));
            }
        }
    }
#endif
}