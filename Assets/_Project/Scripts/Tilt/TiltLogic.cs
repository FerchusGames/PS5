using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TiltLogic : MonoBehaviour
{
    public float speed = 100f;  
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
        if (GameManager.Instance.GameState is GameState.gaming or GameState.tutorial)
        {
            trayTransform.Rotate(directionRot * (speed * Time.deltaTime), Space.Self);
            Quaternion angles = trayTransform.rotation; 
            
            if (angles.x > 0.211f  || angles.x < -0.211f) {
                float targetX = Mathf.Clamp(angles.x, -0.211f, 0.211f);
                angles.x = targetX;
            }
            
            if (angles.z > 0.211f || angles.z < -0.211f) {
                float targetZ = Mathf.Clamp(angles.z, -0.211f, 0.211f);
                angles.z = targetZ;
            }
            
            trayTransform.rotation = angles;
        }
    }
}
