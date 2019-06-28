using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotSpeed = 2.0f;
    private Animation anim;
    private OSC osc;
    public int maxSounds = 0;
    void Start()
    {
        osc = GameObject.Find("OSC").GetComponent<OSC>();
        if (osc != null)
        {
            Debug.Log("FIRE OSC!");
            OscMessage message = new OscMessage();
            message.address = "/Reset";
            message.values.Add(1);
            osc.Send(message);
        }
        else
        {
            Debug.LogError("OSC Object for " + gameObject.name + " not found!");
        }
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
        anim.Play("MoonImpact");
        AkSoundEngine.PostEvent("Play_Moon_response", gameObject);
        Debug.Log("Play ANIM IMPACT");
        maxSounds++;
        if(maxSounds >= 6){
             GameObject[] allLoopObjects = GameObject.FindGameObjectsWithTag("LoopObject");
            for(int i = 0;i<allLoopObjects.Length;i++){
                if(allLoopObjects[i].GetComponent<BoxCollider>() != null)
                {
                    allLoopObjects[i].GetComponent<BoxCollider>().enabled = false;
                }
                if (allLoopObjects[i].GetComponent<SphereCollider>() != null)
                {
                    allLoopObjects[i].GetComponent<SphereCollider>().enabled = false;
                }
                allLoopObjects[i].transform.SendMessage("selfDestruction");
            }
        }
    }
}
