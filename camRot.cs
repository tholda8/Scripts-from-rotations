using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRot : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float zoomSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 50f;

    private float distance = 10f; // poèáteèní vzdálenost od støedu
    private Vector2 rotation = new Vector2(45f, 30f); // poèáteèní úhly

    void Start()
    {
        // spoèítáme výchozí vzdálenost
        distance = Vector3.Distance(transform.position, Vector3.zero);
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            rotation.x += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            rotation.y -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            rotation.y = Mathf.Clamp(rotation.y, -80f, 80f);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }

        Vector3 dir = Quaternion.Euler(rotation.y, rotation.x, 0f) * Vector3.back;

        transform.position = Vector3.zero + dir * distance;
        transform.LookAt(Vector3.zero);
    }
}
