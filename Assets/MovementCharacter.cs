using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    CharacterController Cc;
    public float _Speed = 1;
    Vector3 direction;
    public Transform Cameratransform;
    
    private void Awake()
    {
        Cc = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;

        if(input.GetKey(KeyCode.W))
        {
            direction += Cameratransform.forward;
        }
        if (input.GetKey(KeyCode.S))
        {
            direction += -Cameratransform.forward;
        }
        if (input.GetKey(KeyCode.D))
        {
            direction += Cameratransform.right;
        }
        else if (input.GetKey(KeyCode.A))
        {
            direction += -Cameratransform.right;
        }
        Cc.Move(direction.normalized * Time.deltaTime * _Speed);
    }
}
