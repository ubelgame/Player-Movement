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
        Move();
    }

    void Move(){
        float movez = Input.GetAxis("Vertical");
        movement = new Vector3(0, 0, movez);
        movement = transform.TransformDirection(movement);

        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if(isGrounded && depth.y < 0){
            depth.y = -1;
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

    void Walk(){
        moveSpeed = walkSpeed;
        Ani.SetFloat("speed", 0f, 0.1f, Time.deltaTime);
    }

    void Run(){
        moveSpeed = runSpeed; 
        Ani.SetFloat("speed", 1f, 0.1f, Time.deltaTime);
    }

    // void Idle(){
    //     Ani.SetFloat("speed", 0, 0.1f, Time.deltaTime);

    // }

    void Jump(){
        depth.y = Mathf.Sqrt(jumpDistance * -1 * gravity);
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
            Walk();
        }
        else if(movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.C)){
            Run();
        }
        else if(movement != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.C)){
            Sliding();
        }
        
        }
    }
}
    
        

