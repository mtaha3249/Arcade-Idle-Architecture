using UnityEngine;

[CreateAssetMenu(fileName = "Follow Rotation", menuName = "Utilities/Camera/Follow Camera Rotation", order = 0)]
public class FollowCameraRotation : BaseCameraState
{
    [SerializeField] [Multiline] private string developerNote = "Follow Target and Look At given offset by offset";
    [SerializeField] [Range(0.1f, 20)] float followSpeed = 4;

    [SerializeField] [Range(0.1f, 30)] float lookSpeed = 10;

    public override void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime)
    {
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, posOffset, followSpeed * deltaTime);
        camera.position = Vector3.Lerp(camera.position, target.position, followSpeed * deltaTime);
        camera.rotation = Quaternion.Slerp(camera.rotation, target.rotation, lookSpeed * deltaTime);
        UpdateFOV(camera);
    }
}