using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    Vector2 offset;
    public float speed;

    Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }
    void Update()
    {
        offset.x += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", offset);



    }
}