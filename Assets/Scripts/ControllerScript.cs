using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{

    public GameObject go;
    public GameObject empty;
    // Start is called before the first frame update
    void Start()
    {
        empty = new GameObject();
        go = empty;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit)){
            if(hit.collider != null){
                if(go != hit.collider.gameObject){
                    /*go.transform.SendMessage("disableMaterial");
                    go = hit.collider.gameObject;
                    go.transform.SendMessage("enableMaterial");*/
                    Debug.Log("ON VR RAYCAST ENTER!");
                }
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    go.transform.SendMessage("moveTo");
                }
            }
        }else{
            if(go != null){
                //Raycast Exit
                //go.transform.SendMessage("disableMaterial");
                go = empty;
            }
        }
    }
}
