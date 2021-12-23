using UnityEngine;

public abstract class BaseCameraState : ScriptableObject
{
    // [FoldoutGroup("Field Of View Settings")]
    [SerializeField]
    // [LabelText("Default FOV")]
    float deafultFOV = 60;

    // [FoldoutGroup("Field Of View Settings")]
    [SerializeField]
    bool changeFOV;

    // [FoldoutGroup("Field Of View Settings")]
    // [ShowIf("changeFOV")]
    [SerializeField]
    // [LabelText("Target FOV")]
    float targetFOV = 90;

    // [FoldoutGroup("Field Of View Settings")]
    // [ShowIf("changeFOV")]
    [SerializeField]
    [Range(0.01f, 5)]
    // [LabelText("FOV Change Speed")]
    float fOVChangeSpeed = 0.25f;

    // [FoldoutGroup("Field Of View Settings")]
    [SerializeField]
    [Range(0.01f, 5)] 
    // [LabelText("Default FOV Change Speed")]
    float defaultFOVChangeSpeed = 1;

    [SerializeField] protected Vector3 posOffset, lookOffset;

    UnityEngine.Camera cam;
    private float currentFOV = 0;

    public Vector3 PosOffset
    {
        get => posOffset;
        set => posOffset = value;
    }

    public Vector3 LookOffset
    {
        get => lookOffset;
        set => lookOffset = value;
    }

    public abstract void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime);

    public virtual void OnDrawGizmos()
    {
    }

    protected virtual void UpdateFOV(Transform camera)
    {
        if (cam == null)
            cam = camera.GetComponentInChildren<UnityEngine.Camera>();

        currentFOV = cam.fieldOfView;
        currentFOV = Mathf.Lerp(currentFOV, changeFOV ? targetFOV : deafultFOV,
            (changeFOV ? fOVChangeSpeed : defaultFOVChangeSpeed) * Time.deltaTime);
        cam.fieldOfView = currentFOV;
    }
}