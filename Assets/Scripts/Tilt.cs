using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tilt : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField]
    private InputActionReference left, right;

    private void OnEnable()
    {
        left.action.Enable();
        right.action.Enable();
    }

    private void OnDisable()
    {
        left.action.Disable();
        right.action.Disable();
    }

    private void Update()
    {
        float leftTilt = left.action.ReadValue<float>();
        float rightTilt = right.action.ReadValue<float>();

        if (leftTilt > 0)
        {
            // Tilt left
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed, Space.World);
        }
        else if (rightTilt > 0)
        {
            // Tilt right
            transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * speed, Space.World);
        }
    }
}
