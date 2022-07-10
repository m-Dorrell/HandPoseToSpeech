using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraLook : MonoBehaviour
{

    Vector2 previous;
    public Transform myCamera;

    public float sensitivity = 5f;

    private float xRot;
    private float yRot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        xRot = transform.localRotation.eulerAngles.x;
        yRot = transform.localRotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        xRot += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        yRot += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        yRot = Mathf.Clamp(yRot, -80f, 80f);

        Quaternion newRot = Quaternion.Euler(-yRot, xRot, 0f);

        myCamera.rotation = Quaternion.Slerp(myCamera.rotation,newRot, 5f);

        if(Input.GetKeyDown(KeyCode.Escape)){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
