using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal") * speed * 0.1f, Input.GetAxis("Vertical") * speed * 0.1f, 0f);

        transform.position += move;

        Vector3 lookAt = mouseScreenPosition;
        float AngleRad = Mathf.Atan2(lookAt.y - transform.position.y, lookAt.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90f);

        //Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, -1f);

        //camera.position = desiredPosition;

    }
}
