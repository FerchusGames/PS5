using UnityEngine;
using UnityEngine.InputSystem;

public class Tilt : MonoBehaviour
{
    public float speed = 10f;

    [SerializeField]
    private InputActionReference left, right, up, down;

    private void OnEnable()
    {
        left.action.Enable();
        right.action.Enable();
        up.action.Enable();
        down.action.Enable();
    }

    private void OnDisable()
    {
        left.action.Disable();
        right.action.Disable();
        up.action.Disable();
        down.action.Disable();
    }

    private void Update()
    {
        float leftTilt = left.action.ReadValue<float>();
        float rightTilt = right.action.ReadValue<float>();
        float upTilt = up.action.ReadValue<float>();
        float downTilt = down.action.ReadValue<float>();

        if (leftTilt > 0)
        {
            transform.Rotate(new Vector3(0, 0, 1) * (Time.deltaTime * speed), Space.Self);
        }
        
        if (rightTilt > 0)
        {
            transform.Rotate(new Vector3(0, 0, -1) * (Time.deltaTime * speed), Space.Self);
        }
        
        if (upTilt > 0)
        {
            transform.Rotate(new Vector3(1, 0, 0) * (Time.deltaTime * speed), Space.Self);
        }
        
        if(downTilt > 0)
        {
            transform.Rotate(new Vector3(-1, 0, 0) * (Time.deltaTime * speed), Space.Self);
        }
    }
}
