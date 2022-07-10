using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float speed;
    public float walkSpeed;
    public float sprintSpeed;
    public Transform myCamera; 
    
    Vector3 moveVector;
    Rigidbody rb;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 tmpVector = myCamera.transform.forward * Input.GetAxis("Vertical") + myCamera.transform.right * Input.GetAxis("Horizontal");
        
        moveVector = new Vector3(tmpVector.x, 0, tmpVector.z);

        rb.MovePosition(transform.position +  moveVector.normalized * speed * Time.fixedDeltaTime);
    
        if(Input.GetKeyDown(KeyCode.Space) && grounded){
            rb.AddForce(transform.up * 350f);
        }

        if(Input.GetKey(KeyCode.LeftShift)){
            speed = sprintSpeed;
        }else{
            speed = walkSpeed;
        }
    }   

    void OnCollisionEnter(Collision collision){
        if(collision.transform.gameObject.CompareTag("Floor"))
            grounded = true;
    }

    void OnCollisionExit(Collision collision){
        if(collision.transform.gameObject.CompareTag("Floor"))
            grounded = false;
    }
}
