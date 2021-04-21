using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform groundCheckTransform = null;
    private bool jumpKeyWasPressed;
    private float horizontalInput;

    private float verticalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining;

    private int swapped;

    private void Start() {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            swapped += 1;
        } else if(Input.GetKeyDown(KeyCode.Q)) {
            swapped -= 1;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

    }

    // FixedUpdate is called once every physics update 
    private void FixedUpdate() {
        float jumpMultiplier = 1;
        if((transform.position.y) > 38 && (transform.position.y < 46)) {
            jumpMultiplier = 0.7f;
        }
        if (swapped == 0) {
            rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, GetComponent<Rigidbody>().velocity.y, verticalInput * 2);
        } else if (swapped == 1 || swapped == -3) {
            rigidbodyComponent.velocity = new Vector3(-verticalInput * 2, GetComponent<Rigidbody>().velocity.y, horizontalInput * 2);
        } else if (swapped == -1 || swapped == 3) {
            rigidbodyComponent.velocity = new Vector3(verticalInput * 2, GetComponent<Rigidbody>().velocity.y, -horizontalInput * 2);
        } else if (swapped == 2 || swapped == -2) {
            rigidbodyComponent.velocity = new Vector3(-horizontalInput * 2, GetComponent<Rigidbody>().velocity.y, -verticalInput * 2);
        } else {
            swapped = 0;
            rigidbodyComponent.velocity = new Vector3(-horizontalInput * 2, GetComponent<Rigidbody>().velocity.y, -verticalInput * 2);
        }

        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }

        if(jumpKeyWasPressed)
        {
            float jumpPower = 7;
            if(superJumpsRemaining > 0) {
                jumpPower *= 1.2f;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower * jumpMultiplier, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 7 && superJumpsRemaining == 0) {
            other.gameObject.SetActive(false);
            superJumpsRemaining++;
            StartCoroutine(RespawnDelay(other));
        } else {
            Debug.Log("1 Super jump max!");
        }
    }

    IEnumerator RespawnDelay(Collider other) {
        yield return new WaitForSeconds(3);
        other.gameObject.SetActive(true);
    }

}