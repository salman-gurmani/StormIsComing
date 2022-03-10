using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(CnControls.CnInputManager.GetAxis("Horizontal"), 0, CnControls.CnInputManager.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.LogError("Col = " + hit.gameObject.name.ToString());
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.LogError("Trigger = " + other.gameObject.tag.ToString());
    //}

    private void OnTriggerStay(Collider other)
    {
        Debug.LogError("Trigger = " + other.gameObject.tag.ToString());
    }
}