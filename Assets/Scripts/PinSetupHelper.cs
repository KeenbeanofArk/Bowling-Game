using UnityEngine;

public class PinSetupHelper : MonoBehaviour
{
    public Transform pinPrefab;
    public Transform pinParent;

    // Add this line to show a button in the Inspector
    [ContextMenu("Setup Pins")]
    public void SetupPins()
    {
        // Clear existing pins if any
        foreach (Transform child in pinParent)
        {
            Destroy(child.gameObject);
        }

        // Define pin positions (standard bowling formation)
        Vector3[] pinPositions = new Vector3[]
        {
            new Vector3(0, 0, 0),           // Pin 1 (front)
            
            new Vector3(0.3f, 0, 0.3f),   // Pin 2 (second row)
            new Vector3(-0.3f, 0, 0.3f),    // Pin 3
            
            new Vector3(0.6f, 0, 0.6f),   // Pin 4 (third row)
            new Vector3(0, 0, 0.6f),       // Pin 5
            new Vector3(-0.6f, 0, 0.6f),    // Pin 6
            
            new Vector3(0.9f, 0, 0.9f),   // Pin 7 (back row)
            new Vector3(0.3f, 0, 0.9f),   // Pin 8
            new Vector3(-0.3f, 0, 0.9f),    // Pin 9
            new Vector3(-0.9f, 0, 0.9f)     // Pin 10
        };

        // Create pins at positions
        for (int i = 0; i < pinPositions.Length; i++)
        {
            Transform newPin = Instantiate(pinPrefab, pinParent);
            newPin.localPosition = pinPositions[i];
            newPin.name = "Pin_" + (i + 1);

            // Make sure the pin is properly positioned vertically
            // This assumes the pin's pivot point is at the bottom
            Collider collider = newPin.GetComponent<Collider>();
            if (collider != null)
            {
                float pinHeight = collider.bounds.size.y;
                newPin.localPosition = new Vector3(
                    newPin.localPosition.x,
                    pinHeight * 0.5f,  // Position at half height so bottom touches ground
                    newPin.localPosition.z
                );
            }
        }
    }
}
