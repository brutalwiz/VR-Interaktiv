using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    private OSC osc;
    public GameObject[] spawnPoints;
    public GameObject[] moonObjects;
    private GameObject TEMP;
    // Start is called before the first frame update
    void Start()
    {
        TEMP = new GameObject();
        osc = GameObject.Find("OSC").GetComponent<OSC>();
        PlaceObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetGame();
        }
        
    }

    void PlaceObjects()
    {
        //Randomizes Spawnpoints
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rng = Random.Range(0, spawnPoints.Length);
            TEMP = spawnPoints[rng];
            spawnPoints[rng] = spawnPoints[i];
            spawnPoints[i] = TEMP;
        }

        //Assing all Moon Objects a random spawn position from the list opf spawn defining objects
        for(int i = 0; i< moonObjects.Length; i++)
        {
            if(spawnPoints.Length < moonObjects.Length)
            {
                Debug.LogError("Check Spanpoint/MoonObject Count on GAME MANAGER object.");
                break;
            }

            if (moonObjects[i] != null)
            {
                moonObjects[i].SetActive(true);
                moonObjects[i].transform.position = spawnPoints[i].transform.position;
                Debug.Log("Spawn: " + spawnPoints[i].transform.position);
                Debug.Log("Obj: " + moonObjects[i].transform.position);
            }
            else
            {
                Debug.LogError("A Moon Object is NULL! int i = " + i);
                break;
            }
        }
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].SetActive(false);
        }

    }

    void ResetGame()
    {
        //Sending Reset command to maxMsp
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
        //Resetting all RTPC's
        AkSoundEngine.SetRTPCValue("Blue_3_Waves", 0);
        AkSoundEngine.SetRTPCValue("Blue_Wave", 0);
        AkSoundEngine.SetRTPCValue("Dark_Blue_Sphere", 0);
        AkSoundEngine.SetRTPCValue("Green_Noodle", 0);
        AkSoundEngine.SetRTPCValue("Green_Pulsating_Sphere_Cluster", 0);
        AkSoundEngine.SetRTPCValue("Orange_Blanket", 0);
        AkSoundEngine.SetRTPCValue("Orange_Pulsating_Sphere_Cluster", 0);
        AkSoundEngine.SetRTPCValue("Red_Cube", 0);
        AkSoundEngine.SetRTPCValue("Red_Sphere", 0);
        AkSoundEngine.SetRTPCValue("Yellow_Pyramide", 0);


        SceneManager.LoadScene("SampleScene");
    }
}
