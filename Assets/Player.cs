using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;

    float rotation = 0.0f;
    float rotationSpeed = 250.0f * Mathf.Deg2Rad;
    
    public float playerSpeed;
    Rigidbody2D rb;
    public int gunSelect;
    public float gunSpeed;
    Color gunColor;
    public GameObject grenadeBoom;
    Vector3 grenadeLoc;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Example code to create a bullet and change its velocity:

    }

    void Update()
    {
        // start gun selecting with girl math
        if(gunSelect  == 0)
        {
            gunSpeed = 10.0f;
            gunColor = Color.red;
        }else if (gunSelect == 1)
        {
            gunSpeed = 7.5f;
            gunColor = Color.green;
        }else if (gunSelect == 2)
        {
            gunSpeed = 5.0f;
            gunColor = Color.blue;
        }






        float dt = Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            rotation += rotationSpeed * dt;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotation -= rotationSpeed * dt;
        }

        Vector3 direction = new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0.0f);
        Debug.DrawLine(transform.position, transform.position + direction * 10.0f);

        // Task 1: Move the player forwards when W is held, and backwards when S is held
        // Ensure movement is time-based

        Vector3 forward = transform.right;
        Vector3 right = transform.up;


        if (Input.GetKey(KeyCode.W))
        {
            transform.position += direction * playerSpeed * dt;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= direction * playerSpeed * dt;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Vector3 offset = transform.forward * 10 ;
            //Vector3 newPos = transform.position + offset;
            if (gunSelect == 0 )
            {
                GameObject bulletClone = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                bulletClone.GetComponent<SpriteRenderer>().color = gunColor;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * gunSpeed;
                Destroy(bulletClone, 1.0f);
            }
            else if(gunSelect == 1)
            {
                float newAngle = -30.0f ;
                

                for (int i = 0; i < 3; i++)
                {
                    float newDeg = newAngle * Mathf.Deg2Rad;
                    direction = new Vector3(Mathf.Cos(rotation + newDeg), Mathf.Sin(rotation + newDeg ), 0.0f);
                    GameObject bulletClone = Instantiate(bullet, transform.position + direction  , Quaternion.identity);
                    bulletClone.GetComponent<SpriteRenderer>().color = gunColor;
                    bulletClone.GetComponent<Rigidbody2D>().velocity = direction * gunSpeed;
                    Destroy(bulletClone, 1.0f);
                    newAngle += 30.0f;
                }

            } else if (gunSelect == 2)
            {
                GameObject bulletClone = Instantiate(bullet, transform.position + direction, Quaternion.identity);
                bulletClone.GetComponent<SpriteRenderer>().color = gunColor;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * gunSpeed;
                Debug.Log("Wtf?");
                grenadeLoc = bulletClone.transform.position;
                Invoke("grenadeParticles", 1.0f);
               


            }
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rifle"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            gunSelect = 0;
        }

        else if (collision.CompareTag("Shotgun"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            gunSelect = 1;
        }

        else if (collision.CompareTag("Grenade"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            gunSelect = 2;
        }


    }

    //bonus!!!!!!!!!!
    void grenadeParticles()
    {
        Debug.Log("tried");
        float newLoc = -45.0f;
        float radius = 2f;
        Vector3 randomPos = Random.insideUnitCircle * radius;
        for (int i = 0; i < 8; i++)
        {
            float newDeg = newLoc * Mathf.Deg2Rad;
            GameObject grenada = GameObject.FindGameObjectWithTag("bullet"); 
            
            
            //randomPos += grenada.transform.position;
            randomPos.y = 0f;
            Vector3 direction = randomPos - grenada.transform.position;
            direction.Normalize();

            float dotProduct = Vector3.Dot(grenada.transform.up, direction);
            float dotProductAngle = Mathf.Acos(dotProduct / grenada.transform.up.magnitude * direction.magnitude);

            randomPos.x = Mathf.Cos(dotProductAngle + newDeg) * radius + grenada.transform.position.x;
            randomPos.y = Mathf.Sin(dotProductAngle + newDeg) * radius + grenada.transform.position.y;
            randomPos.z = grenada.transform.position.z;

            GameObject go = Instantiate(grenadeBoom, randomPos, Quaternion.identity);
            go.transform.position = randomPos;
            Destroy(go, 1.0f);
            Destroy(grenada);
            newLoc += 45.0f;
        }
    }


}