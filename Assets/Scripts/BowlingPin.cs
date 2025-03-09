using UnityEngine;
using System.Collections;

public class BowlingPin : MonoBehaviour
{
    public float fallThreshold = 0.5f; // How much the pin needs to tilt to count as fallen

    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool isFallen = false;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if pin is knocked over (if dot product with up vector is less than threshold)
        if (!isFallen && Vector3.Dot(transform.up, Vector3.up) < fallThreshold)
        {
            isFallen = true;
        }
    }

    public bool IsFallen()
    {
        return isFallen;
    }

    public void Reset()
    {
        // Reset position and physics state
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isFallen = false;
    }

    public void AnimateReset()
    {
        // First deactivate physics
        rb.isKinematic = true;

        // Start animation coroutine
        StartCoroutine(AnimateResetCoroutine());
    }

    private IEnumerator AnimateResetCoroutine()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            // Smooth step interpolation for more natural movement
            float smoothT = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPos, startPosition, smoothT);
            transform.rotation = Quaternion.Slerp(startRot, startRotation, smoothT);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure we're exactly at the target position/rotation
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Re-enable physics after animation completes
        rb.isKinematic = false;
        isFallen = false;
    }
}
