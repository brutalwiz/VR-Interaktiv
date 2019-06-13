using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMoon : MonoBehaviour
{
    float moveSpeed = 0.1f;
    GameObject moon;
    Transform moonTransform;
    Vector3 moonDir;

    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
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
        if(moving){
            transform.position = Vector3.Lerp(transform.position,moon.transform.position,Time.deltaTime*moveSpeed);
        }
    }

    void moveTo() {
        moving = true;
    }
    void enableMaterial(){
        //TODO CHANGE MATERIAL
    }
    void disableMaterial(){
        //TODO: CHANGE MATERIAL
    }

    
}
