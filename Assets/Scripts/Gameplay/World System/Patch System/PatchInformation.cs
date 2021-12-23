using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Contains Patch Information for every type
/// </summary>
public class PatchInformation : MonoBehaviourID
{
    /// <summary>
    /// All linked patches
    /// </summary>
    public List<Patch> _linkers;

    /// <summary>
    /// What is the explore index of this patch
    /// </summary>
    [Tooltip("What is the explore index/depth of this patch")]
    public int _exploreIndex = 0;

    /// <summary>
    /// Final price of the patch
    /// </summary>
    [Tooltip("Total price of this patch including Item Mounted")]
    public float _price;

    /// <summary>
    /// Is Item Mounted
    /// </summary>
    public bool _hasItemMounted;

    /// <summary>
    /// Mounted Item ID to fetch Price
    /// </summary>
    [ShowIf("_hasItemMounted", true)] public string _mountedID;

    /// <summary>
    /// Total coins collected
    /// </summary>
    public GenericReference<int> _currentCollected;

    /// <summary>
    /// Raise Event when coins changed
    /// </summary>
    public GameEvent _collectedRaise;

    /// <summary>
    /// check from local saved
    /// </summary>
    public bool isLocked = true;

    /// <summary>
    /// Canvas in the child
    /// </summary>
    [ReadOnly] public ResourceUIView _resourceUI;

    /// <summary>
    /// Mesh of this patch
    /// </summary>
    private GameObject _patchMesh;

    private float _currentPrice;
    private SaveKeys.Keys<bool> _key;

    public void Init()
    {
        _key = new SaveKeys.Keys<bool>(ID, isLocked);
        // update current price
        // used for deduction
        _currentPrice = _price;
        // get mesh of the patch
        _patchMesh = transform.GetChild(0).gameObject;
        // update currency show when this is locked
        ShowPatchPrice();
        // align UI
        _resourceUI.transform.parent.rotation = Quaternion.Euler(Vector3.right * 90);
        // value on resource UI Handler
        _resourceUI.UpdateResourceOutput((int)_currentPrice);
        // fetch lock state
        GetLockState();
        // make patch visible or hidden
        SetPatchVisibility();
    }

    /// <summary>
    /// Deduct money by given amount
    /// </summary>
    /// <param name="amount">int amount to deduct</param>
    /// <returns>true if all money paid</returns>
    public bool DeductMoney(int amount)
    {
        if (_currentPrice <= 0)
        {
            _resourceUI.UpdateCurrencyUI(0);
            return true;
        }
        else
        {
            if (_currentCollected.Value > 0)
            {
                _currentPrice -= amount;
                _currentCollected.Value -= amount;
                _collectedRaise.Raise();
                _resourceUI.UpdateCurrencyUI((int)_currentPrice, true);
            }

            return false;
        }
    }

    /// <summary>
    /// Get patch lock state from local storage
    /// </summary>
    void GetLockState()
    {
        isLocked = SaveLoad.Fetch(_key);
    }
    
    /// <summary>
    /// Update Patch Lock State
    /// </summary>
    public void UpdateLockState()
    {
        SaveLoad.Save(_key.Key, isLocked);
    }

    /// <summary>
    /// Set patch visibility base on the lock state
    /// </summary>
    public void SetPatchVisibility()
    {
        if (!_patchMesh)
            _patchMesh = transform.GetChild(0).gameObject;
        _patchMesh.SetActive(!isLocked);
    }

    /// <summary>
    /// Set patch visibility base on the lock state
    /// </summary>
    /// <param name="tween">Can Tween</param>
    public void SetPatchVisibility(bool tween)
    {
        if (!_patchMesh)
            _patchMesh = transform.GetChild(0).gameObject;

        _patchMesh.SetActive(!isLocked);
        Vector3 targetValue = isLocked ? Vector3.zero : Vector3.one;
        Vector3 currentValue = isLocked ? Vector3.one : Vector3.zero;
        _patchMesh.transform.localScale = currentValue;
        _patchMesh.transform.DOScale(targetValue, 0.25f).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// Show Patch price
    /// </summary>
    public void ShowPatchPrice()
    {
        try
        {
            // update currency show when this is locked
            _resourceUI.UpdateCurrencyUI((int)_currentPrice, true);
        }
        catch (ArgumentNullException)
        {
            Debug.LogError("Resource UI couldn't found. Try Fetch Patch and Disable Patch from Patch Manager");
        }
    }

    /// <summary>
    /// Show linking patch price tag
    /// </summary>
    public void SetBoundingPatchVisibility()
    {
        foreach (var linker in _linkers)
        {
            linker._patchData._linkerScript.SetUIVisibility(linker._patchData._linkerScript.isLocked);
            linker._patchData._linkerScript.ShowPatchPrice();
        }
    }

    /// <summary>
    /// Set UI Visibility base on given parameter
    /// </summary>
    /// <param name="toggleUI">show or hide UI bool</param>
    public void SetUIVisibility(bool toggleUI)
    {
        _resourceUI.gameObject.SetActive(toggleUI);
    }

    /// <summary>
    /// Enable the side bounds of the patch to restrict player without going out
    /// </summary>
    public void RefreshBounds()
    {
        foreach (var patchInfo in _linkers)
        {
            patchInfo._collider.gameObject.SetActive(patchInfo._patchData._linkerScript.isLocked);
        }
    }

    /// <summary>
    /// Run whenever inspector updated
    /// Even new entry added
    /// </summary>
    protected override void Validate()
    {
        _parameters = new List<object>();
        _parameters.Add(gameObject);
        foreach (var patch in _linkers)
        {
            _parameters.Add(patch._patchData._linker);
        }
    }

    /// <summary>
    /// Fetch Resource UI Vew
    /// </summary>
    /// <returns>canvas UI</returns>
    public ResourceUIView _FetchResourceUIView()
    {
        return transform.GetChild(1).GetChild(0).GetComponent<ResourceUIView>();
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(PatchInformation))]
[UnityEditor.CanEditMultipleObjects]
public class PatchInformationEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PatchInformation _patchInformation = (PatchInformation) target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Fetch UI"))
        {
            _patchInformation._resourceUI = _patchInformation._FetchResourceUIView();
        }

        GUILayout.EndHorizontal();
    }
}
#endif