using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // refrences
    [SerializeField]float moveSpeed;
    [SerializeField]float walkSpeed;
    [SerializeField]float runSpeed;
    [SerializeField]LayerMask groundMask;
    [SerializeField]float gravity;
    [SerializeField]float groundDistance;
    [SerializeField]bool isGrounded;
    [SerializeField]float jumpDistance;

    private Vector3 movement;
    private Vector3 depth;

    CharacterController control;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        float movez = Input.GetAxis("Vertical");
        movement = new Vector3(0, 0, movez);

        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if(isGrounded && depth.y < 0){
            depth.y = -1;
        }
        
        if(isGrounded){
            if(movement != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)){
            Walk();
        }
        else if(movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift)){
            Run();
        }
        else if(movement == Vector3.zero){
            Idle();
        }
        
        movement *= moveSpeed;

        if(Input.GetKey(KeyCode.Space)){
            Jump();
        }
        
                
        }
    
        // if(movement != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)){
        //     Walk();
        // }
        // else if(movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift)){
        //     Run();
        // }
        // else if(movement == Vector3.zero){
        //     Idle();
        // }
        
        // movement *= moveSpeed;
        
        
        control.Move(movement * Time.deltaTime);
        depth.y += gravity * Time.deltaTime;
        control.Move(depth * Time.deltaTime);
    }

    void Walk(){
        moveSpeed = walkSpeed;
    }

    void Run(){
        moveSpeed = runSpeed; 
    }

    void Idle(){

    }

    void Jump(){
        depth.y = Mathf.Sqrt(jumpDistance * -1 * gravity);
    }
}
