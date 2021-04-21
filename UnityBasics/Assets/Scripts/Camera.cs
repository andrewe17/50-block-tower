using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
     public GameObject targetObject;
     private float targetAngle = 0;
     const float rotationAmount = 1.5f;
     public float rDistance = 1.0f;
     public float rSpeed = 1.0f;
      
     // Update is called once per frame
     void Update()
     {
      
         // Trigger functions if Rotate is requested
         if (Input.GetKeyDown(KeyCode.Q)) {
             targetAngle -= 90.0f;
         } else if (Input.GetKeyDown(KeyCode.E)) {
             targetAngle += 90.0f;
         }
      
         if(targetAngle != 0)
         {
             Rotate();
         }
     }
      
     protected void Rotate()
     {
 
         float step = rSpeed * Time.deltaTime;
         float orbitCircumference = 2F * rDistance * Mathf.PI;
         float distanceDegrees = (rSpeed / orbitCircumference) * 360;
         float distanceRadians = (rSpeed / orbitCircumference) * 2 * Mathf.PI;
         
         if (targetAngle > 0)
         {
             transform.RotateAround(targetObject.transform.position, Vector3.up, -rotationAmount);
             targetAngle -= rotationAmount;
         }
         else if(targetAngle < 0)
         {
             transform.RotateAround(targetObject.transform.position, Vector3.up, rotationAmount);
             targetAngle += rotationAmount;
         }
      
     }
}
