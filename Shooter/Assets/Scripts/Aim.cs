using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    void Start()
    {
       Cursor.visible = false;
    }
    void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
}
