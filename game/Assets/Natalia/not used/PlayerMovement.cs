using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementVelocity = 1;
    [SerializeField] float jumpVelocity = 10;

    float maxSpeed = 10.0f; float minSpeed = -10.0f;
    int jumpCounter = 0;
    int maxJumpCount = 1; // do przemyslenia czy chcemy double jump, jesli tak to zmienic na 2
    bool jump = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 heightPosition = GetComponentInParent<Rigidbody>().position;
        if (heightPosition[1] >= -10.0f)
        {
            Vector3 currentVelocity = GetComponentInParent<Rigidbody>().velocity;
            if (Input.GetKey(KeyCode.W))
            {
                currentVelocity += new Vector3(0.0f, 0.0f, movementVelocity);
            }
            if (Input.GetKey(KeyCode.S))
            {
                currentVelocity -= new Vector3(0.0f, 0.0f, movementVelocity);
            }
            if (Input.GetKey(KeyCode.A))
            {
                currentVelocity -= new Vector3(movementVelocity, 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                currentVelocity += new Vector3(movementVelocity, 0.0f, 0.0f);
            }
            if (jump)
            {
                if (jumpCounter < maxJumpCount)
                {
                    currentVelocity = new Vector3(currentVelocity[0], jumpVelocity, currentVelocity[2]);
                    jumpCounter++;
                    Debug.Log(jumpCounter);
                }
                jump = false;
            }
            for (int i = 0; i < 3; i++)
            {
                if (currentVelocity[i] >= maxSpeed)
                {
                    currentVelocity[i] = maxSpeed;
                }
                if (currentVelocity[i] <= minSpeed)
                {
                    currentVelocity[i] = minSpeed;
                }
            }
            //Debug.Log(currentVelocity);
            GetComponentInParent<Rigidbody>().velocity = currentVelocity;
        }
        else
        {
            SceneManager.LoadScene("TestScene");
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            jumpCounter = 0; // Counter zeruje sie przy dotknieciu ziemi, umozliwia ponowny skok az do osiagniecia maxJumpCount
            //Debug.Log(jumpCounter);
        }
        if (collision.collider.tag == "Enemy")
        {
            SceneManager.LoadScene("TestScene"); // restart poziomu przy dotknieciu przeciwnika
        }
    }
}
