using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementY : MonoBehaviour
{
    public float speed = 30;
    public string axis = "Vertical2";

    public void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;

    }

    public void SetPosition(int positionx, int positiony)
    {
        transform.position = new Vector3(positionx, positiony, 0);
    }
}
