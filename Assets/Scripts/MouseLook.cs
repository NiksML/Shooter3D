using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[AddComponentMenu("Camera-Control/MouseLook")]
public class MouseLook : MonoBehaviour
{
    //Оси вращения
    public enum RotationAxes {MouseXandY = 0, MouseX = 1, MouseY = 2};
    public RotationAxes axes = RotationAxes.MouseXandY;
    
    //Чувствительность мыши
    [Range(1f, 10f)] public float sensivityX = 2f;
    [Range(1f, 10f)] public float sensivityY = 2f;
    
    //Углы вращения XY
    public float minX = -360f;
    public float maxX = 360f;
    public float minY = -360f;
    public float maxY = 360f;
    
    //Текущий уголь вращения
    private float rotationX = 0f;
    private float rotationY = 0f;
    
    //Тип вращения
    private Quaternion originalRotation;

    private Rigidbody rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (rb)
        {
            rb.freezeRotation = true;
            originalRotation = transform.localRotation;
        }
    }
    
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }
        if (angle > 360f)
        {
            angle -= 360f;
        }

        return Mathf.Clamp(angle, min, max);
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseXandY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensivityY;

            rotationX = ClampAngle(rotationX, minX, maxX);
            rotationY = ClampAngle(rotationY, minY, maxY);
            
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensivityX;
            rotationX = ClampAngle(rotationX, minX, maxX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else if (axes == RotationAxes.MouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * sensivityY;
            rotationY = ClampAngle(rotationY, minY, maxY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }
}
