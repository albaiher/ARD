using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),-1f, Input.GetAxis("Vertical"));

        controller.Move(Time.deltaTime * movement *25);
    }
}
