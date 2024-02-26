using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tilt : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField]
    private InputActionReference left, right, front, back;

    private void OnEnable()
    {
        left.action.Enable();
        right.action.Enable();
        front.action.Enable();
        back.action.Enable();
    }

    private void OnDisable()
    {
        left.action.Disable();
        right.action.Disable();
        front.action.Disable();
        back.action.Disable();
    }

    private void Update()
    {
        float leftTilt = left.action.ReadValue<float>();
        float rightTilt = right.action.ReadValue<float>();
        float frontTilt = front.action.ReadValue<float>();
        float backTilt = back.action.ReadValue<float>();

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
        else if (frontTilt > 0)
        {
            //Tilt front
            transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * speed, Space.World);
        }
        else if (backTilt > 0)
        {
            //Tilt front
            transform.Rotate(new Vector3(-1, 0, 0) * Time.deltaTime * speed, Space.World);
        }
    }
}
