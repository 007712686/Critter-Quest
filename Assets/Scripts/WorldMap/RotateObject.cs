using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Amount of rotation in degrees per press
    public float rotationAmount = 45f;

    // Rotation speed (how fast the object rotates)
    public float rotationSpeed = 5f;

    private float targetRotation;
    private bool isRotating = false;

    public int selection = 0;
    // 0 is home, 1 is town, 2 is forest

    void Update()
    {

        // Check for Q or E key presses only if not already rotating
        if (!isRotating)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {

                if (selection == 2)
                {
                    rotationAmount = 180;
                }
                else
                {

                    rotationAmount = 90;
                }
                targetRotation = NormalizeAngle(transform.eulerAngles.z - rotationAmount);
                isRotating = true;
                switch (selection)
                {
                    case 0:
                        selection = 2;
                        break;
                    case 1:
                        selection = 0;
                        break;
                    case 2:
                        selection = 1;
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {

                if (selection == 1)
                {
                    rotationAmount = 180;
                }
                else
                {

                    rotationAmount = 90;
                }
                targetRotation = NormalizeAngle(transform.eulerAngles.z + rotationAmount);
                isRotating = true;
                switch (selection)
                {
                    case 0:
                        selection = 1;
                        break;
                    case 1:
                        selection = 2;
                        break;
                    case 2:
                        selection = 0;
                        break;
                }
            }
        }

        // If the object is rotating, smoothly rotate it towards the target rotation
        if (isRotating)
        {
            float currentRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, Time.deltaTime * rotationSpeed);

            // Set the final rotation
            transform.eulerAngles = new Vector3(0, 0, currentRotation);

            // Snap to target rotation if close enough
            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetRotation)) < 0.1f)
            {
                transform.eulerAngles = new Vector3(0, 0, targetRotation);
                isRotating = false; // Finish the rotation
            }
        }
    }

    // Normalize angle to ensure it stays within 0 to 360 degrees range
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0) angle += 360;
        return angle;
    }
}
