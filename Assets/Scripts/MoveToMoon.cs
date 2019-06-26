using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMoon : MonoBehaviour
{
    float moveSpeed = 0.01f;
    GameObject moon;
    Transform moonTransform;
    Vector3 moonDir;
    private OSC osc;
    public int loopNmbr;
    public string RTPCName;
    public AK.Wwise.Event ActionEvent;
    public AK.Wwise.Event StopLoopEvent;


    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        moon = GameObject.Find("Moon");
        //Sucht OSC Game Object mit OSC Script
        osc = GameObject.Find("OSC").GetComponent<OSC>();
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
            //Wenn OSC Objectscript gefunden wurde -> Sende Fadein befehl an Max
            if (osc != null)
            {
                Debug.Log("FIRE OSC!");
                OscMessage message = new OscMessage();
                message.address = "/StartLoop";
                message.values.Add(loopNmbr);
                osc.Send(message);
            }
            else
            {
                Debug.Log("OSC Object for " + gameObject.name + " not found!");
            }

            //Zerstöre THIS 2 sek nach berühren des Mondes
            Destroy(gameObject,2);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(moving){
            //Bewegung zum Mond
            transform.position = Vector3.Lerp(transform.position,moon.transform.position,Time.deltaTime*moveSpeed);
            moveSpeed += 0.0001f;
        }
    }

    void moveTo() {
        moving = true;
        StopLoopEvent.Post(gameObject);
        ActionEvent.Post(gameObject);

    }
    void enableMaterial(){
        //TODO: CHANGE MATERIAL
    }
    void disableMaterial(){
        //TODO: CHANGE MATERIAL
    }

    
}
