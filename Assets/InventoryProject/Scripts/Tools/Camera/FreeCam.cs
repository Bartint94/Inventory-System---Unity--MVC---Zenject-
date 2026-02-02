using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    float xRot;
    float yRot;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position += transform.up * -speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -speed * Time.deltaTime;
        }
        xRot -= Input.GetAxisRaw("Mouse Y");
        yRot += Input.GetAxisRaw("Mouse X");
        transform.rotation = Quaternion.Euler(xRot, yRot, 0f);

    }
}
