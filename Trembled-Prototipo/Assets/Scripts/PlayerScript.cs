using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speed = 4;
    public float rotationSpeed = 10;

    public Animator anim_controller;

    private Vector3 forward, right;

    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Camera.main.transform.right;
        right.y = 0;
        right = Vector3.Normalize(right);

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        

        Vector3 direction = horizontalInput * right + verticalInput * forward;
        if (direction.magnitude > 0.1f)
        {
            transform.position += direction * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            anim_controller.SetFloat("move", direction.magnitude);
        }
        else
        {
            anim_controller.SetFloat("move", 0f);
        }
    }
}
