using UnityEngine;

[CreateAssetMenu(fileName = "Smooth Camera Rotate", menuName = "Utilities/Camera/Smooth Camera Rotation",
    order = 0)]
public class SmoothCameraRotate : BaseCameraState
{
    // The distance in the x-z plane to the target
    public float distance = 10.0f;

    // the height we want the camera to be above the target
    public float height = 5.0f;

    // How much we 
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    // Place the script in the Camera-Control group in the component menu
    public override void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime)
    {
        // Early out if we don't have a target
        if (!target) return;

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = camera.eulerAngles.y;
        float currentHeight = camera.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle =
            Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        camera.position = target.position;
        camera.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        camera.position = new Vector3(camera.position.x, currentHeight, camera.position.z);

        // Always look at the target
        camera.LookAt(target);
    }
}