using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform bowlingBall;
    public Transform[] cameraPositions; // Different camera positions (overhead, behind ball, pin view)

    private int currentCameraPosition = 0;
    private Vector3 offset;

    void Start()
    {
        if (bowlingBall != null)
        {
            offset = transform.position - bowlingBall.position;
        }
    }

    void Update()
    {
        // Switch camera view with Tab key
        if (Input.GetKeyDown(KeyCode.Tab) && cameraPositions.Length > 0)
        {
            currentCameraPosition = (currentCameraPosition + 1) % cameraPositions.Length;
            transform.position = cameraPositions[currentCameraPosition].position;
            transform.rotation = cameraPositions[currentCameraPosition].rotation;
        }
    }

    void LateUpdate()
    {
        if (currentCameraPosition == 0 && bowlingBall != null) // Follow ball in default view
        {
            // Smoothly follow the bowling ball
            Vector3 targetPosition = bowlingBall.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
