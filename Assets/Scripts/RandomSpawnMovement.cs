using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnMovement : MonoBehaviour
{
    public float minThrust;
    public float maxThrust;

    public float freq = 0.5f;
    public float magnitude = 0.5f;
    private Rigidbody rb;
    int phase;
    // Start is called before the first frame update
    void Start()
    {
        freq = freq*Random.Range(0.5f,1.5f);
        phase = Random.Range(0,360);
        rb = GetComponent<Rigidbody>();
        //transform.Rotate((int)Random.Range(0f,360f), (int)Random.Range(0f, 360f), (int)Random.Range(0f, 360f));
        rb.freezeRotation = true;
        //Impulse(transform.forward*Random.Range(minThrust,maxThrust));
    }
    private Vector3 oldVelocity;
    private void FixedUpdate()
    {
        oldVelocity = rb.velocity;
    }
    // Update is called once per frame
    void Update()
    {
        //Sinus Movement
        transform.position = transform.position+transform.up *Mathf.Sin(Time.time * freq+phase) * magnitude;
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 refVelocity = Vector3.Reflect(oldVelocity, contact.normal);
        rb.velocity = refVelocity;

        Quaternion rotation = Quaternion.FromToRotation(oldVelocity, refVelocity);
        transform.rotation = rotation * transform.rotation;
    }
    void Impulse(Vector3 dir)
    {
        rb.AddForce(dir, ForceMode.Impulse);
    }
}
