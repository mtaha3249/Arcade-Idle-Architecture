using UnityEngine;

[CreateAssetMenu(fileName = "Follow", menuName = "Utilities/Camera/Follow", order = 0)]
public class FollowCamera : BaseCameraState
{
    [SerializeField][Multiline]
    private string developerNote = "Follow Target and Look At given offset by offset";
    [SerializeField] [Range(0.1f, 5)]
    float followSpeed = 4;

    [SerializeField] [Range(0.1f, 30)] float lookSpeed = 10;

    public override void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime)
    {
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, posOffset, followSpeed * deltaTime);
        camera.position = Vector3.Lerp(camera.position, target.position, followSpeed * deltaTime);
        camera.LookAt(target, lookOffset, lookSpeed, Utility.Axis.Y);
        UpdateFOV(camera);
    }
}