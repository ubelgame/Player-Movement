using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]float mouseSensitivity;
    [SerializeField]GameObject follow;

    private Transform parent;
    Vector2 turn;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate(){
        float mousex = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        parent.Rotate(Vector3.up, mousex);

        float mousey = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; ///.deltaTime;
        parent.Rotate(Vector3.down, mousey);

        // turn.x += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        // turn.y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        // transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        
    }
}
