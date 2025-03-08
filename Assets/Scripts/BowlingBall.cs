using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public float throwForce = 20f;
    public float maxSidewaysPosition = 2f;
    public float sidewaysMovementSpeed = 3f;
    public Transform aimArrow;

    private bool isAiming = true;
    private bool isRolling = false;
    private Rigidbody rb;
    private Vector3 startPosition;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        gameManager = FindObjectOfType<GameManager>();

        // Disable physics until thrown
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isAiming)
        {
            // Move sideways with A/D or left/right arrow keys
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 position = transform.position;
            position.x += horizontalInput * sidewaysMovementSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, -maxSidewaysPosition, maxSidewaysPosition);
            transform.position = position;

            // Adjust aim with mouse movement
            if (aimArrow != null)
            {
                float mouseX = Input.GetAxis("Mouse X");
                aimArrow.Rotate(0, mouseX * 2f, 0);
                // Clamp rotation
                Vector3 currentRotation = aimArrow.eulerAngles;
                if (currentRotation.y > 180)
                    currentRotation.y = Mathf.Clamp(currentRotation.y, 340f, 380f);
                else
                    currentRotation.y = Mathf.Clamp(currentRotation.y, -20f, 20f);
                aimArrow.eulerAngles = currentRotation;
            }

            // Throw the ball on left mouse click or spacebar
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                ThrowBall();
            }
        }

        // Reset ball if it falls off the lane
        if (transform.position.y < -5f)
        {
            ResetBall();
        }
    }

    void ThrowBall()
    {
        isAiming = false;
        isRolling = true;
        rb.isKinematic = false;

        // Get throw direction from aim arrow
        Vector3 throwDirection = aimArrow != null ?
            aimArrow.forward :
            Vector3.forward;

        // Apply force to throw the ball
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        // Add torque (spin) to the ball
        rb.AddTorque(Vector3.right * throwForce * 0.5f);

        // Hide aim arrow
        if (aimArrow != null)
            aimArrow.gameObject.SetActive(false);
    }

    public void ResetBall()
    {
        isRolling = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPosition;

        // Show aim arrow for next throw
        if (aimArrow != null)
            aimArrow.gameObject.SetActive(true);

        isAiming = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // When ball reaches end of lane
        if (other.CompareTag("PinArea") && isRolling)
        {
            Invoke("NotifyBallReachedEnd", 5f); // Give time for pins to fall
        }
    }

    void NotifyBallReachedEnd()
    {
        if (gameManager != null)
        {
            gameManager.OnBallRolled();
        }
    }
}
