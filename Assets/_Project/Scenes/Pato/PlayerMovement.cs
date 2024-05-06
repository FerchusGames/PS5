using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

       
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        
        Vector3 moveVelocity = moveDirection * moveSpeed;

      
        transform.Translate(moveVelocity * Time.deltaTime, Space.World);
        
    }
}
