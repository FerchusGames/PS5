using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSphere : MonoBehaviour
{
    [SerializeField] private float _gizmoRadius = 1;
    
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _gizmoRadius);
    }
}
