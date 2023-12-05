using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public GameObject TriggerZone;
    public GameObject Door;
    public float smooth;

    private Quaternion DoorOpen;
    private Quaternion DoorClosed;

    void Start()
    {

        Door = GameObject.Find("Door");
        TriggerZone = GameObject.Find("TriggerZone");
    }

    void OnTriggerEnter(Collider cube)
    {

        if (cube.tag == "DoorButton")
            TriggerZone.SetActive(true);
        Debug.Log("button activated");

        DoorOpen = Door.transform.rotation = Quaternion.Euler(0, -90, 0);
        DoorClosed = Door.transform.rotation;

        Door.transform.rotation = Quaternion.Lerp(DoorClosed, DoorOpen, Time.deltaTime * smooth);
        Debug.Log("Door Opened");
    }
}
