using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    
    // refrences
    [SerializeField]float movez;
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
    Animator Ani;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        Ani = GetComponentInChildren<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        movez = Input.GetAxis("Vertical");
        Move(movez);
    }

    void Move(float movez){
      
        movement = new Vector3(0, 0, movez);
        movement = transform.TransformDirection(movement);

        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if(isGrounded && depth.y < 0){
            depth.y = -2;
            Ani.SetBool("isGround", true);
            Ani.SetBool("isJumping", false);
            Ani.SetBool("isFalling", false);
        }
        else if(!isGrounded && depth.y < 0){
            Ani.SetBool("isFalling", true);
        }

        // dodging movement
        if(Input.GetKey(KeyCode.V)){
            Ani.SetBool("isDodging", true);
        }
        else if(!Input.GetKey(KeyCode.V)){
            Ani.SetBool("isDodging", false);
        }

        
        
        if(movement != Vector3.zero){
            Ani.SetBool("isMoving",true);
            MovementAnimation();
        }
        else if(movement == Vector3.zero){
            Ani.SetBool("isMoving",false);
        }
        
        
        movement *= moveSpeed;

        if(Input.GetKey(KeyCode.Space)){
            Jump();
        }
        
        // if(movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C)){
        //     Slidefalse();
        //     MovementAnimation();
        // }
        
        // else if(movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.C)){
        //     Slidetrue();
        
        // }
                
        
        
        control.Move(movement * Time.deltaTime);
        depth.y += gravity * Time.deltaTime;
        control.Move(depth * Time.deltaTime);
    }

    void Walk(float movez){
        moveSpeed = walkSpeed;
        if(movez > 0){
            Ani.SetFloat("speed", 0f, 0.1f, Time.deltaTime);
            Ani.SetFloat("reverse",1.0f);
        }
        else if(movez < 0){
            Ani.SetFloat("speed", 0f, 0.1f, Time.deltaTime);
            Ani.SetFloat("reverse",-1.0f);
        }
        
    }

    void Run(float movez){
        moveSpeed = runSpeed; 
        if(movez > 0){
            Ani.SetFloat("speed", 1f, 0.1f, Time.deltaTime);
            Ani.SetFloat("reverse",1.0f);
        }
        else if(movez < 0){
            Ani.SetFloat("speed", 1f, 0.1f, Time.deltaTime);
            Ani.SetFloat("reverse",-1.0f);
        }
        
    }

    // void Idle(){
    //     Ani.SetFloat("speed", 0, 0.1f, Time.deltaTime);

    // }

    void Jump(){
        depth.y = Mathf.Sqrt(jumpDistance * -1 * gravity);
        Ani.SetBool("isJumping", true);
    }

    // void Slidetrue(){
    //     Ani.SetBool("isSliding", true);
    // }

    // void Slidefalse(){
    //     Ani.SetBool("isSliding", false);
    // }
    // void Movetree(){
    //     Ani.SetBool("isMoving",true);
    // }

    void Sliding(){
        Ani.SetFloat("speed", 2f ,0.1f ,Time.deltaTime);
    }

    void MovementAnimation(){
         if(isGrounded )
         {
            if(movement != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C)){
            Walk(movez);
        }
        else if(movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C)){
            Run(movez);
        }
        else if(movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.C)){
            Sliding();
        }
        
        }
    }
}
    
        

