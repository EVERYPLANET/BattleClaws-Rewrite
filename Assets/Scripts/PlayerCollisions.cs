using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerController _controller;

    private void Awake()
    {
        _controller = transform.parent.GetComponent<PlayerController>();
    }

    // This script is on the model, and passes the fact there's been a collision to the controller script
    private void OnTriggerEnter(Collider other)
    {
        print("Collision!");
        if (other.CompareTag("Player"))
        {
            print("P!");
            _controller.KnockBack(other.transform.parent.transform, true);
        }

        if (other.CompareTag("Boundary"))
        {
            // Get the size of the Box Collider
            Vector3 boundarySize = other.bounds.size;

            // Assuming the rectangle is aligned with X and Z axes, use X and Z size values
            float boundaryWidth = boundarySize.x;
            float boundaryHeight = boundarySize.z;

            Debug.Log("Boundary Size - Width: {boundaryWidth}, Height: {boundaryHeight}");

            // Clamp the player's position within the specified boundaries
            float x = Mathf.Clamp(_controller.Position.x, _controller.Position.x - boundaryWidth / 2, _controller.Position.x + boundaryWidth / 2);
            float z = Mathf.Clamp(_controller.Position.z, _controller.Position.z - boundaryHeight / 2, _controller.Position.z + boundaryHeight / 2);

            Debug.Log("Clamped Position - X: {x}, Z: {z}");

            // Update the player's position
            _controller.Position = new Vector3(x, _controller.Position.y, z);

            Debug.Log("Player position updated!");
            Debug.Log("Player Position - X: {_controller.Position.x}, Y: {_controller.Position.y}, Z: {_controller.Position.z}");
           
        }
    }
}
