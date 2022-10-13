using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotor : MonoBehaviour
{
   private const float LANE_DISTANCE = 3.0f;
    private const float TURN_SPEED = 0.05f;
   
    // Movement
    private CharacterController controller;
    private float jumpForce = 4.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private float speed = 7.0f;
   
    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right

    private void Start()
    {
         controller = GetComponent<CharacterController>();
    }

  
    private void Update()
{
    // Gather inpits of where we should be 
    if (Input.GetKeyDown (KeyCode.LeftArrow))
        MoveLane (false);
    if (Input.GetKeyDown (KeyCode.RightArrow))
        MoveLane (true);
//Calculate whwere we should be in the future

    Vector3 targetPosition = transform.position.z * Vector3.forward;
    if (desiredLane == 0)
        targetPosition += Vector3.left * LANE_DISTANCE;
    else if (desiredLane == 2)
        targetPosition += Vector3.right * LANE_DISTANCE;

// Let's calculate our move delta
    Vector3 moveVector = Vector3.zero;
    moveVector.x = (targetPosition - transform.position).normalized.x * speed;
    moveVector.y = -0.1f;
    moveVector.z = speed;

// Move the Pengu
    controller.Move (moveVector * Time.deltaTime);
// Rotate the Pengu to where he is going
    Vector3 dir = controller.velocity;
    dir.y = 0;
    transform.forward = Vector3.Lerp(transform.forward,dir,TURN_SPEED);





}       
    
    private void MoveLane (bool goingRight)
{
    desiredLane += (goingRight) ? 1 : -1;
    desiredLane = Mathf.Clamp(desiredLane, 0, 2);
}



}

