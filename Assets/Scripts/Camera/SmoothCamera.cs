using System;
using UnityEngine;

public enum PlayerState
{
    MainMenu = 0,
    Running = 1,
    FinalMomentum = 2,
    Falling = 3,
    Complete = 4,
    Fail = 5
}

[Serializable]
public struct CameraStateInfo
{
    //[InlineEditor(InlineEditorModes.FullEditor)]
    public BaseCameraState cameraState;

    public PlayerState gameState;
}

[ExecuteInEditMode]
public class SmoothCamera : MonoBehaviour
{
    public enum UpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    [SerializeField] private UpdateType updateType;
    [SerializeField] Transform pivot;
    [SerializeField] bool autoFindPlayer = true;

    [SerializeField] 
    //[ShowIf("autoFindPlayer")] 
    [Tag]
    string targetTag;

    [SerializeField] //[HideIf("autoFindPlayer")]
    Transform target;

    [SerializeField] CameraStateInfo[] cameraStates;
    //[SerializeField] [ReadOnly] 
    BaseCameraState cameraState;

    private void Start()
    {
        PlayerMovement.OnPlayerStateChange += OnStateChange;
        if (autoFindPlayer == true)
        {
            target = GameObject.FindGameObjectWithTag(targetTag).transform;
        }

        cameraState = cameraStates[0].cameraState;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnPlayerStateChange -= OnStateChange;
    }

    void OnStateChange(PlayerState playerState)
    {
        for (int x = 0; x < cameraStates.Length; x++)
        {
            if (cameraStates[x].gameState == playerState)
                cameraState = cameraStates[x].cameraState;
        }
    }

    private void Update()
    {
        if (updateType == UpdateType.Update)
            cameraState.UpdateCamera(target, transform, pivot, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (updateType == UpdateType.FixedUpdate)
            cameraState.UpdateCamera(target, transform, pivot, Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (updateType == UpdateType.LateUpdate)
            cameraState.UpdateCamera(target, transform, pivot, Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (cameraState != null)
            cameraState.OnDrawGizmos();
    }
}