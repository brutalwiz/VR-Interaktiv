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
    bool fade = false;
    float rtpcVal = 0;

    public bool moving;
    private int soundsCount=0;

    private bool destruction = false;
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
            fade = true;
            
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
            
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(moving){
            //Bewegung zum Mond
            transform.position = Vector3.Lerp(transform.position,moon.transform.position,Time.deltaTime*moveSpeed);
            moveSpeed += 0.0005f;
        }
        if (fade && rtpcVal<100)
        {
            AkSoundEngine.SetRTPCValue(RTPCName, rtpcVal);
            rtpcVal += 0.84f;
            if (rtpcVal > 100)
            {
                AkSoundEngine.SetRTPCValue(RTPCName, 100f);
                fade = false;
                Destroy(gameObject, 2);
            }
        }
        if(destruction){
            transform.position = transform.position + new Vector3(0,-1,0)*Time.deltaTime*20;
            moveSpeed += 0.001f;
        }
    }

    void moveTo() {
        moving = true;
        StopLoopEvent.Post(gameObject);
        ActionEvent.Post(gameObject);
    }

    void selfDestruction(){
        destruction = true;
        Destroy(gameObject,7);
    }
    
}
