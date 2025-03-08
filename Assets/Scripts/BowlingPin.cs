using UnityEngine;

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
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isFallen = false;
    }

    public void AnimateReset()
    {
        // First deactivate physics
        rb.isKinematic = true;

        // Animate back to start position (this would normally use a proper animation)
        LeanTween.move(gameObject, startPosition, 0.5f)
            .setEaseInOutQuad();
        LeanTween.rotate(gameObject, startRotation.eulerAngles, 0.5f)
            .setEaseInOutQuad()
            .setOnComplete(() =>
            {
                rb.isKinematic = false;
                isFallen = false;
            });
    }
}
