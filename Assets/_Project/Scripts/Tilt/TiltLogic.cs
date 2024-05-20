using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TiltLogic : MonoBehaviour, TiltActions.ITIltActions
{
    public float speed = 10f;
    private Vector3 directionRot;
    private bool gyroActive;
    
    [SerializeField] private Transform trayTransform;
    private TiltActions _tiltActions;
    private Vector3 _movementValue;

    private void Start()
    {
        _tiltActions = new TiltActions();
        _tiltActions.TIlt.SetCallbacks(this);
        _tiltActions.TIlt.Enable();
    }
    
    public void RotateLeftRight(float rotateDir)
    {
        directionRot.z =  rotateDir;
    }
    
    public void RotateFrontBack(float rotateDir)
    {
        directionRot.x = rotateDir;
    }

    void Update()
    {
        Quaternion angles = trayTransform.rotation; 
        //Debug.Log(angles);

        if (GameManager.Instance.GameState == GameState.gaming)
        {
            switch (gyroActive)
            {
                case true:
                    if ((angles.x < 0.211f && angles.x > -0.211f) && (angles.z < 0.211f && angles.z > -0.211f))
                    {
                        trayTransform.Rotate(_movementValue * (speed * Time.deltaTime), Space.Self);
                    }
                    else
                    {
                        if (angles.x > 0.211f  || angles.x < -0.211f) {
                            float targetX = Mathf.Clamp(angles.x, -0.211f, 0.211f); 
                            angles.x = Mathf.MoveTowardsAngle(angles.x, targetX, speed * Time.deltaTime);
                        }

                        // Correct angles.z if out of bounds
                        if (angles.z > 0.211f || angles.z < -0.211f) {
                            float targetZ = Mathf.Clamp(angles.z, -0.211f, 0.211f);
                            angles.z = Mathf.MoveTowardsAngle(angles.z, targetZ, speed * Time.deltaTime);
                        }
                    
                        trayTransform.rotation = angles; 
                    }
                    break;
                case false:
                    if ((angles.x < 0.211f && angles.x > -0.211f) && (angles.z < 0.211f && angles.z > -0.211f))
                    {
                        trayTransform.Rotate(directionRot * (speed * Time.deltaTime), Space.Self);
                    }
                    else
                    {
                        if (angles.x > 0.211f  || angles.x < -0.211f) {
                            float targetX = Mathf.Clamp(angles.x, -0.211f, 0.211f); 
                            angles.x = Mathf.MoveTowardsAngle(angles.x, targetX, speed * Time.deltaTime);
                        }

                        // Correct angles.z if out of bounds
                        if (angles.z > 0.211f || angles.z < -0.211f) {
                            float targetZ = Mathf.Clamp(angles.z, -0.211f, 0.211f);
                            angles.z = Mathf.MoveTowardsAngle(angles.z, targetZ, speed * Time.deltaTime);
                        }
                    
                        trayTransform.rotation = angles; 
                    }
                    break;
            }
        }

    }

    public void OnRigth(InputAction.CallbackContext context)
    {
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
    }

    public void OnUp(InputAction.CallbackContext context)
    {
    }

    public void OnDown(InputAction.CallbackContext context)
    {
    }

    public void OnTiltGyro(InputAction.CallbackContext context)
    {
        _movementValue = context.ReadValue<Vector3>();
    }
    
    private void OnDestroy()
    {
        _tiltActions.TIlt.Disable();
    }
}
