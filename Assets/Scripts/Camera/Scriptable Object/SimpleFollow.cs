using UnityEngine;

[CreateAssetMenu(fileName = "Simple Follow", menuName = "Utilities/Camera/Simple Follow", order = 0)]
public class SimpleFollow : BaseCameraState
{
    [SerializeField][Multiline]
    private string developerNote = "Follow Target and Look At given offset by offset";
    [Range(0.1f, 5)]
    public float followSpeed = 4;

    [Range(0.1f, 30)] public float lookSpeed = 10;

    public override void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime)
    {
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, posOffset, followSpeed * deltaTime);
        camera.position = Vector3.Lerp(camera.position, target.position, followSpeed * deltaTime);
        camera.rotation = Quaternion.Slerp(camera.rotation, Quaternion.Euler(lookOffset), lookSpeed * deltaTime);
        UpdateFOV(camera);
    }
}