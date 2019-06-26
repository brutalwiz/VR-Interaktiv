using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotSpeed = 2.0f;
    private Animation anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        anim["Impact"].layer = 123;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right*rotSpeed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        anim.Play("Impact");
        AkSoundEngine.PostEvent("Play_Moon_response", gameObject);
        Debug.Log("Play ANIM IMPACT");
    }
}
