using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        // Task 1 -- movement via WASD & direction with mouse or keyboard

        // Task 3 -- fire selected weapon when space is pressed
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Task 2 -- change weapon & color
        Debug.Log(name + " is colliding with " + collision.name);
    }
}
