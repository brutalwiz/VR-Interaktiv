using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public string MovementType;
    public int speed,phase;
    public float x = 0.5f, y=0.5f, z=0.5f;
    public float magnitude= 0.5f;
    public float freq=0.5f;
    public bool randomRotation=true;
    // Start is called before the first frame update
    void Start()
    {
        phase = Random.Range(0, 360);
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementType == "rotation")
        {
            transform.Rotate(new Vector3(1*x,1*y,1*z), speed * Time.deltaTime);
            if (randomRotation)
            {
                x = x + Random.Range(-0.1f, 0.1f);
                y = y + Random.Range(-0.1f, 0.1f);
                z = z + Random.Range(-0.1f, 0.1f);
            }
        }
        else if (MovementType == "upDown")
        {
            transform.position = transform.position + new Vector3(0,1,0) * Mathf.Sin(Time.time * freq + phase) * magnitude;
            
        }
        else if (MovementType == "mixed")
        {
            transform.Rotate(new Vector3(1 * x, 1 * y, 1 * z), speed * Time.deltaTime);
            transform.position = transform.position + transform.up * Mathf.Sin(Time.time * freq + phase) * magnitude;
        }

    }
}
