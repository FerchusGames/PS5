using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform cameraRotator;
    public GameObject selfCamera;
 
    public void Activate()
    {
        selfCamera.SetActive(true);
    }
    public void Deactivate()
    {
        selfCamera.SetActive(false);
    }
}
