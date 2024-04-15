using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TiltLogic : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 directionRot;
    
    [SerializeField] private Transform trayTransform;
    [SerializeField] private Slider sliderRight;
    [SerializeField] private Slider sliderFront;
    
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
            if ((angles.x < 0.211f && angles.x > -0.211f) && (angles.z < 0.211f && angles.z > -0.211f))
            {
                trayTransform.Rotate(directionRot * (speed * Time.deltaTime), Space.Self);
            }
            else
            {
                // Correct angles.x if out of bounds
                if (angles.x > 0.211f  || angles.x < -0.211f) {
                    float targetX = Mathf.Clamp(angles.x, -0.211f, 0.211f); // Get the nearest valid angle within the limits
                    angles.x = Mathf.MoveTowardsAngle(angles.x, targetX, speed * Time.deltaTime); // Smoothly move towards the target angle
                }

                // Correct angles.z if out of bounds
                if (angles.z > 0.211f || angles.z < -0.211f) {
                    float targetZ = Mathf.Clamp(angles.z, -0.211f, 0.211f); // Get the nearest valid angle within the limits
                    angles.z = Mathf.MoveTowardsAngle(angles.z, targetZ, speed * Time.deltaTime); // Smoothly move towards the target angle
                }
                
                trayTransform.rotation = angles;
            }
        }
    }
}
