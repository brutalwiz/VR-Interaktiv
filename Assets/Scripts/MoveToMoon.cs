using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMoon : MonoBehaviour
{
    float moveSpeed = 4;
    GameObject moon;
    Transform moonTransform;
    Vector3 moonDir;
    // Start is called before the first frame update
    void Start()
    {
        moon = GameObject.Find("Moon");
        if(moon != null){
            Transform moonTransform = moon.transform;
            moonDir = moonTransform.position - transform.position;
            moonDir = moonDir / moonDir.magnitude;
        }else{
            Debug.Log("MOON NOT FOUND!");
        }
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "Moon"){
            Debug.Log("Touched MOON!");
            Destroy(gameObject,2);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + moonDir*moveSpeed*Time.deltaTime;
    }

    
}
