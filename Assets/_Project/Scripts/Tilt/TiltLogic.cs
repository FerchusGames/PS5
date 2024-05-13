using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TiltLogic : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 directionRot;
    
    [SerializeField] private Transform trayTransform;

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
        
        if (GameManager.Instance.GameState is GameState.gaming)
        {
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
        }
    }
}
