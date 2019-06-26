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
    void Update()
    {
        if(moving){
            //Bewegung zum Mond
            transform.position = Vector3.Lerp(transform.position,moon.transform.position,Time.deltaTime*moveSpeed);
            moveSpeed += 0.0001f;
        }
        if (fade && rtpcVal<100)
        {
            AkSoundEngine.SetRTPCValue(RTPCName, rtpcVal, gameObject);
            rtpcVal += 0.84f;
            if (rtpcVal > 100)
            {
                AkSoundEngine.SetRTPCValue(RTPCName, 100f, gameObject);
                fade = false;
                Destroy(gameObject, 2);
            }
        }
    }

    void moveTo() {
        moving = true;
        StopLoopEvent.Post(gameObject);
        ActionEvent.Post(gameObject);

    }
    
}
