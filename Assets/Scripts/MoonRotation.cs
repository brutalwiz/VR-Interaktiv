using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotSpeed = 2.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right*rotSpeed*Time.deltaTime);
    }
}
